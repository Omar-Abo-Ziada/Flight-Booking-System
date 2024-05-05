using Flight_Booking_System.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse>> GetAll()
        {
          List<IdentityRole> roles = await roleManager.Roles.ToListAsync();
            if(roles.Count > 0)
            {
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = roles,
                    Message = "All roles"
                };
            }
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "No roles yet"
            };
        }


        [HttpGet("id:{int}")]
        public ActionResult<GeneralResponse> GetById(string id)
        {
            IdentityRole? role =  roleManager.Roles.FirstOrDefault(r => r.Id == id);
            if (role != null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = role,
                    Message = "Get role by id"
                };
            }
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "Invalid id"
            };
        }

        [HttpPost]                              // admin allowed to register users >> if yes >> choose their role from dropdown list >> must reflect on register e.p??????
        public async Task <ActionResult<GeneralResponse>> Add(string roleName)  // should ask for authority of this role????
        {
            IdentityRole role = new IdentityRole();
            role.Name = roleName;
            try
            {
                await roleManager.CreateAsync(role);
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = role,
                    Message = "Role created"
                };
            }
            catch (Exception ex) 
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Error on adding role"
                };
            }
        }



        [HttpDelete]
        public async Task <ActionResult<GeneralResponse>> Delete(string id)  // when delete role delete all its users?????
        {
           ActionResult <GeneralResponse> generalResponse = GetById(id);
           IdentityRole role = generalResponse.Value.Data;
            if (role != null) 
            {
                try
                {
                    await roleManager.DeleteAsync(role);
                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Data = role,
                        Message = "Role deleted"
                    };
                }
                catch (Exception ex)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "Error on deleting role"
                    };
                }
                }
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "Invalid role"
            };
        }


        [HttpPut]
        public async Task<ActionResult<GeneralResponse>> Update(IdentityRole editedRole)
        {
            IdentityRole? role = roleManager.Roles.FirstOrDefault(r => r.Id == editedRole.Id);
            if (role != null) 
            {
                try
                {
                    role.Name = editedRole.Name;
                    await roleManager.UpdateAsync(role);
                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Data = role,
                        Message = "Role updated"
                    };
                }
                catch(Exception ex)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = ex.Message,
                    };
                }

            }
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message ="Invalid role",
            };

        }
    }
}
