using Microsoft.AspNetCore.Authentication;
using SmartLife.Models;
using Microsoft.AspNetCore.Localization;

namespace SmartLife;

internal class MinRoutes
{
    internal static void Map(WebApplication app)
    {
        MapMiscRoutes(app);
        MapDeleteRoutes(app);
    }

    private static void MapMiscRoutes(WebApplication app)
    {
        app.MapGet("/SetLanguage", (HttpContext context, string returnUrl) =>
        {
            bool isAr = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");

            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(isAr ? "en" : "ar")),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Results.Redirect(returnUrl);
        });

        app.MapGet("/Admin/Logout", async (HttpContext ctx) =>
            {
                await ctx.SignOutAsync("SLCookieAuth");
                return Results.Redirect("/Index");
            }
        );
    }

    private static void MapDeleteRoutes(WebApplication app)
    {
        app.MapPost("/Partners/Delete/{id}", async (int id, SmartLifeDb ctx) =>
            {
                ctx.PartnersClients.Remove(new PartnerClient { Id = id });
                await ctx.SaveChangesAsync();

                return Results.Redirect("/Partners/Index");
            }
        ).RequireAuthorization();

        app.MapPost("/Clients/Delete/{id}", async (int id, SmartLifeDb ctx) =>
            {
                ctx.PartnersClients.Remove(new PartnerClient { Id = id });
                await ctx.SaveChangesAsync();

                return Results.Redirect("/Clients/Index");
            }
        ).RequireAuthorization();

        app.MapPost("/News/Delete/{id}", async (int id, SmartLifeDb ctx) =>
            {
                ctx.News.Remove(new Post { Id = id });
                await ctx.SaveChangesAsync();

                return Results.Redirect("/News/Index");
            }
        ).RequireAuthorization();

        app.MapPost("/Products/Delete/{id}", async (int id, SmartLifeDb ctx) =>
            {
                ctx.Products.Remove(new Product { Id = id });
                await ctx.SaveChangesAsync();

                return Results.Redirect("/Products/Index");
            }
        ).RequireAuthorization();

        app.MapPost("/Team/Delete/{id}", async (int id, SmartLifeDb ctx) =>
            {
                ctx.Team.Remove(new TeamMember { Id = id });
                await ctx.SaveChangesAsync();

                return Results.Redirect("/Team/Index");
            }
        ).RequireAuthorization();

        app.MapPost("/Contacts/Delete/{id}", async (string id, SmartLifeDb ctx) =>
            {
                ctx.Contacts.Remove(new Contact { Country = id });
                await ctx.SaveChangesAsync();

                return Results.Redirect("/Admin/EditContacts");
            }
        ).RequireAuthorization();
    }
}