using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants.Message;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Results.DataResultOptions.DataOptions;
using Core.Utilities.Results.ResultOptions.Options;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class EfCarManager : ICarService
    {
        ICarDal _carDal;

        public EfCarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("admin,moderator")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [SecuredOperation("admin,moderator")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintainanceTimeCar);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        [CacheAspect]
        [PerformanceAspect(20)]
        public IDataResult<List<Car>> GetByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId), Messages.CarsListedByBrandId);
        }

        [CacheAspect]
        [PerformanceAspect(20)]
        public IDataResult<List<Car>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.CarId == carId), Messages.CarsListedByCarId);
        }

        [CacheAspect]
        [PerformanceAspect(20)]
        public IDataResult<List<Car>> GetByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId), Messages.CarsListedByColorId);
        }

        [CacheAspect]
        [PerformanceAspect(20)]
        public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c =>( c.DailyPrice >= min && c.DailyPrice <= max)),Messages.CarsListedByDailyPrice);
        }

        [CacheAspect]
        [PerformanceAspect(20)]
        public IDataResult<List<CarDetailDto>> GetCarDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetails(),Messages.CarDetailsListed);
        }

        [SecuredOperation("admin,moderator")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            IResult result = BusinessRules.Run(CheckCarIdExists(car.CarId));
            if (result != null)
            {
                return new ErrorResult();
            }
            _carDal.Update(car);
            return new SuccessResult(Messages.CarInfoUpdated);
        }


        //////////////////// Business Rules //////////////////////////////


        private IResult CheckCarIdExists(int carId)
        {
            var result = _carDal.GetAll(c => c.CarId == carId).Count();
            if (result != null)
            {
                return new ErrorResult(Messages.CarIsAlreadyExist);
            }
            return new SuccessResult();
        }

    }
}
