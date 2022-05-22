using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    //joinleri burada yapacağız. Bu bir veritabanı tablosu olmadığı için IEntity den inherit etmiyoruz
    public class CarDetailDto:IDto
    {
        public int CarId { get; set; }
        public string CarName  { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
    }
}
