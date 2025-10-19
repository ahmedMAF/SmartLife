using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartLife.Data;
using SmartLife.Services;
using SmartLife.Utilities;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace SmartLife;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        MySqlServerVersion serverVersion = new(new Version(8, 0, 29));

#if DEBUG
        string connectionString = builder.Configuration.GetConnectionString("DevConnection");
#else
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(80);
            options.ListenAnyIP(443, listenOptions =>
            {
                listenOptions.UseHttps(httpsOptions =>
                {
                    httpsOptions.ServerCertificateSelector = (connectionContext, hostName) =>
                    {
                        if (hostName == "smartlifeeg.com" || hostName == "www.smartlifeeg.com")
                            return new X509Certificate2("/opt/SmartLife/certs/eg.pfx", "SmartLife@123");
                        else if (hostName == "smartlifeae.com" || hostName == "www.smartlifeae.com")
                            return new X509Certificate2("/opt/SmartLife/certs/ae.pfx", "SmartLife@123");

                        return null;
                    };
                });
            });
        });
#endif

        // Add services to the container.
        builder.Services.AddSession();
        builder.Services.AddRazorPages(options =>
        {
            options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
        });

        builder.Services.AddAuthentication("SLCookieAuth")
            .AddCookie("SLCookieAuth", options =>
            {
                options.LoginPath = "/Admin/Login";
                options.LogoutPath = "/Admin/Logout";
            });

        builder.Services.AddDbContext<SmartLifeDb>(options =>
        {
            options.UseMySql(connectionString, serverVersion, options =>
                options.UseMicrosoftJson(MySqlCommonJsonChangeTrackingOptions.FullHierarchyOptimizedFast));
#if DEBUG
            options.LogTo(Console.WriteLine, LogLevel.Error)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
#endif
        });

        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            string[] supportedCultures = ["en", "ar"];
            options.SetDefaultCulture("en");
            options.AddSupportedCultures(supportedCultures);
            options.AddSupportedUICultures(supportedCultures);
        });

        builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
        builder.Services.AddTransient<EmailService>();

        WebApplication app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<SmartLifeDb>();

            try
            {
                dbContext.Database.Migrate(); // This applies all pending migrations
            }
            catch
            {
            }
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios,
            // see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseSession();
        app.UseHttpsRedirection();
        app.UseStaticFiles(
#if DEBUG
            new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store");
                    ctx.Context.Response.Headers.Append("Pragma", "no-cache");
                    ctx.Context.Response.Headers.Append("Expires", "0");
                }
            }
#endif
        );
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

        app.MapRazorPages();
        MinRoutes.Map(app);

        FileHelper.Environment = app.Environment;
        app.Run();
    }
}
