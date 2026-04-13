using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = System.TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

// Make ASP.NET Core configuration available to the legacy DAL layer
Library.SQLServer.Connection.SetConfiguration(builder.Configuration);
Library.Root.Other.BusinessLogicBase.SetConfiguration(builder.Configuration);

var app = builder.Build();

// If hosted under a virtual directory (e.g. /PFRLabelIssuing),
// set the PathBase so all generated URLs include the prefix.
// Read from config so you can change it per environment without recompiling.
var pathBase = builder.Configuration["AppSettings:PathBase"];
if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Serve static files from specific folders under the content root
// because the project does not use a wwwroot folder.
var contentRoot = builder.Environment.ContentRootPath;
string[] staticFolders = { "css", "image", "images", "js", "resources" };
foreach (var folder in staticFolders)
{
    var folderPath = System.IO.Path.Combine(contentRoot, folder);
    if (System.IO.Directory.Exists(folderPath))
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(folderPath),
            RequestPath = "/" + folder
        });
    }
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();

