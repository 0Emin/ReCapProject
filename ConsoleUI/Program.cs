using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Data Transformation Object => DTOs taşıyacağım objeler..
            CarTest();
            //IoC öğrenince new lememe gerek kalmıycak
            //BrandTest();
        }

        private static void BrandTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            foreach (var brand in brandManager.GetAll())
            {
                Console.WriteLine(brand.BrandName);
            }
        }

        private static void CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());

            var Result = carManager.GetCarDetails();
            if (Result.Success)
            {
                foreach (var car in Result.Data )
                {
                    Console.WriteLine("Araba ismi: {0}, Araba markası: {1}, Rengi: {2} Günlük Fiyat: {3}", car.CarName, car.BrandName, car.ColorName, car.DailyPrice);
                }
            }
            else
            {
                Console.WriteLine(Result.Message);
            }
            
        }
    }
}
