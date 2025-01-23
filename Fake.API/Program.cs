using Fake.API.Database;
using Fake.API.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Fake.API;
using Fake.API.Profiles;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 註冊控制器並啟用 XML 支援
builder.Services.AddControllers(setupAction =>
{
    setupAction.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson(setupAction =>
{
    setupAction.SerializerSettings.ContractResolver = new
    CamelCasePropertyNamesContractResolver();
})
    .AddXmlDataContractSerializerFormatters()
    .ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetail = new ValidationProblemDetails(context.ModelState)
        {
            Type = "無所謂",
            Title = "數據驗證失敗",
            Status = StatusCodes.Status422UnprocessableEntity,
            Detail = "請看詳細內容",
            Instance = context.HttpContext.Request.Path
        };

        problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
        return new UnprocessableEntityObjectResult(problemDetail)
        {
            ContentTypes = { "Application/problem+json" }
        };
    };
});

// 添加依賴注入
builder.Services.AddScoped<ITouristRouteRepository, TouristRouteRepository>();

// 設定資料庫連線 (MySQL)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySQLConnectionString");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// 設定 AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<TouristRouteProfile>(); // 確保你有創建 TouristRouteProfile 類別
    cfg.AddProfile<TouristRoutePictureProfile>(); // 確保你有創建 TouristRoutePictureProfile 類別
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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
