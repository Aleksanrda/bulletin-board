using BulletinBoard.Api.Accounts.DTO;
using BulletinBoard.Core.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Api.Accounts
{
    public interface IAccountsService
    {
        Task<IdentityResult> Register(PostRegisterDTO model);

        Task<User> Login(PostLoginDTO model);
    }
}
