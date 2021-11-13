using Core.Utilities.Results;
using Entity.Concrete;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);

        IResult IsCarEverRented(int carId);
        IResult IsCarReturned(int carId);
        IResult IsCarAvaible(int carId);

        IDataResult<Rental> GetByRentalId(int rentId);
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<CarRentDetailDto>> GetCarDetail();
        IDataResult<List<CarRentDetailDto>> GetCarDetailById(int carId);
    }
}
