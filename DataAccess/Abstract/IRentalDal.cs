using Core.DataAccess;
using Entity.Concrete;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
        List<CarRentDetailDto> GetCarsDetails();
        List<CarRentDetailDto> GetCarsDetailsById(int carId);
    }
}
