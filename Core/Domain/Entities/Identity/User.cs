﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }


       
    }
}

 