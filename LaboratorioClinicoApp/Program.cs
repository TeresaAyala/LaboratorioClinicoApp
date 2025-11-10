using LaboratorioClinicoApp.Components;
using LaboratorioClinicoApp.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// Dirección de tu API (ajusta si está en la nube o local)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:44320/")
});



//Builder de Login
builder.Services.AddScoped<AuthServices>();
builder.Services.AddScoped<ProtectedLocalStorage>();

//Los builder de cada entidad
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<CitaService>();
builder.Services.AddScoped<ExamenService>();
builder.Services.AddScoped<ResultadoService>();
builder.Services.AddScoped<RolService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
