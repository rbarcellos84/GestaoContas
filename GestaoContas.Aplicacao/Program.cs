using GestaoContas.Dominio.Interfaces.Reports;
using GestaoContas.Dominio.Interfaces.Repositories;
using GestaoContas.Dominio.Interfaces.Services;
using GestaoContas.Dominio.Services;
using GestaoContas.InfraEstrutura.Repositories;
using GestaoContas.InfraRelatorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//mapeamento das injeções de dependencia ------------------------------------
builder.Services.AddTransient<IContaDominioService, ContaDominioService>();
builder.Services.AddTransient<IContaRepository, ContaRepository>();
builder.Services.AddTransient<IContaReports, ContaReports>();
//mapeamento das injeções de dependencia ------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contas}/{action=Home}/{id?}");

app.Run();
