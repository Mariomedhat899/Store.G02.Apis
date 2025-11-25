using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.NotFound
{
    public class UserNotFoundExeption(string email) : NotFoundExeption($"User With Email : {email} Was Not Found !!")
    {
    }
}
