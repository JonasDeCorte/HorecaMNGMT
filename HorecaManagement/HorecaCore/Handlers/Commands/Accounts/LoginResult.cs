﻿namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class LoginResult
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}