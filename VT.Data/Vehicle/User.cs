using System;
using Microsoft.AspNetCore.Identity;

namespace VT.Data.Vehicle
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public User()
        {
            Id = new Guid();
            CreatedDate = DateTime.UtcNow;
        }
    }
}
