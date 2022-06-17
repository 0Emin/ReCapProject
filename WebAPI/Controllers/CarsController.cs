using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{             //Controller ın ismi
    [Route("api/[controller]")]    //Bir class la ilgili bilgi verme, onu imzalama yöntemidir attribute
    [ApiController] //=> ATTRIBUTE, Java da ANNOTATION
    public class CarsController : ControllerBase
    {
        //Loosely coupled (gevşek bağımlılık)
        //naming convention => Alt çizgi koyarak field oluşturma
        //IoC Container => Inversion of Control (değişimin kontrolü) -- startup kısmında var bu yapı
        ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }
        //bu http sorgularına "allias" (takma isim) verdik "getall,getbyid,add" gibi
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //Dependency chain --

            var result = _carService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetByCarId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]  //bi data vermemiz lazım. Adı üzerinde post
        //postman de body - raw - text=> json a tıklıyoruz.
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }

        [HttpGet("getbybrand")]
        public IActionResult GetByBrand(int brandId)
        {
            var result = _carService.GetByBrandId(brandId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCardetails")]
        public IActionResult GetCarDetails()
        {
            var result = _carService.GetCarDetails();

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();

        }
    }
}
