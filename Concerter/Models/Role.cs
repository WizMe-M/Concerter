using System;
using System.Collections.Generic;

namespace Concerter.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
        
        public static RoleAccess AuthorizeUser(int? roleId)
        {
            return roleId switch
            {
                1 => RoleAccess.Cashier,
                2 => RoleAccess.Artist,
                3 => RoleAccess.Impresario,
                4 => RoleAccess.Organizer,
                _ => throw new ArgumentOutOfRangeException($"User's role's id ({roleId}) was outside range [1,4]")
            };
        }
    }
}
