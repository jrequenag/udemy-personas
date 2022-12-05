using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;

using Microsoft.Extensions.Configuration;

using Persons.Cmd.Api.Commands;
using Persons.Cmd.Domain.Aggregates;
using Persons.Cmd.Infratrusture.Config;
using Persons.Cmd.Infratrusture.Dispachers;
using Persons.Cmd.Infratrusture.Handlers;
using Persons.Cmd.Infratrusture.Producers;
using Persons.Cmd.Infratrusture.Repositories;
using Persons.Cmd.Infratrusture.Stores;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<RabbitMqConfigParams>(builder.Configuration.GetSection(nameof(RabbitMqConfigParams)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<PersonAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();


//registro de command hanlder method

var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
var dispatcher = new CommandDispatcher();
dispatcher.RegisterHandler<NewPersonCommand>(commandHandler.HandlerAsync);
dispatcher.RegisterHandler<EditPersonCommand>(commandHandler.HandlerAsync);
dispatcher.RegisterHandler<AddIdentityDocumentCommand>(commandHandler.HandlerAsync);
dispatcher.RegisterHandler<DeletePersonCommand>(commandHandler.HandlerAsync);
dispatcher.RegisterHandler<EditdentityDocumentCommand>(commandHandler.HandlerAsync);
dispatcher.RegisterHandler<RemoveIdentityDocumentCommand>(commandHandler.HandlerAsync);

builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
