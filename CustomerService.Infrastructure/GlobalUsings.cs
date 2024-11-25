//-----------------------System
global using System.Linq.Expressions;
//-----------------------Microsoft
global using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
//-----------------------CustomerService
global using CustomerService.Domain.ValueObjects;
global using CustomerService.Domain.IEntities;
global using CustomerService.Infrastructure.Data.Configurations;
global using CustomerService.Infrastructure.Data.Properties;
global using CustomerService.Infrastructure.ValueConversions;
global using CustomerService.Application.IRepository.ICustomerRepositories;
global using CustomerService.Application.IRepository;
global using CustomerService.Infrastructure.Data;
global using CustomerService.Domain.Entities;