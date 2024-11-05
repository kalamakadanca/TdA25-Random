using TourDeApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Remove MVC services
// builder.Services.AddControllersWithViews();
builder.Services.AddRazorComponents(); // Add Blazor services

// Add anti-forgery services
builder.Services.AddAntiforgery();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Update error handling
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add anti-forgery middleware
app.UseAntiforgery();

// Remove authorization and MVC route mapping
// app.UseAuthorization();
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

// Map Razor components for Blazor Server
app.MapRazorComponents<App>();

app.Run();
