using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EfDals;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcWebUI.Entities;
using MvcWebUI.Middlewares;
using MvcWebUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/{0}.cshtml");
    });
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal,EfCategoryDal>();
builder.Services.AddSingleton<ICartService,CartManager>();
builder.Services.AddSingleton<ICartSessionService,CartSessionManager>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//Sessionlar? çal?st?rmak ve aktif etmek için;
builder.Services.AddSession();

//CustomIdentityDbContext i de ekleyelim
builder.Services.AddDbContext<CustomIdentityDbContext>(options =>
options
.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Northwind;Trusted_Connection=true;"));
//Default token provider = Sayfalar aras?nda geçi? yaparken kullan?c? bilgilerinin ta??nmas?n? saglayan servistir !
builder.Services.AddIdentity<CustomIdentityUser,
    CustomIdentityRole>()
    .AddEntityFrameworkStores<CustomIdentityDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddDistributedMemoryCache();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
//app.UseMvc(ConfigureRoute);

// void ConfigureRoute(IRouteBuilder routeBuilder)
//{
//    routeBuilder.MapRoute("Default", "{ controller = Product}/{ action=Index}/{id?}");
//}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Products}/{action=Index}");
});
app.Run();
