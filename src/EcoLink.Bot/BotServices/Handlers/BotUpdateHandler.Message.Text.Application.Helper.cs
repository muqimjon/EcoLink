using EcoLink.Application.Investors.DTOs;
using EcoLink.Application.Entrepreneurs.DTOs;
using EcoLink.Application.ProjectManagers.DTOs;
using EcoLink.Application.Representatives.DTOs;

namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    public static string GetApplicationInfoForm(EntrepreneurResultDto dto)
        => $"Ism: {dto.User.FirstName}\n" +
        $"Familiya: {dto.User.LastName}\n" +
        $"Yoshi: {dto.User.Age}\n" +
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
        $"Yoshi: {dto.User.Age}\n" +
        $"Rol: {Enum.GetName(dto.User.Profession)}\n" +
        $"Sektor: {dto.Sector}\n" +
        $"Ma'lumoti: {dto.User.Degree}\n" +
        $"Qiymati: {dto.InvestmentAmount}\n" +
        $"Telefon raqami: {dto.User.Phone}\n" +
        $"Email: {dto.User.Email}";

    public static string GetApplicationInfoForm(RepresentativeResultDto dto)
        => $"Ism: {dto.User.FirstName}\n" +
        $"Familiya: {dto.User.LastName}\n" +
        $"Yoshi: {dto.User.Age}\n" +
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
        $"Yoshi: {dto.User.Age}\n" +
        $"Rol: {Enum.GetName(dto.User.Profession)}\n" +
        $"Loyiha: {dto.User.Languages}\n" +
        $"Sektor: {dto.User.Experience}\n" +
        $"Yordam turi: {dto.User.Address}\n" +
        $"Kerakli summa: {dto.ProjectDirection}\n" +
        $"Tikilgan aktiv: {dto.Expectation}\n" +
        $"Telefon raqami: {dto.Purpose}\n";
}
