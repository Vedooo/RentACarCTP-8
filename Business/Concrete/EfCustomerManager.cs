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
    public class EfCustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public EfCustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            IResult result = BusinessRules.Run(CheckIfCustomerAlreadyExists(customer.CustomerId), CheckIfCompanyAlreadyExists(customer.CompanyName));
            if (result != null)
            {
                return new ErrorResult();
            }
            _customerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }

        [SecuredOperation("admin,modaretor")]
        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);
        }

        [CacheAspect]
        [PerformanceAspect(20)]
        public IDataResult<List<Customer>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Customer>>(Messages.MaintainanceTimeCustomer);
            }
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(),Messages.CustomersListed);
        }

        [CacheAspect]
        [PerformanceAspect(20)]
        public IDataResult<List<Customer>> GetByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(cu => cu.CustomerId == customerId), Messages.CustomersListed);
        }

        public IResult Update(Customer customer)
        {
            IResult result = BusinessRules.Run(CheckIfCustomerAlreadyExists(customer.CustomerId), CheckIfCompanyAlreadyExists(customer.CompanyName));
            if (result != null)
            {
                return new ErrorResult();
            }
            _customerDal.Update(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }

        //////////////////// Business Rules //////////////////////////////

        private IResult CheckIfCustomerAlreadyExists(int customerId)
        {
            var result = _customerDal.GetAll(cu => cu.CustomerId == customerId).Any();
            if (result == true)
            {
                return new ErrorResult(Messages.CustomerAlreadyExists);
            }
            return new SuccessResult();
        }


        private IResult CheckIfCompanyAlreadyExists(string companyName)
        {
            var result = _customerDal.GetAll(cu => cu.CompanyName == companyName).Any();
            if (result == true)
            {
                return new ErrorResult(Messages.CustomerAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
