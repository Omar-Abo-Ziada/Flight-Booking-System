using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Flight_Booking_System.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatRepository seatRepository;

        public SeatController(ISeatRepository _seatRepository)
        {
            seatRepository = _seatRepository;
        }


        [HttpGet]
        public ActionResult<GeneralResponse> GetAllSeats()
        {
            List<Seat> seats = seatRepository.GetAll();
            List<SeatDTO> seatDTOs = new List<SeatDTO>();
            foreach (Seat seat in seats)
            {
                SeatDTO seatDTO = new SeatDTO()
                {
                    Id = seat.Id,
                    Number = seat.Number,
                    Section = seat.Section,
                    TicketId = seat.TicketId
                };

                seatDTOs.Add(seatDTO);
            }
            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = seatDTOs,
                Message = "Here is all the Seats"
            };
        }

        [HttpGet("{seatId:int}")]
        public ActionResult<GeneralResponse> GetSeatById(int seatId)
        {
            Seat seat = seatRepository.GetById(seatId);
            if (seat == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Invalid Id"
                };
            }

            else
            {
                SeatDTO seatDTO = new SeatDTO()
                {
                    Id = seat.Id,
                    Number = seat.Number,
                    Section = seat.Section,
                    TicketId = seat.TicketId
                };

                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = seatDTO,
                    Message = "Here is the seat you Searched For"
                };
            }
        }

        [HttpPost]
        public ActionResult<GeneralResponse> AddSeat(SeatDTO seatDTO)
        {
            if(ModelState.IsValid)
            {
                Seat seat = new Seat()
                {
                    Id = seatDTO.Id,
                    Number = seatDTO.Number,
                    Section = seatDTO.Section,
                    TicketId = seatDTO.TicketId

                };
                seatRepository.Insert(seat);
                seatRepository.Save();
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = seatDTO,
                    Message = "Seat Added Successfully"
                };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Something Wrong"
                };
                
            }
        }


        [HttpPut("{seatId:int}")]
        public ActionResult<GeneralResponse> UpdateSeat(int seatId,SeatDTO seatDTO)
        {
            Seat seat=seatRepository.GetById(seatId);
           if(seat == null||seat.Id!=seatId)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Invalid Seat ID"

                };
            }

            else
            {
                seat.Id = seatDTO.Id;
                seat.Number = seatDTO.Number;
                seat.Section = seatDTO.Section;
                seat.TicketId = seatDTO.TicketId;
                seatRepository.Update(seat);
                seatRepository.Save();
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = seatDTO,
                    Message = "Updated Successfully"
                };

            }
        }


        [HttpDelete("{seatId:int}")]
        public ActionResult<GeneralResponse> DeleteSeat(int seatId)
        {
            Seat seat=seatRepository.GetById(seatId);
            if(seat==null)
            {

                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Invalid Seat ID"
                };
            }
            else
            {
                seatRepository.Delete(seat);
                seatRepository.Save();
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = null,
                    Message = "Seat Deleted Successfully"
                };
            }
        }



    }
}
