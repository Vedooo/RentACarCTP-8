using Core.DataAccess.EntityFramework;
using Core.Entity.Concrete;
using DataAccess.Abstract;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, CarContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (CarContext context = new CarContext())
            {
                var result = from Op in context.OperationClaims
                             join UserOp in context.UserOperationClaims
                             on Op.Id equals UserOp.OperationClaimId
                             where UserOp.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = Op.Id,
                                 Name = Op.Name
                             };
                return result.ToList();
            }
        }
    }
}
