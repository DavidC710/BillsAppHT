﻿global using Microsoft.AspNetCore.Mvc;
global using MediatR;
global using BillsApp.Application.UseCases.Common;
global using BillsApp.Application.UseCases.Login.Commands.CreateUserCommand;
global using BillsApp.Application.UseCases.Login.Commands.LoginCommand;
global using BillsApp.Application.Interfaces.Repositories;
global using BillsApp.Infrastructure.Repositories;
global using BillsApp.Infrastructure;
global using Microsoft.EntityFrameworkCore;
global using BillsApp.Infrastructure.Factories;
global using BillsApp.Infrastructure.Interceptors;
global using BillsApp.Application.UseCases.Login.Commands.RecoverPasswordCommand;
global using BillsApp.Application.UseCases.Login.Queries;
global using FluentValidation;
global using BillsApp.Application.UseCases.Login.Commands.UpdateUserCommand;
global using Serilog;