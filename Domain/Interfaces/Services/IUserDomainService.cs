﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUserDomainService
    {
        public void UpdateUser(User user, User updateInfo);

        public Task EnsureUserNotExists(string userName, string email);

        public void EnsurePasswordCanBeChanged(User user, string oldPassword, string newPassword);

        public void ChangePassword(User user, string newPassword);

    }
}