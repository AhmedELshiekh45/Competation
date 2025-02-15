using System.Security.Claims;
using BL_Layer;
using BL_Layer.Repos;
using DataAccess_Layer.Context;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.CompetitonBL;
using ServiceLayer.ContestantBL.Mapping;
using ServiceLayer.ContestantBL.Service;
using ServiceLayer.ExamLogic.Service;
using ServiceLayer.TeacherLogic.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole>(
    p =>
    {
        p.Password.RequireNonAlphanumeric = false;
        p.Password.RequiredLength = 6;
        p.Password.RequireUppercase = false;
        p.Password.RequireDigit = false;
    }



    ).AddEntityFrameworkStores<MyContext>();

builder.Services.AddScoped<MyContext>();
builder.Services.AddScoped<ContestentServices>();
builder.Services.AddScoped<ExamService>();
builder.Services.AddScoped<TeacherService>();
builder.Services.AddScoped<ComputionService>();
//builder.Services.AddScoped<IService<CreateContestantDto>, ContestentServices>();
builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

builder.Services.AddAutoMapper(typeof(ContestantProfile).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    await SeedRoles(roleManager);
    await SeedAdmin(userManager);
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
{
    var roles = new[] { "Admin", "Result Entry", "Data Entry", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
static async Task SeedAdmin(UserManager<User> userManager)
{
    var admin =
       new User
       {
           Id = Guid.NewGuid().ToString(),
           Email = "ahm75295@gmail.com",
           PasswordHash = "Ahmed123!@#",
           UserName = "Ahmedelshiekh45",
           PhoneNumber = "01063038833"
       };
    var claim = new Claim("Role", "Admin");
    if (await userManager.FindByEmailAsync(admin.Email) == null)
    {
        var result = await userManager.CreateAsync(admin, "Ahmed123!@#");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
            await userManager.AddClaimAsync(admin, claim);
        }


    }
}