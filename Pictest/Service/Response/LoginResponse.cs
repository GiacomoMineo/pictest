﻿using Pictest.Model;

namespace Pictest.Service.Response
{
    public class LoginResponse
    {
        public User User{ get; set; }
        public string Token { get; set; }
    }
}
