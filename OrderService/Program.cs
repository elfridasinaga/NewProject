using GraphQLAPI.GraphQL;
using Microsoft.EntityFrameworkCore;
using OrderService.Models;

var builder = WebApplication.CreateBuilder(args);
var conString = builder.Configuration.GetConnectionString("MyDatabase");
builder.Services.AddDbContext<StudyCaseContext>(options =>
     options.UseSqlServer(conString)
);

builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("KafkaSettings"));
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("TokenSettings"));

// graphql
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.MapGraphQL();
app.MapGet("/", () => "Hello World!");


app.Run();
