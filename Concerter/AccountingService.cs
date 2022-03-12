using System;
using System.Linq;
using System.Threading.Tasks;
using Concerter.Models;
using Microsoft.EntityFrameworkCore;

namespace Concerter;

public class AccountingService
{
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