using Microsoft.Extensions.DependencyInjection;
using SaleSampleAPI.Data;
using Microsoft.EntityFrameworkCore;
using SaleSampleAPI.Repository;
using SaleSampleAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductContext>
    (options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("ProductContext") 
        //get the ConnStr from appseeting.json
        )
    );
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//kind of register the controllers but they are not recognized until you map them (app.MapControllers)
builder.Services.AddControllers();

//Injections, must use interfaces to allow mocking god
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
