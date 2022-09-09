using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.Rental;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        IRentalService _rentalService;
        private readonly IMapper _mapper;

        public RentalsController(IRentalService rentalService, IMapper mapper)
        {
            _rentalService = rentalService;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _rentalService.GetAll();
            if (result.Success)
            {
                return Ok(result.Success);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(RentalDtoForAdd rentalDtoForAdd)
        {
            var rental = _mapper.Map<Rental>(rentalDtoForAdd);
            Payment payment = _mapper.Map<Payment>(rentalDtoForAdd.PaymentDtoForAdd);
            rental.Payment = payment;

            var result = _rentalService.Add(rental);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(RentalDtoForDelete rentalDtoForDelete)
        {
            var rental = _mapper.Map<Rental>(rentalDtoForDelete);
            var result = _rentalService.Delete(rental);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }
    }
}
