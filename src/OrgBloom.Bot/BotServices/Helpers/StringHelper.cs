using OrgBloom.Application.Entrepreneurs.DTOs;
using OrgBloom.Application.Investors.DTOs;

namespace OrgBloom.Bot.BotServices.Helpers;

public static class StringHelper
{

    public static string GetEntrepreneurApplicationInfoForm(EntrepreneurResultDto dto)
        => $"Ism: {dto.User.FirstName}\n" +
        $"Familiya: {dto.User.LastName}\n" +
        $"Otasining ismi: {dto.User.Patronomyc}\n" +
        $"Yoshi: {TimeHelper.GetAge(dto.User.DateOfBirth)}" +
        $"Sohasi; {Enum.GetName(dto.User.Profession)}";

    public static string GetInvestmentApplicationInfoForm(InvestorResultDto dto)
        => $"Ism: {dto.User.FirstName}\n" +
        $"Familiya: {dto.User.LastName}\n" +
        $"Otasining ismi: {dto.User.Patronomyc}\n" +
        $"Yoshi: {TimeHelper.GetAge(dto.User.DateOfBirth)}" +
        $"Sohasi; {Enum.GetName(dto.User.Profession)}";
}
