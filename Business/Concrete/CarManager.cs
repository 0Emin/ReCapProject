using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IBrandService _brandService;
        public CarManager(ICarDal carDal, IBrandService brandService)
        {
            _carDal = carDal;
            _brandService = brandService;
        }

        
  
    
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfCarCountOfBrandCorrect(car.BrandId), CheckIfBrandLimitExceded());

            if (result != null)
            {
                return result;
            }
            _carDal.Add(car);
            return new SuccessResult(CarMessages.CarAdded);
        }

       
        public IResult Delete(Car car)
        {
            IResult result = BusinessRules.Run(CheckCardIdExist(car.CarId));

            if (result != null)
            {
                return result;
            }
            _carDal.Delete(car);
            return new SuccessResult(CarMessages.CarDeleted);
        }
        
        [ValidationAspect(typeof(CarValidator))]

        public IResult Update(Car car)
        {
            IResult result = BusinessRules.Run(CheckCardIdExist(car.CarId));

            if (result != null)
            {
                return result;
            }
            _carDal.Update(car);
            return new SuccessResult(CarMessages.CarUpdated);
        }

        public IDataResult<Car> GetByCarId(int carId)
        {

            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId));
        }

       
        public IDataResult<List<Car>> GetAll()
        {

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), CarMessages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            var result = new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), CarMessages.CarDetailList);
            return result;
        }
        private IResult CheckIfCarCountOfBrandCorrect(int brandId)
        {
            if (brandId == 1)
            {
                var result = _carDal.GetAll(c => c.BrandId == brandId).Count;
                if (result >= 2)
                {
                    return new ErrorResult(CarMessages.CarCountOfOpelError);
                }
            }
            return new SuccessResult();
        }
        private IResult CheckIfBrandLimitExceded()
        {
            var result = _brandService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IResult CheckCardIdExist(int carId)
        {
            var result = _carDal.GetAll(c => c.CarId == carId);
            if (result != null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        public IDataResult<List<Car>> GetByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByBrandId(brandId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByColorId(colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByCarId(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByCarId(carId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorAndByBrand(int colorId, int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByColorAndByBrand(colorId, brandId));
        }
    }
}