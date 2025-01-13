using Fake.API.Database;
using Fake.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
// 添加依賴注入
//builder.Services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();
builder.Services.AddScoped<ITouristRouteRepository, TouristRouteRepository>();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

var app = builder.Build();

// 配置中介軟體
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
// 配置控制器路由 
app.MapControllers();

app.Run();


