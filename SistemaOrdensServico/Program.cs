using Microsoft.EntityFrameworkCore;
using SistemaOrdensServico.Data;
using SistemaOrdensServico.Repositories;
using SistemaOrdensServico.Repositories.Interfaces;
using SistemaOrdensServico.Services;
using SistemaOrdensServico.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos serviços MVC
builder.Services.AddControllersWithViews();

// Configuração da conexão com o banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConexaoBanco")
    )
);

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();

builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();

builder.Services.AddScoped<IServicoRepository, ServicoRepository>();

builder.Services.AddScoped<ITecnicoRepository, TecnicoRepository>();

// Serviço
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IEquipamentoService, EquipamentoService>();

builder.Services.AddScoped<IOrdemServicoService, OrdemServicoService>();

builder.Services.AddScoped<IServicoService, ServicoService>();

builder.Services.AddScoped<ITecnicoService, TecnicoService>();


var app = builder.Build();

// Configuração do ambiente
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rota inicial do sistema
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
