
using Microsoft.EntityFrameworkCore;
using OrderService.GraphQL;
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("allowedOrigin", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}
);

var app = builder.Build();

app.MapGraphQL();
app.MapGet("/", () => "Hello World!");


app.Run();
