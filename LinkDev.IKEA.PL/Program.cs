using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
                //contextLifetime: ServiceLifetime.Scoped,
                //optionsLifetime: ServiceLifetime.Scoped,
                optionsAction: (optionsBuilder) => 
                {
                    //optionsBuilder.UseSqlServer("Server = .; Database = IKEA_G03; Trusted_Connection = True; TrustServerCertificate = True;");
                    optionsBuilder.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                    //optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
                );


            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            #region I replaced the following code by AddDbContext in the previous code
            //builder.Services.AddScoped<ApplicationDbContext>();
            //builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>((ServiceProvider) =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            //    optionsBuilder.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);

            //    //using var scope = ServiceProvider.CreateScope();

            //    //var departmentRepo = scope.ServiceProvider.GetService<IDepartmentRepository>();

            //    var option = optionsBuilder.Options;

            //    return option;
            //});
            #endregion

            #endregion

            var app = builder.Build();

            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion
            app.Run();
        }
    }
}
