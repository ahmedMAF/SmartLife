using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using SmartLife.Models;

namespace SmartLife;

internal class MinRoutes
{
    internal static void Map(WebApplication app)
    {
        app.MapGet("/Admin/Logout", async (HttpContext ctx) =>
            {
                await ctx.SignOutAsync("SLCookieAuth");
                return Results.Redirect("/Index");
            }
        ).RequireAuthorization();

        app.MapPost("/Partners/Delete/{id}", async (int id, SmartLifeDb ctx) =>
            {
                Console.WriteLine("SMD: " + id);
                ctx.PartnersClients.Remove(new PartnerClient { Id = id });
                await ctx.SaveChangesAsync();

                return Results.Redirect("/Partners/Index");
            }
        ).RequireAuthorization();

        
    }
}