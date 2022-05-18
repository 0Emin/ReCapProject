using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void Add(Car car)
        {
            if (car.CarName.Length>=2&&car.DailyPrice>0)
            {
                _carDal.Add(car);
            }
            else
            {
                throw new NotImplementedException("Araba ismi minimum 2 karakter olmalı ve araba günlük fiyatı 0'dan büyük olmalıdır");
            }
            
        }

        public List<Car> GetAll()
        {
            //iş kodları
            //yetkisi var mı ? gibi süreçlerden geçtikten sonra
            // bize ilgili verileri verebilir.
            return _carDal.GetAll();

        }

        public List<Car> GetCarsByBrandId(int id)
        {
            return _carDal.GetAll(c => c.BrandId == id);
        }

        public List<Car> GetCarsByColorId(int brandid)
        {
            return _carDal.GetAll(c => c.ColorId == c.ColorId);
        }
    }
}
