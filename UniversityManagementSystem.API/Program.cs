using UniversityManagementSystem.API.Middlewares;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Interfaces.Addresses;
using UniversityManagementSystem.Application.Interfaces.Countries;
using UniversityManagementSystem.Application.Interfaces.MainFee;
using UniversityManagementSystem.Application.Interfaces.Phones;
using UniversityManagementSystem.Application.UseCases.Addresses;
using UniversityManagementSystem.Application.UseCases.Country;
using UniversityManagementSystem.Application.UseCases.MainFees;
using UniversityManagementSystem.Application.UseCases.Phone;
using UniversityManagementSystem.Infrastructure.Data;
using UniversityManagementSystem.Infrastructure.Data.ADO;

var builder = WebApplication.CreateBuilder(args);

// Register Repositories
builder.Services.AddScoped<IAddressRepository, AddressRepositoryData>();
builder.Services.AddScoped<IPhoneRepository, PhoneRepositoryData>();
builder.Services.AddScoped<ICountryRepository,CountryRepositoryData>();
builder.Services.AddScoped<IMainFeesRepository,MainFeesRepositoryData>();

// Register UseCase

//Address UseCase

builder.Services.AddScoped<AddAddressUseCase>();
builder.Services.AddScoped<UpdateAddressUseCase>();
builder.Services.AddScoped<GetAddressByIdUseCase>();
builder.Services.AddScoped<GetAddressesByPersonIdUseCase>();
builder.Services.AddScoped<GetAllAddressesUseCase>();

//Phone UseCase

builder.Services.AddScoped<AddPhoneNumberUseCase>();
builder.Services.AddScoped<UpdatePhoneNumberUseCase>();
builder.Services.AddScoped<GetPhoneByIdUseCase>();
builder.Services.AddScoped<GetPhoneByPhoneNumberUseCase>();
builder.Services.AddScoped<GetAllPhonesByPersonUseCase>();
builder.Services.AddScoped<GetAllPhonesUseCase>();

//Country UseCase

builder.Services.AddScoped<GetCountryByIdUseCase>();
builder.Services.AddScoped<GetCountryByNameUseCase>();
builder.Services.AddScoped<GetAllCountriesUseCase>();

//MainFees UseCase

builder.Services.AddScoped<AddMainFeeUseCase>();
builder.Services.AddScoped<UpdateMainFeeUseCase>();
builder.Services.AddScoped<DeleteMainFeeUseCase>();
builder.Services.AddScoped<GetMainFeeByIdUseCase>();
builder.Services.AddScoped<GetAllFeesUseCase>();

//Events Log
builder.Services.AddSingleton<IAppLog, clsEventLogLogger>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.Run();
