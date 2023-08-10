using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;
using TechWizMain.Areas.Identity.Data;
using TechWizMain.Data;
using TechWizMain.Models;
using TechWizMain.Repository;
using TechWizMain.Repository.BillRepository;
using TechWizMain.Repository.CategoryRepository;
using TechWizMain.Repository.FeedbackRepository;
using TechWizMain.Repository.ProductRepository;
using TechWizMain.Repository.UserRepository;
using TechWizMain.Services.AdminService;
using TechWizMain.Services.FeedbackService;
using TechWizMain.Services.ProductsService;
using TestEmail.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    IConfigurationSection googleAuthNSection = configuration.GetSection("Authentication:Google");
    googleOptions.ClientId = googleAuthNSection["ClientId"];
    googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
}).AddFacebook(facebookOptions =>
{
    IConfigurationSection facebookAuthNSection = configuration.GetSection("Authentication:Facebook");
    facebookOptions.AppId = facebookAuthNSection["AppId"];
    facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
    //facebookOptions.CallbackPath = "/signin-facebook";
    //facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
    //facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
});

//Mail Config
var mailSettings = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSettings);
builder.Services.AddSingleton<IEmailSender, SendMailService>();
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<TechWizContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<UserManagerContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentity<UserManager, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<UserManagerContext>().AddDefaultUI();
builder.Services.Configure<IdentityOptions>(options => {
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 5 lầ thì khóa
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
});


builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductService, ProductService>();
//Đăng kí service 
builder.Services.AddScoped(typeof(GenericRepository<>));
builder.Services.AddTransient<IProductRepository,ProductRepository>();
builder.Services.AddTransient<IBillRepository, BillRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IFeedbackService, FeedbackService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
