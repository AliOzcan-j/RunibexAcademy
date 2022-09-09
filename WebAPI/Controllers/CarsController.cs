using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.Car;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarService _carService;
        private readonly IMapper _mapper;

        public CarsController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(CarDtoForAdd carDtoForAdd)
        {
            
            var car = _mapper.Map<Car>(carDtoForAdd);
            car.Model = _mapper.Map<Model>(carDtoForAdd.ModelDto);
            var result = _carService.Add(car);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(CarDtoForUpdate carDtoForUpdate)
        {
            var car = _mapper.Map<Car>(carDtoForUpdate);
            var result = _carService.Update(car);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(CarDtoForUpdate carDtoForUpdate)
        {
            var car = _mapper.Map<Car>(carDtoForUpdate);
            var result = _carService.Delete(car);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            return BadRequest(result.Message);
        }
    }
}
