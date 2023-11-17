using Malv.Data;
using Microsoft.EntityFrameworkCore;
using Malv.Data.EF;
using Malv.Filters;
using Malv.Services;
using Microsoft.Extensions.FileProviders;

AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
{
    Console.WriteLine(eventArgs.Exception);
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>((options) =>
{
    options.UseMySQL(connectionString);
    options.LogTo(Console.WriteLine);
});
builder.Services.InitMalvServices(builder.Configuration);
builder.Services.InitMalvAuth(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ControllerFilter>();
});

builder.Services.AddEntityFrameworkData();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
    .AllowCredentials()); // allow credentials

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

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
app.Run();