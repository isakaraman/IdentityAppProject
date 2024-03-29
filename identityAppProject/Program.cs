using identityAppProject.Authorization;
using identityAppProject.Data;
using identityAppProject.Helpers;
using identityAppProject.Interfaces;
using identityAppProject.Models;
using identityAppProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(e =>
e.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ISendGridEmail, SendGridEmail>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("SendGrid"));
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyTrainerChecker", policy => policy.Requirements.Add(new OnlyTrainerAuthorization()));
    options.AddPolicy("CheckNicknameTeddy", policy => policy.Requirements.Add(new NicknameRequirment("teddy")));
});
builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = "test";
        options.AppSecret = "test";
    })
    .AddGoogle(options =>
    {
        options.ClientId = "test";
        options.ClientSecret = "test";
    });
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 5;
    opt.Password.RequireLowercase = true;
    opt.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromSeconds(5);
    opt.Lockout.MaxFailedAccessAttempts = 5;
    //opt.SignIn.RequireConfirmedAccount = true;
});

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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
