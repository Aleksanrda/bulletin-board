using BulletinBoard.Api.Accounts.DTO;
using BulletinBoard.Core.Entities;
using BulletinBoard.Core.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;


namespace BulletinBoard.Api.Accounts
{
    public class AccountsService : IAccountsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserStore<User> userStore;
        private readonly RoleStore<Role> roleStore;
        private UserManager<User> userManager;
        private RoleManager<Role> roleManager;

        public AccountsService(IUnitOfWork unitOfWork,
            UserStore<User> userStore, RoleStore<Role> roleStore)
        {
            this.unitOfWork = unitOfWork;
            this.userStore = userStore;
            this.roleStore = roleStore;
            userManager = new UserManager<User>(userStore);
            roleManager = new RoleManager<Role>(roleStore);
        }

        public async Task<IdentityResult> Register(PostRegisterDTO model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            User user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                BirthDate = model.BirthDate,
                Bio = model.Bio,
                TypeUser = model.TypeUser
            };

            var result = await userManager.CreateAsync(user, model.Password);

            return result;
        }

        public async Task<User> Login(PostLoginDTO model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var user = await userManager.FindAsync(model.UserName, model.Password);

            return user;
        }
    }
}
