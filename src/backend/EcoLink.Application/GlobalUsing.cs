// MAIN
global using MediatR;
global using AutoMapper;
global using EcoLink.Domain.Enums;
global using EcoLink.Domain.Entities.Users;
global using EcoLink.Application.Users.DTOs;
global using EcoLink.Domain.Entities.Investment;
global using EcoLink.Application.Commons.Helpers;
global using EcoLink.Application.Commons.Constants;
global using EcoLink.Domain.Entities.Representation;
global using EcoLink.Application.Commons.Exceptions;
global using EcoLink.Application.Commons.Interfaces;
global using EcoLink.Domain.Entities.Entrepreneurship;
global using EcoLink.Domain.Entities.ProjectManagement;

// //
global using EcoLink.Application.Commons.Mappers;
global using EcoLink.Application.InvestmentApps.DTOs;
global using Microsoft.Extensions.DependencyInjection;
global using EcoLink.Application.Users.Queries.GetUsers;
global using EcoLink.Application.RepresentationApps.DTOs;
global using EcoLink.Application.EntrepreneurshipApps.DTOs;
global using EcoLink.Application.ProjectManagementApps.DTOs;
global using EcoLink.Application.Users.Commands.CreateUsers;
global using EcoLink.Application.Users.Commands.DeleteUsers;
global using EcoLink.Application.Users.Commands.UpdateUsers;
global using EcoLink.Application.Apps.Commands.CreateEntrepreneurs;
global using EcoLink.Application.InvestmentApps.Queries.GetInvestmentApp;
global using EcoLink.Application.InvestmentApps.Commands.CreateInvestment;
global using EcoLink.Application.RepresentationApps.Queries.GetRepresentationApp;
global using EcoLink.Application.EntrepreneurshipApps.Queries.GetEntrepreneurshipApp;
global using EcoLink.Application.ProjectManagementApps.Queries.GetProjectManagementApp;
global using EcoLink.Application.InvestmentApps.Commands.UpdateInvestment;
global using EcoLink.Application.RepresentationApps.Commands.CreateRepresentation;
global using EcoLink.Application.RepresentationApps.Commands.UpdateRepresentation;
global using EcoLink.Application.EntrepreneurshipApps.Commands.UpdateEntrepreneurship;
global using EcoLink.Application.ProjectManagementApps.Commands.CreateProjectManagement;
global using EcoLink.Application.ProjectManagementApps.Commands.UpdateProjectManagement;