using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetCarsByBrandId(int id);
        IDataResult<List<Car>> GetCarsByColorId(int colorId);
        IResult Add(Car car); //bu voidli diye bunu ayrı tuttuk, void yerine IResult yazdık
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<Car> GetById(int carId);
    }
}

//void yerine "IResult" yazdık
