﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Models.Responses
{
    public class AccountResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
