using System.Collections.Generic;
using System.Threading.Tasks;
using Concerter.Models;
using Microsoft.EntityFrameworkCore;

namespace Concerter;

public class EventManagerService
{
    public async Task<IEnumerable<Event>> GetEventsAsync()
    {
        await using var context = new EP_02_01Context();
        await context.Events.LoadAsync();
        return await context.Events.ToListAsync();
    }
}