using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.UnAuthorized
{
    public class UnAuthorizedExeption() : Exception("You are not authorized to perform this action.")
    {
    }
}
