// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core;

public class Pet
{
    public Guid PetId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public PetType PetType { get; set; }
    public string? Breed { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Color { get; set; }
    public decimal? Weight { get; set; }
    public string? MicrochipNumber { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<VetAppointment> VetAppointments { get; set; } = new List<VetAppointment>();
    public ICollection<Medication> Medications { get; set; } = new List<Medication>();
    public ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
