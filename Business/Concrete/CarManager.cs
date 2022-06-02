using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validators.FluentValidation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IBrandService _brandService; //_brandDal şeklinde yapsaydık olmaz 

        public CarManager(ICarDal carDal, IBrandService brandService)
        {
            _carDal = carDal;
            _brandService = brandService;
        }
        [SecuredOperation("car.add, admin")]
        [ValidationAspect(typeof(CarValidator))] //Add metodunu doğrula, CarValidator ü kullanarak 
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            //business codes
            //validation ( doğrulama )

            //Ders 13 II. Kısım:  Bir markada en fazla 10 araba olabilir. En aşağı in
            var result = BusinessRules.Run(CheckIfCarNameExist(car.CarName),
                CheckIfCarCountOfBrandCorrect(car.BrandId),
                CheckIfBrandLimitExceded(car.BrandId));

            if (result != null)
            {
                return result;
            }
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
        [CacheAspect] //key,value ; key cache e verdiğimiz isim ::::: Parametre yoksa mesela GetAll() işlemini : Business.Concrete.CarMnager.GetAll
                      //Parametreli olanı ise Business.Concrete.CarManager.GetById(1) gibi cache yapabiliriz 
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 9)
            {                                         //Generate field yaptık ampulden                     
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }                                                 //bakım
            //Generate field yaptık ampulden               
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);

            //data =>(_carDal.GetAll());
        }
        [CacheAspect]
        [PerformanceAspect(5)] // bu metodun çalışması 5 saniyeyi geçerse beni uyar,**** eğer bunu Core daki inerceptor lere koyarsak sistemdeki her şeyi takip eder *****
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

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")] // Sadece get yazsaydık bellekteki içerisinde get olan tüm keyleri iptal etmiş olurduk. Yanş ürünü güncellemişken her yerdeki cache i silerdik
        public IResult Update(Car car)
        {
            if (CheckIfCarCountOfBrandCorrect(car.BrandId).Success)
            {
                _carDal.Update(car);

                return new SuccessResult(Messages.CarUpdated);
            }
            return new ErrorResult();
        }

        private IResult CheckIfCarCountOfBrandCorrect(int brandId)
        {
            //Select count(*) from cars where brandId=1
            var result = _carDal.GetAll(c => c.BrandId == brandId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.CarCountOfBrandError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCarNameExist(string carName)
        {

            var result = _carDal.GetAll(c => c.CarName == carName).Count();
            if (result > 1)
            {
                return new ErrorResult(Messages.CarNameAlreadyExist);
            }
            return new SuccessResult();
        }

        //private IResult CheckIfCarNameExist(string carName)
        //{

        //    var result = _carDal.GetAll(c => c.CarName == carName).Any();  //Any burada var mı ? görevi görüyor
        //    if (result)
        //    {
        //        return new ErrorResult(Messages.CarNameAlreadyExist);
        //    }
        //    return new SuccessResult();
        //}

        private IResult CheckIfBrandLimitExceded(int brandId)
        {

            var result = _brandService.GetAll();

            if (result.Data.Count > 15)              //Eğer mevcut marka sayısı 15i geçtiyse yeni araba ekleme 
            {
                return new ErrorResult(Messages.BrandLimitExceded);
            }

            return new SuccessResult();


            // Araba için brand nasıl yorumlanıyor? sorusunu aradaığımız için bu kısımda yazdığımız kodda. CarManager kısmına yazdık bu kodları
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
