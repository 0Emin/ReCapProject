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

        public List<Car> GetAll()
        {
            //iş kodları
            //yetkisi var mı ? gibi süreçlerden geçtikten sonra
            // bize ilgili verileri verebilir.
            return _carDal.GetAll();

        }
    }
}
