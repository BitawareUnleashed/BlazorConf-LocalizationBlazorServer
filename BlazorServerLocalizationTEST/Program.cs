using BlazorServerLocalizationTEST.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(options => options.DetailedErrors = true);

/* ADD LOCALIZATION */
builder.Services.AddLocalization();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


/* SIMULATES SERVICE FOR MESSAGING */
builder.Services.AddScoped<GenericService>();


builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "EnableCors",
        policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyOrigin();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


/* ADD LOCALIZATION */
var localizeOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures("en", "it", "uk")
    .AddSupportedUICultures("en", "it", "uk");
app.UseRequestLocalization(localizeOptions);



app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("EnableCors");

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
