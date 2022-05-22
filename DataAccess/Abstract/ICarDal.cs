using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal:IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarDetails();
    }
}


//Code Refactoring: Kodun iyileştirilmesi=> IEntityRepository yi Core katmanına taşıyınca sonrasında yapmamız gerekenler bu i