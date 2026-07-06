using UniversityManagementSystem.API.Middlewares;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Interfaces.Addresses;
using UniversityManagementSystem.Application.Interfaces.Countries;
using UniversityManagementSystem.Application.Interfaces.Persons;
using UniversityManagementSystem.Application.Interfaces.Phones;
using UniversityManagementSystem.Application.UseCases.Addresses;
using UniversityManagementSystem.Application.UseCases.Countries;
using UniversityManagementSystem.Application.UseCases.Persons;
using UniversityManagementSystem.Application.UseCases.Phones;
using UniversityManagementSystem.Infrastructure.Data;
using UniversityManagementSystem.Infrastructure.Data.ADO;

var builder = WebApplication.CreateBuilder(args);

// Register Repositories
builder.Services.AddScoped<IAddressRepository, AddressRepositoryData>();
builder.Services.AddScoped<IPhoneRepository, PhoneRepositoryData>();
builder.Services.AddScoped<ICountryRepository, CountryRepositoryData>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryData>();

// Register UseCase

//Address UseCase

builder.Services.AddScoped<AddAddressUseCase>();
builder.Services.AddScoped<UpdateAddressUseCase>();
builder.Services.AddScoped<GetAddressByIdUseCase>();
builder.Services.AddScoped<GetAddressesByPersonIdUseCase>();
builder.Services.AddScoped<GetAllAddressesUseCase>();

//Phone UseCase

builder.Services.AddScoped<AddPhoneUseCase>();
builder.Services.AddScoped<UpdatePhoneUseCase>();
builder.Services.AddScoped<GetPhoneByIdUseCase>();
builder.Services.AddScoped<GetPhoneByPhoneNumberUseCase>();
builder.Services.AddScoped<GetPhonesByPersonIdUseCase>();
builder.Services.AddScoped<GetAllPhonesUseCase>();

//Country UseCase

builder.Services.AddScoped<GetCountryByIdUseCase>();
builder.Services.AddScoped<GetCountryByNameUseCase>();
builder.Services.AddScoped<GetAllCountriesUseCase>();

//Person UseCase

builder.Services.AddScoped<AddPersonUseCase>();
builder.Services.AddScoped<DeletePersonUseCase>();
builder.Services.AddScoped<GetAllPeopleUseCase>();
builder.Services.AddScoped<GetPersonByIdUseCase>();
builder.Services.AddScoped<GetPersonByNationalNoUseCase>();
builder.Services.AddScoped<UpdatePersonInfoByAdminUseCase>();
builder.Services.AddScoped<UpdatePersonUseCase>();

//Events Log
builder.Services.AddSingleton<IAppLog, EventLogLogger>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.Run();