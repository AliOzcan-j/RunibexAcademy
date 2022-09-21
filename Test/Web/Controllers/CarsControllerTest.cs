using AutoMapper;
using Business.Abstract;
using Business.Extensions.MappingProfiles;
using Core.DataAccess;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;
using Entities.DTOs.Car;
using Entities.DTOs.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;

namespace Test.Web.Controllers
{
    public class CarsControllerTest
    {
        private readonly Mock<ICarService> _mock;
        private readonly CarsController _controller;
        private readonly IMapper _mapper;
        private List<Car> _cars;
        private CarDtoForAdd _carDtoForAdd;
        private CarDtoForUpdate _carDtoForUpdate;

        public CarsControllerTest()
        {
            _mock = new Mock<ICarService>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
            _controller = new CarsController(_mock.Object, _mapper);
            _cars = new List<Car>()
                        {
                            new Car()
                            {
                                Id = 1,
                                SupplierId = 1,
                                MilageLimit = true,
                                DailyPrice = 100,
                                Color = new Color()
                                {
                                    Name = "test color 1",
                                },
                                FuelType = new FuelType()
                                {
                                    Name = "test fuel type 1"
                                },
                                Transmission = true
                            },
                            new Car()
                            {
                                Id = 2,
                                SupplierId = 1,
                                MilageLimit = true,
                                DailyPrice = 100,
                                Color = new Color()
                                {
                                    Name = "test color 2",
                                },
                                FuelType = new FuelType()
                                {
                                    Name = "test fuel type 2"
                                },
                                Transmission = true
                            }
                        };
            _carDtoForAdd = new CarDtoForAdd()
            {
                ColorId = 1,
                DailyPrice = 100,
                FuelTypeId = 1,
                MilageLimit = false,
                SupplierId = 1,
                Transmission = false,
                ModelDto = new ModelDto()
                {
                    BrandId = 1,
                    ModelYear = "test year",
                    NamePrefix = "test prefix",
                    NameSuffix = "test suffix"
                }
            };
            _carDtoForUpdate = new CarDtoForUpdate()
            {
                ColorId = 1,
                DailyPrice = 100,
                FuelTypeId = 1,
                MilageLimit = false,
                SupplierId = 1,
                Transmission = false,
                Id = 1,
                ModelId = 1
            };
        }

        [Fact]
        public void GetAll_ResultIsASuccess_ReturnsOkResultWithCarData()
        {
            _mock.Setup(x => x.GetAll(null)).Returns(new SuccessDataResult<List<Car>>(_cars));

            var managerResult = _mock.Object.GetAll();
            
            var dataResult = Assert.IsType<SuccessDataResult<List<Car>>>(managerResult);
            Assert.True(dataResult.Success);
            Assert.IsAssignableFrom<List<Car>>(dataResult.Data);


            var controllerResult = _controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(controllerResult);
            var returnedCars = Assert.IsAssignableFrom<List<Car>>(okResult.Value);

            Assert.Equal<int>(2, returnedCars.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetById_IdIsValid_ReturnsOkResultWithCarData(int carId)
        {
            Car car = _cars.Single(x => x.Id == carId);
            _mock.Setup(x => x.GetById(carId)).Returns(new SuccessDataResult<Car>(car));

            var managerResult = _mock.Object.GetById(carId);
            var dataResult = Assert.IsType<SuccessDataResult<Car>>(managerResult);
            Assert.True(dataResult.Success);
            Assert.IsAssignableFrom<Car>(dataResult.Data);

            var controllerResult = _controller.GetById(carId);
            var okResult = Assert.IsType<OkObjectResult>(controllerResult);
            var returnedCar = Assert.IsAssignableFrom<Car>(okResult.Value);

            Assert.Equal<int>(carId, returnedCar.Id);
        }

        [Fact]
        public void GetById_IdIsInvalid_ReturnsBadResultWithErrorMessage()
        {
            string message = "Invalid Id";
            int id = 0;
            _mock.Setup(x => x.GetById(id)).Returns(new ErrorDataResult<Car>(message));

            var managerResult = _mock.Object.GetById(0);
            Assert.IsType<ErrorDataResult<Car>>(managerResult);
            Assert.Null(managerResult.Data);
            Assert.False(managerResult.Success);

            var controllerResult = _controller.GetById(id);
            var badResult = Assert.IsType<BadRequestObjectResult>(controllerResult);
            Assert.Equal("Invalid Id", message);
        }
        
        [Fact]
        public void Add_ResultIsSuccess_ReturnsOkResultWithSuccessStatus()
        {
            var car = _mapper.Map<Car>(_carDtoForAdd);
            car.Model = _mapper.Map<Model>(_carDtoForAdd.ModelDto);
            _mock.Setup(x => x.Add(It.IsAny<Car>())).Returns(new SuccessResult());

            var managerResult = _mock.Object.Add(car);
            Assert.IsType<SuccessResult>(managerResult);
            Assert.True(managerResult.Success);

            var controllerResult = _controller.Add(_carDtoForAdd);
            var okResult = Assert.IsType<OkObjectResult>(controllerResult);
            Assert.IsType<bool>(okResult.Value);
        }

        [Fact]
        public void Update_ResultIsSuccess_ReturnsOkResultWithSuccessStatus()
        {
            var car = _cars.Single(x => x.Id == 1);
            var carForUpdate = _mapper.Map<Car>(_carDtoForUpdate);
            _mock.Setup(x => x.Update(It.IsAny<Car>())).Callback<Car>((x) => carForUpdate.DailyPrice += 10).Returns(new SuccessResult());

            var managerResult = _mock.Object.Update(carForUpdate);
            Assert.IsType<SuccessResult>(managerResult);
            Assert.True(managerResult.Success);
            Assert.NotEqual<decimal>(carForUpdate.DailyPrice, car.DailyPrice);

            carForUpdate.DailyPrice -= 10;
            var controllerResult = _controller.Update(_carDtoForUpdate);
            var okResult = Assert.IsType<OkObjectResult>(controllerResult);
            Assert.IsType<bool>(okResult.Value);
            Assert.NotEqual<decimal>(carForUpdate.DailyPrice, car.DailyPrice);
        }
    }
}
