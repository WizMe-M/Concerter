using System.Collections.Generic;
using System.IO;
using System.Linq;
using Concerter.Models;
using OfficeOpenXml;

namespace Concerter.Export;

public class ExcelExporter
{
    private const string Path = @"C:\Users\ender\OneDrive\Рабочий стол\Sells.xlsx";

    public ExcelExporter()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public byte[] Generate(IEnumerable<Ticket> tickets)
    {
        var sells = tickets
            .GroupBy(ticket => ticket.Event)
            .OrderBy(group => group.Key.Date)
            .ThenBy(group => group.Key.Name)
            .Select(group => new { Event = group.Key, Tickets = group.ToArray()})
            .ToArray();
        var lastRow = sells.Length + 1;
        var total = 0m;
        
        var package = new ExcelPackage();
        var sheet = package.Workbook.Worksheets.Add("Продажи");

        sheet.Cells["A1"].Value = "№ пп";
        sheet.Cells["B1"].Value = "Дата";
        sheet.Cells["C1"].Value = "Мероприятие";
        sheet.Cells["D1"].Value = "Стоимость";
        sheet.Cells["E1"].Value = "Продано билетов";
        sheet.Cells["F1"].Value = "Выручка";

        for (var i = 1; i <= sells.Length; i++)
        {
            var sell = sells[i - 1];
            sheet.Cells[i + 1, 1].Value = i;
            sheet.Cells[i + 1, 2].Value = sell.Event.Date;
            sheet.Cells[i + 1, 3].Value = sell.Event.Name;
            sheet.Cells[i + 1, 4].Value = sell.Event.Cost;
            var ticketSold = sell.Tickets.Sum(ticket => ticket.Amount);
            sheet.Cells[i + 1, 5].Value = ticketSold;
            var totalSold = sell.Tickets.Sum(ticket => ticket.Amount * sell.Event.Cost);
            sheet.Cells[i + 1, 6].Value = totalSold;
            total += totalSold;
        }

        sheet.Cells[lastRow + 1, 5].Value = "ИТОГО:";
        sheet.Cells[lastRow + 1, 6].Value = total;
        sheet.Cells[1, 1, lastRow, 6].AutoFitColumns();
        return package.GetAsByteArray();
    }

    public void Export(IEnumerable<Ticket> tickets)
    {
        var data = Generate(tickets);
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }
        File.WriteAllBytes(Path, data); 
    }
}