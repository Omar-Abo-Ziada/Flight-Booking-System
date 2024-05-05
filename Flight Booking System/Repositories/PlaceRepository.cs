using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Flight_Booking_System.Repositories
{
    public class PlaceRepository : Repository<Place>, IPlaceRepository
    {
        public PlaceRepository(ITIContext _context) : base(_context)
        {

        }

        public List<Place> GetAllWithChilds(int flightId)
        {
            Flight flightfromDB = Context.Flights.FirstOrDefault(f => f.Id == flightId);

            IQueryable <Place> query = Context.Places.Where(p => p.ArrivingFlights.Contains(flightfromDB));

            query = query.Include(p => p.Country).Include(p => p.State);

            return query.ToList();
        }


        //public void Insert(Place item)
        //{
        //    Context.Add(item);
        //}

        //public void Update(Place item)
        //{
        //    Context.Update(item);
        //}

        //public List<Place> GetAll(string? include = null)
        //{
        //    if (include == null)
        //    {
        //        return Context.Places.ToList();
        //    }
        //    return Context.Places.Include(include).ToList();
        //}

        //public Place GetById(int Id)
        //{
        //    return Context.Places.FirstOrDefault(item => item.Id == Id);
        //}
        //public List<Place> Get(Func<Place, bool> where)
        //{
        //    return Context.Places.Where(where).ToList();
        //}

        //public void Delete(Place item)
        //{
        //    Context.Remove(item);
        //}

        //public void Save()
        //{
        //    Context.SaveChanges();
        //}
    }
}
