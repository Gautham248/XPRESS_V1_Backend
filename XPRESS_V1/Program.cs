using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register repositories as services
builder.Services.AddScoped<ITravelRequestService, TravelRequestRepository>();
builder.Services.AddScoped<IAuditLogService, AuditLogRepository>();
builder.Services.AddScoped<IUserService, UserRepository>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply CORS policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();