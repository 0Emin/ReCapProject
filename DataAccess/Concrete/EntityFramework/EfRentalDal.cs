using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;



namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from ca in context.Cars
                             join b in context.Brands
                             on ca.BrandId equals b.BrandId
                             join re in context.Rents
                             on ca.CarId equals re.CarId
                             join co in context.Colors
                             on ca.ColorId equals co.ColorId
                             from u in context.Users
                             join cu in context.Customers
                             on u.Id equals cu.UserId
                             select new RentalDetailDto
                             {
                                 CarId = ca.CarId,
                                 BrandId = b.BrandId,
                                 ColorName = co.ColorName,
                                 BrandName = b.BrandName,
                                 ModelName = ca.CarName,
                                 RentalId = re.RentalId,
                                 RentDate = re.RentDate,
                                 ReturnDate = re.ReturnDate,
                                 CustomerName = u.FirstName,
                                 CustomerLastname = u.LastName

                             };
                return result.ToList();
            }
        }
    }
}


//CARS HATA VERİYOR