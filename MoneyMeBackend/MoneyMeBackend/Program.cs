using MoneyMeBackend.Workers.Abstraction;
using MoneyMeBackend.Workers;
using MoneyMeBackend.DBContext;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add Cross Origin
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register dbcontext
builder.Services.AddDbContext<MoneyMeDBContext>();

// register dependencies
builder.Services.AddScoped<ICalculateQuoteWorker, CalculateQuoteWorker>();
builder.Services.AddScoped<IGetQuoteWorker, GetQuoteWorker>();
builder.Services.AddScoped<IApplyLoanWorker, ApplyLoanWorker>();
builder.Services.AddScoped<IEditQuoteWorker, EditQuoteWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
