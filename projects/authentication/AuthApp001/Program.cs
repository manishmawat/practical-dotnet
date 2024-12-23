using Microsoft.Extensions.DependencyInjection;

namespace AuthApp001
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication()
                .AddCookie(Settings.AuthCookieName,
                    options =>
                    {
                        options.Cookie.Name = Settings.AuthCookieName;
                        options.LoginPath = "/Auth/Login";
                        options.AccessDeniedPath = "/Home/Forbidden";
                    });

            builder.Services.AddAuthorization(
                options => { options.AddPolicy("Admin", policy => { policy.RequireClaim("admin", "true"); }); }
            );
            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");


            app.Run();
        }
    }
}