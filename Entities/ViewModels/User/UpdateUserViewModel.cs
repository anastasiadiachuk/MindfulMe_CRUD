﻿using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels.User
{
    public class UpdateUserViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public UserType UserType { get; set; }
    }
}
