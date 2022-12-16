using CQRS.Core.Config;
using CQRS.Core.Consumers;

using Microsoft.EntityFrameworkCore;

using Persons.Query.Domain.Repositories;
using Persons.Query.Infrastructure.Consumers;
using Persons.Query.Infrastructure.DataAccess;
using Persons.Query.Infrastructure.Handlers;
using Persons.Query.Infrastructure.repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Action<DbContextOptionsBuilder> configureDbContext = (o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDbContext<DatabaseContext>(configureDbContext);
builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

var databaseContext = builder.Services.BuildServiceProvider().GetService<DatabaseContext>();
databaseContext.Database.EnsureCreated();
builder.Services.Configure<RabbitMqConfigParams>(builder.Configuration.GetSection(nameof(RabbitMqConfigParams)));

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IDocumentIdentityRepository, DocumentIdentityRepository>();
builder.Services.AddScoped<IEventHandler, Persons.Query.Infrastructure.Handlers.EventHandler>();
builder.Services.AddScoped<IEventConsumer, EventConsumer>();

builder.Services.AddControllers();

builder.Services.AddHostedService<ConsumerHostedService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
