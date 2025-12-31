// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MotorcycleRideLog.Core;

public class Motorcycle
{
    public Guid MotorcycleId { get; set; }
    public Guid UserId { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int? Year { get; set; }
    public MotorcycleType Type { get; set; }
    public string? VIN { get; set; }
    public string? LicensePlate { get; set; }
    public int? CurrentMileage { get; set; }
    public string? Color { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
