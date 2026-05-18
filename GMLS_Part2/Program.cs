using Microsoft.EntityFrameworkCore;
using GMLS_Part2.Data;
using GMLS_Part2.Services;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// ADD MVC SERVICES
// =====================================================

builder.Services.AddControllersWithViews();

// =====================================================
// REGISTER SQL SERVER LOCALDB
// =====================================================

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration
            .GetConnectionString("DefaultConnection")));

// =====================================================
// REGISTER CUSTOM SERVICES
// =====================================================

// Currency API Service

builder.Services.AddHttpClient<CurrencyService>();

// File Validation Service

builder.Services.AddScoped<FileValidationService>();

// =====================================================
// BUILD APPLICATION
// =====================================================

var app = builder.Build();

// =====================================================
// CONFIGURE HTTP REQUEST PIPELINE
// =====================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

// =====================================================
// STATIC FILES
// =====================================================

app.MapStaticAssets();

// =====================================================
// DEFAULT ROUTE
// =====================================================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// =====================================================
// RUN APPLICATION
// =====================================================

app.Run();