using OrgBloom.Application.Investors.DTOs;
using OrgBloom.Application.Entrepreneurs.DTOs;
using OrgBloom.Application.ProjectManagers.DTOs;
using OrgBloom.Application.Representatives.DTOs;

namespace OrgBloom.Bot.BotServices.Helpers;

public static class StringHelper
{
    public static string GetEntrepreneurshipApplicationInfoForm(EntrepreneurResultDto dto)
        => $"Ism: {dto.User.FirstName}\n" +
        $"Familiya: {dto.User.LastName}\n" +
        $"Otasining ismi: {dto.User.Patronomyc}\n" +
        $"Yoshi: {TimeHelper.GetAge(dto.User.DateOfBirth)}\n" +
        $"Sohasi: {Enum.GetName(dto.User.Profession)}";

    public static string GetInvestmentApplicationInfoForm(InvestorResultDto dto)
        => $"Ism: {dto.User.FirstName}\n" +
        $"Familiya: {dto.User.LastName}\n" +
        $"Otasining ismi: {dto.User.Patronomyc}\n" +
        $"Yoshi: {TimeHelper.GetAge(dto.User.DateOfBirth)}\n" +
        $"Sohasi: {Enum.GetName(dto.User.Profession)}";

    public static string GetProjectManagementApplicationInfoForm(ProjectManagerResultDto dto)
        => $"Ism: {dto.User.FirstName}\n" +
        $"Familiya: {dto.User.LastName}\n" +
        $"Otasining ismi: {dto.User.Patronomyc}\n" +
        $"Yoshi: {TimeHelper.GetAge(dto.User.DateOfBirth)}\n" +
        $"Sohasi: {Enum.GetName(dto.User.Profession)}";

    public static string GetRepresentationApplicationInfoForm(RepresentativeResultDto dto)
        => $"Ism: {dto.User.FirstName}\n" +
        $"Familiya: {dto.User.LastName}\n" +
        $"Otasining ismi: {dto.User.Patronomyc}\n" +
        $"Yoshi: {TimeHelper.GetAge(dto.User.DateOfBirth)}\n" +
        $"Sohasi: {Enum.GetName(dto.User.Profession)}";
}
