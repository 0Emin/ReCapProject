using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validators.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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

        [ValidationAspect(typeof(CarValidator))] //Add metodunu doğrula, CarValidator ü kullanarak 
        public IResult Add(Car car)
        {
            //business codes
            //validation ( doğrulama )

            //fluent validation sayesinde aşağıdaki if li kodlardan kurtulcaz

            //Aşağıdaki kodlarımız bir validation yapacağımız zaman yazacağımız standart kodlarımızdır
            // (Buraları da refactor edeceğiz )

            


            //if (car.CarName.Length < 2)
            //{
            //    //magic strings => stringleri ayrı ayrı yazmak (sorun), bu yüzden Messages bölümü oluşturduk
            //    return new ErrorResult(Messages.CarNameInvalid);
            //}
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
            //if (car.CarName.Length >= 2 && car.DailyPrice > 0)
            //{
            //    _carDal.Add(car);
            //    return new Result(true,"Araba eklendi");   //bu şekilde yazabilmemiz için constructor lazım
            //}
            //else
            //{
            //    throw new NotImplementedException("Araba ismi minimum 2 karakter olmalı ve araba günlük fiyatı 0'dan büyük olmalıdır");
            //}
        }


        //Aşağıdaki yere IDataResult yazınca return kısmında _carDal ın altını çizmişti, bir "Data" da dönmesini bekliyomuş. Bu yüzden DataResult oluşturduk.
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 9)
            {                                         //Generate field yaptık ampulden                     
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            //Generate field yaptık ampulden               
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);

            //data =>(_carDal.GetAll());
        }

        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorid)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorid));
        }
    }
}
