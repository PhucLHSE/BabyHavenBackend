using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Repositories.Models;

namespace BabyHaven.Services.IServices
{
    public interface IJwtTokenService
    {
        string GenerateJSONWebToken(UserAccount user);
    }
}
