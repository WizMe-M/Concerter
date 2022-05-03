using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Concerter.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }

        public static IEnumerable<Genre> GetGenres()
        {
            using var context = new EP_02_01Context();
            return context.Genres.ToArray();
        }

        public static bool Exists(string name)
        {
            using var context = new EP_02_01Context();
            return context.Genres.FirstOrDefault(genre => genre.Name == name) is not null;
        }

        public static void Add(string name)
        {
            using var context = new EP_02_01Context();
            var genre = new Genre { Name = name };
            context.Genres.Add(genre);
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await using var context = new EP_02_01Context();
            var genre = (await context.Genres.FirstOrDefaultAsync(g => g.Id == Id))!;
            genre.Name = Name;
            context.Genres.Update(genre);
            await context.SaveChangesAsync();
        }

        public static void Delete(int id)
        {
            using var context = new EP_02_01Context();
            var genre = context.Genres.FirstOrDefault(g => g.Id == id)!;
            context.Genres.Remove(genre);
            context.SaveChanges();
        }
    }
}