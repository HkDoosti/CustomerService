//--------------------------System
global using System.Net;
global using System.Text.Json;
global using System.Reflection;
//--------------------------Microsoft
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.AspNetCore.Mvc;
//--------------------------MediatR
global using MediatR;
//--------------------------FluentValidation
global using FluentValidation;
//--------------------------CustomerService
global using CustomerService.Application.IRepository;
global using CustomerService.Application.IRepository.ICustomerRepositories;
global using CustomerService.Infrastructure.Data;
global using CustomerService.Infrastructure.Repository;
global using CustomerService.Infrastructure.Repository.CustomerRepository;
global using CustomerService.SharedKernel;
global using CustomerService.Presentation.Server.Extensions;
global using CustomerService.Presentation.Server.Middlewars;
global using CustomerService.Application;
global using CustomerService.Application.Behaviors;
global using CustomerService.Application.Commands.CustomerCommands.AddCustomerCommand;
global using CustomerService.Application.Commands.CustomerCommands.DeleteCustomerCommand;
global using CustomerService.Application.Commands.CustomerCommands.EditCustomerCommand;

