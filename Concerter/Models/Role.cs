using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public string AlterName
        {
            get
            {
                return Id switch
                {
                    1 => "Кассир",
                    2 => "Артист",
                    3 => "Импресарио",
                    4 => "Организатор"
                };
            }
        }

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

        public static IEnumerable<Role> GetRoles()
        {
            using var context = new EP_02_01Context();
            context.Roles.Load();
            return context.Roles.ToArray();
        }
    }
}
