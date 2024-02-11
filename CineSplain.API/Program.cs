var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOutputCache();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var allowedOrigin1 = Environment.GetEnvironmentVariable("ALLOWED_ORIGIN_1");
var allowedOrigin2 = Environment.GetEnvironmentVariable("ALLOWED_ORIGIN_2");
var allowedOrigin3 = Environment.GetEnvironmentVariable("ALLOWED_ORIGIN_3");
var allowedOrigin4 = Environment.GetEnvironmentVariable("ALLOWED_ORIGIN_4");

app.UseCors(options => {
    options.WithOrigins(allowedOrigin1, allowedOrigin2, allowedOrigin3, allowedOrigin4);
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.UseOutputCache();

app.Run();