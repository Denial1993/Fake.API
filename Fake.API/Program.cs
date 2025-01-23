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

// ���U����ñҥ� XML �䴩
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
            Type = "�L�ҿ�",
            Title = "�ƾ����ҥ���",
            Status = StatusCodes.Status422UnprocessableEntity,
            Detail = "�ЬݸԲӤ��e",
            Instance = context.HttpContext.Request.Path
        };

        problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
        return new UnprocessableEntityObjectResult(problemDetail)
        {
            ContentTypes = { "Application/problem+json" }
        };
    };
});

// �K�[�̿�`�J
builder.Services.AddScoped<ITouristRouteRepository, TouristRouteRepository>();

// �]�w��Ʈw�s�u (MySQL)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySQLConnectionString");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// �]�w AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<TouristRouteProfile>(); // �T�O�A���Ы� TouristRouteProfile ���O
    cfg.AddProfile<TouristRoutePictureProfile>(); // �T�O�A���Ы� TouristRoutePictureProfile ���O
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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
