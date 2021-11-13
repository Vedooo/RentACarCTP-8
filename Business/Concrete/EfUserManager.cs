using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Entity.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class EfUserManager : IUserService
    {
        IUserDal _userDal;

        public EfUserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        [CacheAspect]
        [PerformanceAspect(20)]
        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        [CacheAspect]
        [PerformanceAspect(20)]
        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }
    }
}
