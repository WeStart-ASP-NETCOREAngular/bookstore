using BookStore.API.Data;
using BookStore.API.Mapping;
using BookStore.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMapping();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDataLayer(builder.Configuration)
    .AddAuthLayer(builder.Configuration);
builder.Services.AddTransient<IImageUploaderService, ImageUploaderService>();
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy",
        builder => builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials().WithExposedHeaders("totalAmountOfRecords"));
});

builder.Services.AddSwaggerConfig();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
