using E_SportManager;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
            .AddDatabase(builder.Configuration)
            .AddDatabaseDeveloperPageExceptionFilter()
            .AddIdentity()
            .AddAntiforgeryHeader()
            .AddControllersWithAutoAntiforgeryTokenAttribute()
            .AddAutoMapper(typeof(MappingProfiler))
            .AddApplicationServices(builder.Configuration)
            .AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.PrepareDatabase()
   .UseHttpsRedirection()
   .UseStaticFiles()
   .UseRouting()
   .UseAuthentication()
   .UseAuthorization()
   .UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultAreaRoute();
        endpoints.MapDefaultControllerRoute();
        endpoints.MapRazorPages();
    });

app.Run();
