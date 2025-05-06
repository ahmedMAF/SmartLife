using Microsoft.EntityFrameworkCore;
using SmartLife.Models;
using System.Net;
using System.Text.Json;

namespace SmartLife.Utilities;

public static class ContactHelper
{
    public static async Task<Contact> GetContactByIpAsync(SmartLifeDb context, HttpContext ctx)
    {
        string? country = ctx.Session.GetString("country");
        
        if (string.IsNullOrEmpty(country))
        {
            IPAddress? ip = ctx.Connection.RemoteIpAddress;
            HttpClient client = new();

            try
            {
                HttpResponseMessage response = await client.GetAsync($"http://ip-api.com/json/{ip}?fields=16386");
                IpApiResponse c = JsonSerializer.Deserialize<IpApiResponse>(await response.Content.ReadAsStringAsync());

                country = c.Success ? c.CountryCode : "EG";
            }
            catch (System.Exception)
            {
                country = "EG";
            }
            finally
            {
                client.Dispose();
            }
            
            ctx.Session.SetString("country", country);
        }

        return await context.Contacts.FirstOrDefaultAsync(m => m.Country == country);
    }

    private struct IpApiResponse
    {
        public bool Success;
        public string CountryCode;
    }
}
