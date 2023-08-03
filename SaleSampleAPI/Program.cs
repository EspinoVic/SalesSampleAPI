using Microsoft.Extensions.DependencyInjection;
using SaleSampleAPI.Data;
using Microsoft.EntityFrameworkCore;
using SaleSampleAPI.Repository;
using SaleSampleAPI.Services;
using SaleSampleAPI.Services.interfaces;
using SaleSampleAPI.Repository.interfaces;

var builder = WebApplication.CreateBuilder(args);

//also injects the existing DBContexts
builder.Services.AddDbContext<SalesContext>
    (options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("SalesDB")
        //builder.Configuration.GetConnectionString("ProductContext")

        //get the ConnStr from appseeting.json
        )
    );
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//kind of register the controllers but they are not recognized until you map them (app.MapControllers)
builder.Services.AddControllers();

//Injections, must use interfaces to allow mocking god - dios god - esta god 
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISalesService, SalesService>();

builder.Services.AddScoped<ITaxesRegionRepository, TaxesRegionRepository>();


/*builder.Services.AddScoped<>*/


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
