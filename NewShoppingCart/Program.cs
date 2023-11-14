using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewShoppingCart.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace NewShoppingCart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<NewShoppingCartContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("NewShoppingCartContext") ?? throw new InvalidOperationException("Connection string 'NewShoppingCartContext' not found.")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<NewShoppingCartContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            builder.Services.AddSession();
           
            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //.AddRoles<IdentityRole>() // 添加对角色的支持
            //.AddEntityFrameworkStores<NewShoppingCartContext>();

            // 添加自定义用户服务
            builder.Services.AddScoped<IUserService, UserService>();

            #region 配置鉴权
            {
                //选择那种方式来鉴权
                builder.Services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
                {
                    option.LoginPath = "/Manage/Login";//如果没有用户信息就跳---鉴权失败--授权就也失败---跳转到指定的Action
                    option.AccessDeniedPath = "/Home/NoAuthority";
                });
            }
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();//鉴权
            app.UseAuthorization();
            //app.MapGet("/", context =>
            //{
            //    context.Response.Redirect("/Identity/Account/Login");
            //    return Task.CompletedTask;
            //});

            app.MapControllerRoute(

                name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");
     //pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}");
            app.MapRazorPages();
            app.Run();
        }
    }
}