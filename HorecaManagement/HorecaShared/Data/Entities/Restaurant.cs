﻿using Horeca.Shared.Data.Entities.Account;

namespace Horeca.Shared.Data.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }

        public List<ApplicationUser> Employees { get; set; } = new();
    }
}