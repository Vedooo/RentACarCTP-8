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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class EfBrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public EfBrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [SecuredOperation("admin,moderator")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandNameAlreadyExists(brand.BrandName), CheckBrandExist(brand.BrandId));
            if (result != null)
            {
                return new ErrorResult(Messages.BrandAlreadyExists);
            }
            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdded);
        }

        [SecuredOperation("admin,moderator")]
        public IResult Delete(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckBrandExist(brand.BrandId));
            if (result != null)
            {
                return new ErrorResult();
            }
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }

        [CacheAspect]
        [PerformanceAspect(20)]
        public IDataResult<List<Brand>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Brand>>(Messages.MaintainanceTimeBrand);
            }
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.BrandsListed);
        }

        [CacheAspect]
        [PerformanceAspect(20)]
        public IDataResult<List<Brand>> GetByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(b => b.BrandId == brandId), Messages.BrandsListedById);
        }

        [ValidationAspect(typeof(BrandValidator))]
        [SecuredOperation("admin,moderator")]
        public IResult Update(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandNameAlreadyExists(brand.BrandName));
            if (result != null)
            {
                return new ErrorResult(Messages.BrandAlreadyExists);
            }
            _brandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }

        //////////////////// Business Rules //////////////////////////////


        private IResult CheckIfBrandNameAlreadyExists(string brandName)
        {
            var result = _brandDal.GetAll(b => b.BrandName == brandName).Any();
            if (result == true)
            {
                return new SuccessResult(Messages.BrandAlreadyExists);
            }
            return new ErrorResult();
        }

        private IResult CheckBrandExist(int brandId)
        {
            var result = _brandDal.GetAll(b => b.BrandId == brandId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.InvalidBrandName);
            }
            return new SuccessResult();
        }
    }
}
