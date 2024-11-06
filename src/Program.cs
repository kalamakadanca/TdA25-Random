using TourDeApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents(); // Add Blazor services
builder.Services.AddControllers(); // Add API controllers

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

app.UseAuthorization();

// Map API controllers
app.MapControllers();

// Map Razor components for Blazor Server
app.MapRazorComponents<App>();

app.Run();
