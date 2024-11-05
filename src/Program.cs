var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Remove MVC services
// builder.Services.AddControllersWithViews();
builder.Services.AddRazorComponents(); // Add Blazor services

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

app.UseAuthorization();

// Remove MVC route mapping
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapBlazorHub(); // Map Blazor Hub
app.MapFallbackToPage("/_Host"); // Map fallback to Blazor

app.Run();
