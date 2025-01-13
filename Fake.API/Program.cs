using Fake.API.Database;
using Fake.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
// �K�[�̿�`�J
//builder.Services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();
builder.Services.AddScoped<ITouristRouteRepository, TouristRouteRepository>();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

var app = builder.Build();

// �t�m�����n��
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
// �t�m������� 
app.MapControllers();

app.Run();


