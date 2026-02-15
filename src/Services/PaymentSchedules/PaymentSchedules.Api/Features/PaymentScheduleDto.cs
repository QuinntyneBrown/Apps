using PaymentSchedules.Core.Models;

namespace PaymentSchedules.Api.Features;

public record PaymentScheduleDto(
    Guid ScheduleId,
    Guid OfferId,
    int PaymentNumber,
    DateTime DueDate,
    decimal PrincipalAmount,
    decimal InterestAmount,
    decimal TotalPayment,
    decimal RemainingBalance,
    DateTime CreatedAt);

public static class PaymentScheduleExtensions
{
    public static PaymentScheduleDto ToDto(this PaymentSchedule schedule)
    {
        return new PaymentScheduleDto(
            schedule.ScheduleId,
            schedule.OfferId,
            schedule.PaymentNumber,
            schedule.DueDate,
            schedule.PrincipalAmount,
            schedule.InterestAmount,
            schedule.TotalPayment,
            schedule.RemainingBalance,
            schedule.CreatedAt);
    }
}
