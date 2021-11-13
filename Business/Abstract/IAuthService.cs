using Core.Entity.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IResult UserExists(string email);

        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
