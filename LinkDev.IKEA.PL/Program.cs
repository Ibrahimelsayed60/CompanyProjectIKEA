using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using LinkDev.IKEA.PL.Mapping;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace LinkDev.IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure services 
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(

                optionsAction: (optionsBuilder) => 
                {

                    optionsBuilder.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                }
                );




            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddTransient<IAttachmentService, AttachmentService>();


            builder.Services.AddAutoMapper(M => M.AddProfile( new MappingProfile()));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "Account/Login";
                    options.AccessDeniedPath = "Home/Error";
                });

            #endregion

            var app = builder.Build();

            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            #endregion
            app.Run();
        }
    }
}
