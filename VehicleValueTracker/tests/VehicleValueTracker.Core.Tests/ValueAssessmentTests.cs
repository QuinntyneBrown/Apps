// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleValueTracker.Core.Tests;

public class ValueAssessmentTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesValueAssessment()
    {
        // Arrange
        var assessmentId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var assessmentDate = new DateTime(2024, 3, 15);
        var estimatedValue = 28000m;
        var mileageAtAssessment = 25000m;
        var conditionGrade = ConditionGrade.Good;
        var valuationSource = "KBB";
        var exteriorCondition = "Minor scratches";
        var interiorCondition = "Clean";
        var mechanicalCondition = "Excellent";

        // Act
        var assessment = new ValueAssessment
        {
            ValueAssessmentId = assessmentId,
            VehicleId = vehicleId,
            AssessmentDate = assessmentDate,
            EstimatedValue = estimatedValue,
            MileageAtAssessment = mileageAtAssessment,
            ConditionGrade = conditionGrade,
            ValuationSource = valuationSource,
            ExteriorCondition = exteriorCondition,
            InteriorCondition = interiorCondition,
            MechanicalCondition = mechanicalCondition
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assessment.ValueAssessmentId, Is.EqualTo(assessmentId));
            Assert.That(assessment.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(assessment.AssessmentDate, Is.EqualTo(assessmentDate));
            Assert.That(assessment.EstimatedValue, Is.EqualTo(estimatedValue));
            Assert.That(assessment.MileageAtAssessment, Is.EqualTo(mileageAtAssessment));
            Assert.That(assessment.ConditionGrade, Is.EqualTo(conditionGrade));
            Assert.That(assessment.ValuationSource, Is.EqualTo(valuationSource));
            Assert.That(assessment.ExteriorCondition, Is.EqualTo(exteriorCondition));
            Assert.That(assessment.InteriorCondition, Is.EqualTo(interiorCondition));
            Assert.That(assessment.MechanicalCondition, Is.EqualTo(mechanicalCondition));
        });
    }

    [Test]
    public void CalculateDepreciation_ValidPurchasePrice_CalculatesDepreciation()
    {
        // Arrange
        var assessment = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            AssessmentDate = DateTime.UtcNow,
            EstimatedValue = 28000m,
            MileageAtAssessment = 25000m,
            ConditionGrade = ConditionGrade.Good
        };

        // Act
        assessment.CalculateDepreciation(35000m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assessment.DepreciationAmount, Is.EqualTo(7000m));
            Assert.That(assessment.DepreciationPercentage, Is.EqualTo(20.0m));
        });
    }

    [Test]
    public void CalculateDepreciation_ZeroPurchasePrice_DoesNotCalculate()
    {
        // Arrange
        var assessment = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            AssessmentDate = DateTime.UtcNow,
            EstimatedValue = 28000m,
            MileageAtAssessment = 25000m,
            ConditionGrade = ConditionGrade.Good
        };

        // Act
        assessment.CalculateDepreciation(0m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assessment.DepreciationAmount, Is.Null);
            Assert.That(assessment.DepreciationPercentage, Is.Null);
        });
    }

    [Test]
    public void AddModifications_AddsModificationsToList()
    {
        // Arrange
        var assessment = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            AssessmentDate = DateTime.UtcNow,
            EstimatedValue = 30000m,
            MileageAtAssessment = 20000m,
            ConditionGrade = ConditionGrade.VeryGood
        };

        var modifications = new[] { "Aftermarket exhaust", "Custom wheels", "Tinted windows" };

        // Act
        assessment.AddModifications(modifications);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assessment.Modifications, Has.Count.EqualTo(3));
            Assert.That(assessment.Modifications, Contains.Item("Aftermarket exhaust"));
            Assert.That(assessment.Modifications, Contains.Item("Custom wheels"));
            Assert.That(assessment.Modifications, Contains.Item("Tinted windows"));
        });
    }

    [Test]
    public void AddKnownIssues_AddsIssuesToList()
    {
        // Arrange
        var assessment = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            AssessmentDate = DateTime.UtcNow,
            EstimatedValue = 25000m,
            MileageAtAssessment = 30000m,
            ConditionGrade = ConditionGrade.Fair
        };

        var issues = new[] { "Minor paint chips", "Worn tires", "AC needs service" };

        // Act
        assessment.AddKnownIssues(issues);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assessment.KnownIssues, Has.Count.EqualTo(3));
            Assert.That(assessment.KnownIssues, Contains.Item("Minor paint chips"));
            Assert.That(assessment.KnownIssues, Contains.Item("Worn tires"));
            Assert.That(assessment.KnownIssues, Contains.Item("AC needs service"));
        });
    }

    [Test]
    public void UpdateEstimatedValue_ValidValue_UpdatesValue()
    {
        // Arrange
        var assessment = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            AssessmentDate = DateTime.UtcNow,
            EstimatedValue = 28000m,
            MileageAtAssessment = 25000m,
            ConditionGrade = ConditionGrade.Good
        };

        // Act
        assessment.UpdateEstimatedValue(30000m);

        // Assert
        Assert.That(assessment.EstimatedValue, Is.EqualTo(30000m));
    }

    [Test]
    public void UpdateEstimatedValue_NegativeValue_ThrowsArgumentException()
    {
        // Arrange
        var assessment = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            AssessmentDate = DateTime.UtcNow,
            EstimatedValue = 28000m,
            MileageAtAssessment = 25000m,
            ConditionGrade = ConditionGrade.Good
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => assessment.UpdateEstimatedValue(-1000m));
        Assert.That(ex.Message, Does.Contain("Estimated value cannot be negative"));
    }

    [Test]
    public void ConditionGrade_AllValuesCanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new ValueAssessment { ConditionGrade = ConditionGrade.Excellent }, Throws.Nothing);
            Assert.That(() => new ValueAssessment { ConditionGrade = ConditionGrade.VeryGood }, Throws.Nothing);
            Assert.That(() => new ValueAssessment { ConditionGrade = ConditionGrade.Good }, Throws.Nothing);
            Assert.That(() => new ValueAssessment { ConditionGrade = ConditionGrade.Fair }, Throws.Nothing);
            Assert.That(() => new ValueAssessment { ConditionGrade = ConditionGrade.Poor }, Throws.Nothing);
        });
    }

    [Test]
    public void Modifications_InitializesAsEmptyList()
    {
        // Arrange & Act
        var assessment = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            AssessmentDate = DateTime.UtcNow,
            EstimatedValue = 30000m,
            MileageAtAssessment = 20000m,
            ConditionGrade = ConditionGrade.Good
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assessment.Modifications, Is.Not.Null);
            Assert.That(assessment.Modifications, Is.Empty);
        });
    }

    [Test]
    public void KnownIssues_InitializesAsEmptyList()
    {
        // Arrange & Act
        var assessment = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            AssessmentDate = DateTime.UtcNow,
            EstimatedValue = 30000m,
            MileageAtAssessment = 20000m,
            ConditionGrade = ConditionGrade.Good
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(assessment.KnownIssues, Is.Not.Null);
            Assert.That(assessment.KnownIssues, Is.Empty);
        });
    }

    [Test]
    public void UpdateEstimatedValue_ZeroValue_IsValid()
    {
        // Arrange
        var assessment = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            AssessmentDate = DateTime.UtcNow,
            EstimatedValue = 15000m,
            MileageAtAssessment = 50000m,
            ConditionGrade = ConditionGrade.Poor
        };

        // Act & Assert
        Assert.DoesNotThrow(() => assessment.UpdateEstimatedValue(0m));
        Assert.That(assessment.EstimatedValue, Is.EqualTo(0m));
    }
}
