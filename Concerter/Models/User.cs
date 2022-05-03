using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Concerter.Models
{
    public partial class User
    {
        public User()
        {
            Events = new HashSet<Event>();
            ParticipatingArtists = new HashSet<ParticipatingArtist>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string Password { get; set; } = null!;
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<ParticipatingArtist> ParticipatingArtists { get; set; }

        public static async Task<User?> SearchAsync(string email, string password)
        {
            await using var database = new EP_02_01Context();
            var user = await database.Users
                .Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.Email == email && user.Password == password);
            return user;
        }

        public async Task SaveAsync()
        {
            await using var context = new EP_02_01Context();
            var toUpdate = (await context.Users.FirstOrDefaultAsync(u => u.Id == Id))!;
            toUpdate.Email = Email;
            toUpdate.FirstName = FirstName;
            toUpdate.LastName = LastName;
            toUpdate.MiddleName = MiddleName;
            toUpdate.Password = Password;
            toUpdate.RoleId = RoleId;
            context.Users.Update(toUpdate);
            await context.SaveChangesAsync();
        }
        
        public static IEnumerable<User> GetImpresario()
        {
            const int impresarioRoleId = 3;
            using var context = new EP_02_01Context();
            return context.Users
                .Where(user => user.RoleId == impresarioRoleId)
                .ToArray();
        }

        public static IEnumerable<User> GetUsers(User excludeUser)
        {
            using var context = new EP_02_01Context();
            return context.Users
                .Include(user => user.Role)
                .Where(user => user.Id != excludeUser.Id)
                .ToArray();
        }

        public async void Register()
        {
            await using var context = new EP_02_01Context();
            context.Add(this);
            await context.SaveChangesAsync();
        }
    }
}