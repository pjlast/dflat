using Transactions.APIV1;
using Transactions.Store;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IStore>(new InMemoryStore());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("/api/v1/transactions").MapTransactionsAPI();

app.Run();

public partial class Program { }
