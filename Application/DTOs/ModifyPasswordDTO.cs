﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ModifyPasswordDTO
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
