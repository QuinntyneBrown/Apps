// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.NutritionInfo;

public record CreateNutritionInfoCommand(
    Guid ProductId,
    int Calories,
    decimal TotalFat,
    decimal? SaturatedFat,
    decimal? TransFat,
    decimal? Cholesterol,
    decimal Sodium,
    decimal TotalCarbohydrates,
    decimal? DietaryFiber,
    decimal? TotalSugars,
    decimal Protein,
    string? AdditionalNutrients) : IRequest<NutritionInfoDto>;

public class CreateNutritionInfoCommandHandler : IRequestHandler<CreateNutritionInfoCommand, NutritionInfoDto>
{
    private readonly INutritionLabelScannerContext _context;

    public CreateNutritionInfoCommandHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<NutritionInfoDto> Handle(CreateNutritionInfoCommand request, CancellationToken cancellationToken)
    {
        var nutritionInfo = new Core.NutritionInfo
        {
            NutritionInfoId = Guid.NewGuid(),
            ProductId = request.ProductId,
            Calories = request.Calories,
            TotalFat = request.TotalFat,
            SaturatedFat = request.SaturatedFat,
            TransFat = request.TransFat,
            Cholesterol = request.Cholesterol,
            Sodium = request.Sodium,
            TotalCarbohydrates = request.TotalCarbohydrates,
            DietaryFiber = request.DietaryFiber,
            TotalSugars = request.TotalSugars,
            Protein = request.Protein,
            AdditionalNutrients = request.AdditionalNutrients,
            CreatedAt = DateTime.UtcNow
        };

        _context.NutritionInfos.Add(nutritionInfo);
        await _context.SaveChangesAsync(cancellationToken);

        return nutritionInfo.ToDto();
    }
}
