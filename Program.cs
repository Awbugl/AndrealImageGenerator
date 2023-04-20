using System.Net;
using AndrealImageGenerator.Common;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
ServicePointManager.ServerCertificateValidationCallback = (
    _,
    _,
    _,
    _) => true;

ServicePointManager.DefaultConnectionLimit = 512;
ServicePointManager.Expect100Continue = false;
ServicePointManager.UseNagleAlgorithm = false;
ServicePointManager.ReusePort = true;
ServicePointManager.CheckCertificateRevocationList = true;
WebRequest.DefaultWebProxy = null;

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore)
       .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
            options.SuppressMapClientErrors = true;
        });

var app = builder.Build();

app.UseExceptionHandler(new ExceptionHandlerOptions
                        {
                            ExceptionHandler = context =>
                            {
                                context.Response.Clear();
                                context.Response.StatusCode = 500;
                                var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                                if (ex != null) ExceptionLogger.Write(ex);
                                return Task.CompletedTask;
                            }
                        });

app.MapControllers();
app.Run();
