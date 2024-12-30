using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamSpace.Application.Services.Base;
public interface IUserService
{
   Task<bool> CreateUser(string username, string email, string password, string PhoneNumber,Guid roleId);

    Task<string> LoginUser(string username, string password);
}
