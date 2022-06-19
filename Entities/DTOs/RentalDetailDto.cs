﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class RentalDetailDto
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastname { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public string ModelName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}