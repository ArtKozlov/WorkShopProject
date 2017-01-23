using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todoclient.Interfaces
{
    public interface IUserService
    {
        int CreateUser(string userName);
        int GetOrCreateUser();
    }
}
