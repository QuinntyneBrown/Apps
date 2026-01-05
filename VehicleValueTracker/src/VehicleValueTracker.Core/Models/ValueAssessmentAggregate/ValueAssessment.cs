// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleValueTracker.Core;

/// <summary>
/// Represents a value assessment for a vehicle at a specific point in time.
/// </summary>
public class ValueAssessment
{
    /// <summary>
    /// Gets or sets the unique identifier for the value assessment.
    /// </summary>
    public Guid ValueAssessmentId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the vehicle.
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// Gets or sets the assessment date.
    /// </summary>
    public DateTime AssessmentDate { get; set; }

    /// <summary>
    /// Gets or sets the estimated market value.
    /// </summary>
    public decimal EstimatedValue { get; set; }

    /// <summary>
    /// Gets or sets the odometer reading at assessment.
    /// </summary>
    public decimal MileageAtAssessment { get; set; }

    /// <summary>
    /// Gets or sets the overall condition grade.
    /// </summary>
    public ConditionGrade ConditionGrade { get; set; }

    /// <summary>
    /// Gets or sets the source of the valuation (e.g., "KBB", "Edmunds", "Manual").
    /// </summary>
    public string? ValuationSource { get; set; }

    /// <summary>
    /// Gets or sets the exterior condition notes.
    /// </summary>
    public string? ExteriorCondition { get; set; }

    /// <summary>
    /// Gets or sets the interior condition notes.
    /// </summary>
    public string? InteriorCondition { get; set; }

    /// <summary>
    /// Gets or sets the mechanical condition notes.
    /// </summary>
    public string? MechanicalCondition { get; set; }

    /// <summary>
    /// Gets or sets the list of modifications or upgrades.
    /// </summary>
    public List<string> Modifications { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the list of known issues.
    /// </summary>
    public List<string> KnownIssues { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the depreciation amount since purchase.
    /// </summary>
    public decimal? DepreciationAmount { get; set; }

    /// <summary>
    /// Gets or sets the depreciation percentage.
    /// </summary>
    public decimal? DepreciationPercentage { get; set; }

    /// <summary>
    /// Gets or sets additional assessment notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the vehicle.
    /// </summary>
    public Vehicle? Vehicle { get; set; }

    /// <summary>
    /// Calculates depreciation based on purchase price.
    /// </summary>
    /// <param name="purchasePrice">The original purchase price.</param>
    public void CalculateDepreciation(decimal purchasePrice)
    {
        if (purchasePrice > 0)
        {
            DepreciationAmount = purchasePrice - EstimatedValue;
            DepreciationPercentage = Math.Round((DepreciationAmount.Value / purchasePrice) * 100, 2);
        }
    }

    /// <summary>
    /// Adds modifications to the assessment.
    /// </summary>
    /// <param name="modifications">The modifications to add.</param>
    public void AddModifications(IEnumerable<string> modifications)
    {
        Modifications.AddRange(modifications);
    }

    /// <summary>
    /// Adds known issues to the assessment.
    /// </summary>
    /// <param name="issues">The issues to add.</param>
    public void AddKnownIssues(IEnumerable<string> issues)
    {
        KnownIssues.AddRange(issues);
    }

    /// <summary>
    /// Updates the estimated value.
    /// </summary>
    /// <param name="newValue">The new estimated value.</param>
    /// <exception cref="ArgumentException">Thrown when value is negative.</exception>
    public void UpdateEstimatedValue(decimal newValue)
    {
        if (newValue < 0)
        {
            throw new ArgumentException("Estimated value cannot be negative.", nameof(newValue));
        }

        EstimatedValue = newValue;
    }
}
