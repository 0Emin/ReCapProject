using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{CarId=1,BrandId=1,ColorId=1,DailyPrice=500,ModelYear=1999,Description="Exotic"},
                new Car{CarId=2,BrandId=1,ColorId=2,DailyPrice=600,ModelYear=1962,Description="Classic"},
                new Car{CarId=3,BrandId=2,ColorId=3,DailyPrice=700,ModelYear=2020,Description="Modern"},
                new Car{CarId=4,BrandId=2,ColorId=4,DailyPrice=900,ModelYear=2005,Description="Sport"},
                new Car{CarId=5,BrandId=3,ColorId=5,DailyPrice=1100,ModelYear=2022,Description="New"}

            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            //referanslarına göre bulup silmemiz gerekir. Bu yüzden _cars.Remove() gibi bir şey işimize yaramaz
            //Bu yüzden bir ürünü sileceğimiz zaman da onun Primary key ini kullanırız zaten


            //LINQ siz yazım Language Integrated Query -- Dile gömülü sorgulama
            //Car CarToDelete = null;
            //foreach (var c in _cars)
            //{

            //    if (c.CarId==car.CarId)
            //    {
            //        CarToDelete = c;
            //    }
            //}

            //_cars.Remove(CarToDelete);
                                 
                               //First
                               //FirstOrDefault (aynı şey)
            Car carsToDelete = _cars.SingleOrDefault(c=>c.CarId==car.CarId);

        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car CarToUpdate = _cars.SingleOrDefault(c=>c.CarId==car.CarId);
            CarToUpdate.CarId = car.CarId;
            CarToUpdate.BrandId = car.BrandId;
            CarToUpdate.ColorId = car.ColorId;
            CarToUpdate.DailyPrice = car.DailyPrice;
            CarToUpdate.ModelYear = car.ModelYear;
            CarToUpdate.Description = car.Description;
        }
    }
}
