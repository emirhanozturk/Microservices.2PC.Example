using Coordinator.Models.Contexts;
using Coordinator.Services;
using Coordinator.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TwoPhaseCommitContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddHttpClient("OrderAPI", client => client.BaseAddress = new("https://localhost:7079"));
builder.Services.AddHttpClient("StockAPI", client => client.BaseAddress = new("https://localhost:7258"));
builder.Services.AddHttpClient("PaymentAPI", client => client.BaseAddress = new("https://localhost:7220"));

builder.Services.AddTransient<ITransactionService, TransactionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/create-order-transaction",async (ITransactionService transactionService) =>
{
    //Phase 1 - Prepare
    var transactionId = await transactionService.CreateTransactionAsync();
    await transactionService.PrepareServicesAsync(transactionId);
    bool transactionState = await transactionService.CheckReadyServicesAsync(transactionId);
    if (transactionState)
    {
        // Phase Commit - Commit
        await transactionService.CommitAsync(transactionId);
        transactionState =await transactionService.CheckTransactionStateServicesAsync(transactionId);
    }

    if(!transactionState)
    {
        await transactionService.RollbackAsync(transactionId);
    }
});


app.Run();
