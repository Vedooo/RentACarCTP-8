using Core.Utilities.Results;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IColorService
    {
        IResult Add(Color color);
        IResult Update(Color color);
        IResult Delete(Color color);

        IDataResult<List<Color>> GetAll();
        IDataResult<List<Color>> GetByColorId(int colorId);
    }
}
