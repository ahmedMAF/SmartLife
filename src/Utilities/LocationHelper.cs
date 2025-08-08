using SmartLife.Models;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace SmartLife.Utilities;

public static class LocationHelper
{
    private static readonly JsonSerializerOptions _options = new() 
    { 
        PropertyNameCaseInsensitive = true
    };

    public static List<(string Code, string Name)> GetCountries()
    {
        var countries = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .Select(c => new RegionInfo(c.Name))
            .GroupBy(r => r.TwoLetterISORegionName)
            .Select(g => g.First())
            .OrderBy(r => r.EnglishName);

        return [.. countries.Select(r => (r.TwoLetterISORegionName, r.EnglishName))];
    }

    public static string GetCountryNameBy2LetterISO(string isoCode)
    {
		try
		{
			var region = new RegionInfo(isoCode.ToUpper());
			return region.EnglishName;
		}
		catch (Exception)
		{
			return "";
		}
    }

    public static async Task<Contact> GetContactByIpAsync(SmartLifeDb context, HttpContext ctx)
    {
        string? country = ctx.Session.GetString("country");
        
        if (string.IsNullOrEmpty(country))
        {
            IPAddress? ip = ctx.Connection.RemoteIpAddress;
            HttpClient client = new();

            try
            {
                string requestUri = $"http://ip-api.com/json/{ip}?fields=16386";
                HttpResponseMessage response = await client.GetAsync(requestUri);
                string json = await response.Content.ReadAsStringAsync();
                IpApiResponse c = JsonSerializer.Deserialize<IpApiResponse>(json, _options);

                country = c.Status == "success" ? c.CountryCode : "EG";
            }
            catch (Exception)
            {
                country = "EG";
            }
            finally
            {
                client.Dispose();
            }
            
            ctx.Session.SetString("country", country);
        }

        return await context.Contacts.FindAsync(country);
    }
    
    public static async Task<string> GetContactByIpTestAsync(SmartLifeDb context, HttpContext ctx)
    {
        string? country = ctx.Session.GetString("country");
        
        if (string.IsNullOrEmpty(country))
        {
            IPAddress? ip = ctx.Connection.RemoteIpAddress;
            HttpClient client = new();

            try
            {
                string requestUri = $"http://ip-api.com/json/{ip}?fields=16386";
                HttpResponseMessage response = await client.GetAsync(requestUri);
                string json = await response.Content.ReadAsStringAsync();
                IpApiResponse c = JsonSerializer.Deserialize<IpApiResponse>(json, _options);

                country = c.Status == "success" ? c.CountryCode : "EG";
            }
            catch (Exception)
            {
                country = "EG";
            }
            finally
            {
                client.Dispose();
            }
            
            // ctx.Session.SetString("country", country);
        }

        return country;
    }

    private struct IpApiResponse
    {
        public string Status { get; set; }
        public string CountryCode { get; set; }
    }
}
