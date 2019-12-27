using Microsoft.AspNetCore.Identity;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowthTrigal.Common.Models
{
    public class UserResponse : IdentityUser
    {
        [PrimaryKey]
        public string Document { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }
}
