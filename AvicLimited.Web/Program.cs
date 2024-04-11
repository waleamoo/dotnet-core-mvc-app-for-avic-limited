using AvicLimited.Data.Models;
using AvicLimited.Web.Infrastructure;
using AvicLimited.Web.Repositories.Implementation;
using AvicLimited.Web.Repositories.Interface;
using AvicLimited.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// add razor compilation for development 
#if DEBUG
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
#endif

// use lower case urls 
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add the DB context 
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("AvicContext"))
    );
// identity with identity role 
builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.User.RequireUniqueEmail = true; // requires unique email 
}).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

// inject repos
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
// services
builder.Services.AddScoped<IUserService, UserService>();
// redirect authorized request 
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = builder.Configuration["Application:LoginPath"];
});

// claims factory - for the header to display the name and last name of the authenticated user 
builder.Services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, AppUserClaimsPrincipalFactory>();


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
    name: "areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "searchProjectByCategory",
    pattern: "{controller=Home}/{action=Index}/{p}/{categoryId?}/{subCategoryId?}"
);

app.Run();
