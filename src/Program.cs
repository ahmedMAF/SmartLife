using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SmartLife;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        MySqlServerVersion serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Add services to the container.
        builder.Services.AddRazorPages();
        //builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<MySqlDb>(options => {
            options.UseMySql(connectionString, serverVersion, options => options.UseMicrosoftJson());
#if DEBUG
            options.LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
#endif
        });

        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            string[] supportedCultures = [ "en", "ar" ];
            options.SetDefaultCulture("en");
            options.AddSupportedCultures(supportedCultures);
            options.AddSupportedUICultures(supportedCultures);
        });

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // For controllers/routes attributes
        app.UseRouting();
        // Direct
        MapRoutes(app);

        app.UseAuthorization();

        app.MapRazorPages();

        app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

        app.Run();
    }

    private static void MapRoutes(WebApplication app)
    {
        //app.MapGet("", () => "");
    }
}