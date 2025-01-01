﻿using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}