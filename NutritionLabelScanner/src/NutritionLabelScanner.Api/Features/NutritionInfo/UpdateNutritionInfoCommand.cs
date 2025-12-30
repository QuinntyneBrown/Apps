// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NutritionLabelScanner.Api.Features.NutritionInfo;

public record UpdateNutritionInfoCommand(
    Guid NutritionInfoId,
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

public class UpdateNutritionInfoCommandHandler : IRequestHandler<UpdateNutritionInfoCommand, NutritionInfoDto>
{
    private readonly INutritionLabelScannerContext _context;

    public UpdateNutritionInfoCommandHandler(INutritionLabelScannerContext context)
    {
        _context = context;
    }

    public async Task<NutritionInfoDto> Handle(UpdateNutritionInfoCommand request, CancellationToken cancellationToken)
    {
        var nutritionInfo = await _context.NutritionInfos
            .FirstOrDefaultAsync(x => x.NutritionInfoId == request.NutritionInfoId, cancellationToken)
            ?? throw new InvalidOperationException($"NutritionInfo with ID {request.NutritionInfoId} not found.");

        nutritionInfo.ProductId = request.ProductId;
        nutritionInfo.Calories = request.Calories;
        nutritionInfo.TotalFat = request.TotalFat;
        nutritionInfo.SaturatedFat = request.SaturatedFat;
        nutritionInfo.TransFat = request.TransFat;
        nutritionInfo.Cholesterol = request.Cholesterol;
        nutritionInfo.Sodium = request.Sodium;
        nutritionInfo.TotalCarbohydrates = request.TotalCarbohydrates;
        nutritionInfo.DietaryFiber = request.DietaryFiber;
        nutritionInfo.TotalSugars = request.TotalSugars;
        nutritionInfo.Protein = request.Protein;
        nutritionInfo.AdditionalNutrients = request.AdditionalNutrients;

        await _context.SaveChangesAsync(cancellationToken);

        return nutritionInfo.ToDto();
    }
}
