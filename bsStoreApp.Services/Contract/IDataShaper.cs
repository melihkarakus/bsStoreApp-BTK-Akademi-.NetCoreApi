using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Services.Contract
{
    public interface IDataShaper<T>
    {
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString); //Liste türünde
        ExpandoObject ShapeData(T entity, string fieldsString); //Tek veri türünde
    }
}
