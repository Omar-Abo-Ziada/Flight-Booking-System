using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Flight_Booking_System.Repositories
{
   
        public class CountryRepository : Repository<Country>, ICountryRepository
        {
            public CountryRepository(ITIContext _context) : base(_context)
            {
            }

            public void Insert(Country item)
            {
                Context.Add(item);
            }

            public void Update(Country item)
            {
                Context.Update(item);
            }

            public List<Country> GetAll(string? include = null)
            {
                if (include == null)
                {
                    return Context.Countries.ToList();
                }
                return Context.Countries.Include(include).ToList();
            }

            public Country GetById(int Id)
            {
                return Context.Countries.FirstOrDefault(item => item.Id == Id);
            }
            public List<Country> Get(Func<Country, bool> where)
            {
                return Context.Countries.Where(where).ToList();
            }

            public void Delete(Country item)
            {
                Context.Remove(item);
            }

            public void Save()
            {
                Context.SaveChanges();
            }
        }
    }
