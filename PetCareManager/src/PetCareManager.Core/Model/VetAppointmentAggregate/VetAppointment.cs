// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core;

public class VetAppointment
{
    public Guid VetAppointmentId { get; set; }
    public Guid PetId { get; set; }
    public Pet? Pet { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? VetName { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public decimal? Cost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
