using OrgBloom.Application.Investors.DTOs;
using OrgBloom.Application.Entrepreneurs.DTOs;
using OrgBloom.Application.ProjectManagers.DTOs;
using OrgBloom.Application.Representatives.DTOs;

namespace OrgBloom.Bot.BotServices.Helpers;

public static class StringHelper
{
    public static string GetApplicationInfoForm(EntrepreneurResultDto dto)
        => $"Ism: {dto.User.FirstName}\n" +
        $"Familiya: {dto.User.LastName}\n" +
        $"Yoshi: {TimeHelper.GetAge(dto.User.DateOfBirth)}\n" +
        $"Rol: {Enum.GetName(dto.User.Profession)}\n" +
        $"Sektor: {dto.User.Experience}\n" +
        $"Loyiha: {dto.Project}\n" +
        $"Yordam turi: {dto.HelpType}\n" +
        $"Kerakli summa: {dto.RequiredFunding}\n" +
        $"Tikilgan aktiv: {dto.AssetsInvested}\n" +
        $"Telefon raqami: {dto.User.Phone}\n" +
        $"Email: {dto.User.Email}";

    public static string GetApplicationInfoForm(InvestorResultDto dto)
        => $"Ism: {dto.User.FirstName}\n" +
        $"Familiya: {dto.User.LastName}\n" +
        $"Yoshi: {TimeHelper.GetAge(dto.User.DateOfBirth)}\n" +
        $"Rol: {Enum.GetName(dto.User.Profession)}\n" +
        $"Sektor: {dto.Sector}\n" +
        $"Ma'lumoti: {dto.User.Degree}\n" +
        $"Qiymati: {dto.InvestmentAmount}\n" +
        $"Telefon raqami: {dto.User.Phone}\n" +
        $"Email: {dto.User.Email}";

    public static string GetApplicationInfoForm(RepresentativeResultDto dto)
        => $"Ism: {dto.User.FirstName}\n" +
        $"Familiya: {dto.User.LastName}\n" +
        $"Yoshi: {TimeHelper.GetAge(dto.User.DateOfBirth)}\n" +
        $"Rol: {Enum.GetName(dto.User.Profession)}\n" +
        $"Loyiha: {dto.User.Languages}\n" +
        $"Sektor: {dto.User.Experience}\n" +
        $"Yordam turi: {dto.User.Address}\n" +
        $"Kerakli summa: {dto.Area}\n" +
        $"Tikilgan aktiv: {dto.Expectation}\n" +
        $"Telefon raqami: {dto.Purpose}\n";

    public static string GetApplicationInfoForm(ProjectManagerResultDto dto)
        => $"Ism: {dto.User.FirstName}\n" +
        $"Familiya: {dto.User.LastName}\n" +
        $"Yoshi: {TimeHelper.GetAge(dto.User.DateOfBirth)}\n" +
        $"Rol: {Enum.GetName(dto.User.Profession)}\n" +
        $"Loyiha: {dto.User.Languages}\n" +
        $"Sektor: {dto.User.Experience}\n" +
        $"Yordam turi: {dto.User.Address}\n" +
        $"Kerakli summa: {dto.ProjectDirection}\n" +
        $"Tikilgan aktiv: {dto.Expectation}\n" +
        $"Telefon raqami: {dto.Purpose}\n";
}
