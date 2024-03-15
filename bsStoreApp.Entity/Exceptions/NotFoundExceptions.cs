using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Entity.Exceptions
{
    public abstract class NotFoundExceptions : Exception
    {
        protected NotFoundExceptions(string message) : base(message)
        {
            
        }
    }
}
    