using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Modules;
using Seabattle.Data.Contexts;
using Seabattle.Logic.Profiles;
using Seabattle.Logic.Services;
using Seabattle.Logic.Services.Contracts;
using System.Collections.Generic;

namespace Seabattle.Logic
{
    public class SeabattleDIModule : NinjectModule
    {
        public override void Load()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>
            {
                new UserProfile(),
                new GameProfile(),
                new AreaProfile(),
                new ShipProfile(),
                new CellProfile(),
                new ShipProfile()
            }));
            var mapper = configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper);
            this.Bind<SeabattleContext>().ToSelf();


            this.Bind<IUserStore<IdentityUser>>().ToMethod(ctx => new UserStore<IdentityUser>(ctx.Kernel.Get<SeabattleContext>()));
            this.Bind<UserManager<IdentityUser>>().ToMethod(ctx =>
            {
                var manager = new UserManager<IdentityUser>(ctx.Kernel.Get<IUserStore<IdentityUser>>());
                manager.UserValidator = new UserValidator<IdentityUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };
                manager.PasswordValidator = new PasswordValidator()
                {
                    RequireDigit = false,
                    RequiredLength = 3,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false,
                    RequireUppercase = false
                };

                return manager;
            });

            this.Bind<IUserService>().To<UserService>();
            this.Bind<IGameService>().To<GameServise>();
        }
    }
}
