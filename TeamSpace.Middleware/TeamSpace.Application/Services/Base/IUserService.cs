using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamSpace.Application.Services.Base;
public interface IUserService
{
    Task<bool> RegisterUserAsync(string username, string password, string email);
}
