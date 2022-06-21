using Microsoft.EntityFrameworkCore;
using Prolunteer.BusinessLogic.Base;
using Prolunteer.BusinessLogic.Implementation.Account.Models;
using Prolunteer.BusinessLogic.Implementation.Account.Utilities;
using Prolunteer.BusinessLogic.Implementation.Account.Validations;
using Prolunteer.Common.DTOs;
using Prolunteer.Common.Extensions;
using Prolunteer.Entities;
using Prolunteer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.Account
{
    public class UserAccountService : BaseService
    {
        private readonly RegisterModelValidator RegisterModelValidator;
        private readonly ChangePasswordModelValidator ChangePasswordModelValidator;
        public UserAccountService(ServiceDependencies serviceDependencies)
            : base(serviceDependencies)
        {
            this.RegisterModelValidator = new RegisterModelValidator(uow);
            this.ChangePasswordModelValidator = new ChangePasswordModelValidator(
                uow.Users.Get()
                .Where(u => u.Id == CurrentUser.Id)
                .Select(u => u.PasswordHash)
                .FirstOrDefault());
        }

        public CurrentUserDTO Register(RegisterModel model)
        {
            CurrentUserDTO UserDto = new CurrentUserDTO
            {
                IsAuthenticated = false
            };
            var user = ExecuteInTransaction(uow =>
            {
                RegisterModelValidator.Validate(model).ThenThrow();

                var user = Mapper.Map<RegisterModel, Entities.User>(model);

                uow.Users.Insert(user);

                uow.SaveChanges();

                UserDto = new CurrentUserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsAuthenticated = true,
                    Roles = user.UserRoles.Select(ur => ur.RoleId).ToList(),
                    Certifications = new List<int>()
                };

                return user;
            });

            Task.Run(() => NotificationManager.SendRegistrationNotification(user));

            return UserDto;
        }

        public PaginationDTO<UserViewModel> GetUsers(int pageNumber, int pageSize, string filter)
        {
            var users = uow.Users.Get()
                .Where(u => !u.IsDeleted)
                .Where(u => !u.UserRoles.Any(ur => ur.RoleId == (int)RoleTypes.Admin));
            if (!string.IsNullOrWhiteSpace(filter))
            {
                users = users.Where(u => u.FirstName.Contains(filter) || u.LastName.Contains(filter) || u.EMail.Contains(filter));
            }
            var elements = users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(u => u.UserRoles)
                .Select(u => Mapper.Map<UserViewModel>(u))
                .ToList();
            var count = users.Count();

            return new PaginationDTO<UserViewModel>(elements, count);
        }

        public bool RemoveUser(Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                var userToRemove = uow.Users.Get().Where(u => u.Id == id).SingleOrDefault();

                if(userToRemove == null)
                {
                    return false;
                }

                userToRemove.IsDeleted = true;

                uow.Users.Update(userToRemove);
                uow.SaveChanges();

                return true;
            });
        }

        public CurrentUserDTO Login(string email, string password)
        {
            var user = uow.Users.Get()
                .Where(u => u.EMail == email && !u.IsDeleted)
                .Include(u => u.UserRoles)
                .Include(u => u.UserCertifications)
                .FirstOrDefault();

            if (user == null)
            {
                return new CurrentUserDTO { IsAuthenticated = false };
            }

            var passwordHash = user.PasswordHash;

            if (!PasswordUtilities.CheckPassword(password, passwordHash))
            {
                return new CurrentUserDTO { IsAuthenticated = false };
            }


            return new CurrentUserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAuthenticated = true,
                Roles = user.UserRoles.Select(ur => ur.RoleId).ToList(),
                Certifications = user.UserCertifications.Select(uc => uc.CertificationId).ToList()
            };
        }

        public void ChangePassword(ChangePasswordModel model)
        {
            ExecuteInTransaction(uow =>
            {
                ChangePasswordModelValidator.Validate(model).ThenThrow();

                var newPasswordHash = PasswordUtilities.Hash(model.NewPassword);

                var currentUserEntity = uow.Users.Get().FirstOrDefault(u => u.Id == CurrentUser.Id);

                currentUserEntity.PasswordHash = newPasswordHash;

                uow.Users.Update(currentUserEntity);
                uow.SaveChanges();
            });
        }

        public Entities.User GetUser(Guid id)
        {
            return uow.Users
                .Get()
                .Where(u => u.Id == id)
                .FirstOrDefault();
        }
        public IQueryable<Entities.User> GetUserQueryable() {
            return uow.Users.Get();
        }
        public List<ListItemModel<int, string>> GetRolesAsListItemModelList()
        {
            return uow.Roles.Get()
                .Where(r => r.Id != (int)RoleTypes.Admin)
                .Select(r => new ListItemModel<int, string>
                {
                    Text = r.Name,
                    Value = r.Id
                }).ToList();
        }
    }
}
