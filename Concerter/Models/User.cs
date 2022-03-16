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

        public static User? Search(string email, string password)
        {
            using var database = new EP_02_01Context();
            var user = database.Users.FirstOrDefault(user => user.Email == email && user.Password == password);
            return user;
        }

        public static async Task<User?> SearchAsync(string email, string password)
        {
            await using var database = new EP_02_01Context();
            var user = await database.Users.FirstOrDefaultAsync(
                user => user.Email == email && user.Password == password);
            return user;
        }

        public async Task SaveAsync()
        {
            await using var context = new EP_02_01Context();
            context.Users.Update(this);
            await context.SaveChangesAsync();
        }
    }
}