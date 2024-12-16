using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Carglass.TechnicalAssessment.Backend.DL;
using Carglass.TechnicalAssessment.Backend.BL;
using Carglass.TechnicalAssessment.Backend.Dtos;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

//Configurar Autofac como el contenedor de dependencias
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule<BLModule>();
    containerBuilder.RegisterModule<DLModule>();
    containerBuilder.RegisterModule<DtosModule>();
});

//Registrar FluentValidation
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<Program>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Registrar AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new() { Title = "Javier Mora - Carglass Technical Assesment API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();