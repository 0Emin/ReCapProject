using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;


namespace DataAccess.Concrete.EntityFramework
{
    //public class EfRentDal : EfEntityRepositoryBase<Rent, ReCapContext>, IRentDal
    //{
    //    public List<RentDetailDto> GetRentalDetails()
    //    {
    //        using (ReCapContext context = new ReCapContext())
    //        {
    //            var result = from ca in context.Cars
    //                         join b in context.Brands
    //                         on ca.BrandId equals b.BrandId
    //                         join re in context.Rents
    //                         on ca.CarId equals re.CarId
    //                         join co in context.Colors
    //                         on ca.ColorId equals co.ColorId
    //                         from u in context.Users
    //                         join cu in context.Customers
    //                         on u.UserId equals cu.UserId
    //                         select new RentalDetailDto
    //                         {
    //                             CarId = ca.CarId,
    //                             BrandId = b.BrandId,
    //                             ColorName = co.ColorName,
    //                             BrandName = b.BrandName,
    //                             ModelName = ca.ModelName,
    //                             RentalId = re.RentalId,
    //                             RentDate = re.RentDate,
    //                             ReturnDate = re.ReturnDate,
    //                             CustomerName = u.FirstName,
    //                             CustomerLastname = u.LastName

    //                         };
    //            return result.ToList();
    //        }
    //    }
    //}
}


//CARS HATA VERİYOR