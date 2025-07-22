using Microsoft.EntityFrameworkCore;
using reserva_de_salas.Data;
using reserva_de_salas.Interfaces;
using reserva_de_salas.Repositories;
using reserva_de_salas.Repositorys;
using reserva_de_salas.Services;
using reserva_de_salas.Services.Strategy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuração do banco de dados
builder.Services.AddDbContext<BancoContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Escopos de Repositorio
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ISalaRepository, SalaRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

// Escopos das Estratégias de Validação (Padrão Strategy)
builder.Services.AddScoped<ValidadorDeReservaCapacidade>();
builder.Services.AddScoped<ValidadorDeReservaHorario>(); 

//Escopos de Serviço
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ISalaService, SalaService>();
builder.Services.AddScoped<IReservaService, ReservaService>();

// Registro do Facade (Padrão Facade)
builder.Services.AddScoped<ReservasFacade>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Reserva}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
