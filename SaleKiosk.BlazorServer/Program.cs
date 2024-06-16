using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using SaleKiosk.Application.Mappings;
using SaleKiosk.Application.Services;
using SaleKiosk.Application.Validators;
using SaleKiosk.BlazorServer.Data;
using SaleKiosk.Domain.Contracts;
using SaleKiosk.Infrastructure;
using SaleKiosk.Infrastructure.Repositories;
using SaleKiosk.SharedKernel.Dto;
using NLog.Web;
using NLog;
using SaleKiosk.BlazorServer.Services;
using Radzen;


// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddSingleton<WeatherForecastService>();

    // rejestracja automappera w kontenerze IoC
    builder.Services.AddAutoMapper(typeof(KioskMappingProfile));

    // rejestracja kontekstu bazy w kontenerze IoC
    var sqliteConnectionString = @"Data Source=c:\Database\database.db";

    builder.Services.AddDbContext<KioskDbContext>(options =>
        options.UseSqlite(sqliteConnectionString));

    // rejestracja walidatora
    builder.Services.AddScoped<IValidator<CreateHotelDto>, RegisterCreateHotelDtoValidator>();
    builder.Services.AddScoped<IValidator<UpdateHotelDto>, RegisterUpdateHotelDtoValidator>();

    builder.Services.AddScoped<IValidator<CreateUserDto>, RegisterCreateUserDtoValidator>();
    builder.Services.AddScoped<IValidator<UpdateUserDto>, RegisterUpdateUserDtoValidator>();

    builder.Services.AddScoped<IValidator<CreateReviewDto>, RegisterCreateReviewDtoValidator>();


    builder.Services.AddScoped<IKioskUnitOfWork, KioskUnitOfWork>();
    builder.Services.AddScoped<DataSeeder>();

    builder.Services.AddScoped<IHotelRepository, HotelRepository>();
    builder.Services.AddScoped<IHotelService, HotelService>();

    builder.Services.AddScoped<IUserRepository, UserRepository>();//
    builder.Services.AddScoped<IUserService, UserService>();


    builder.Services.AddScoped<IFileService, FileService>();

    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<IOrderService, OrderService>();

    builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
    builder.Services.AddScoped<IReviewService, ReviewService>();


    builder.Services.AddRadzenComponents();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    //// seeding data
    //using (var scope = app.Services.CreateScope())
    //{
    //    var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    //    dataSeeder.Seed();
    //}

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

