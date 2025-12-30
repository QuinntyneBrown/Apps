// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core;

public class Vaccination
{
    public Guid VaccinationId { get; set; }
    public Guid PetId { get; set; }
    public Pet? Pet { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateAdministered { get; set; }
    public DateTime? NextDueDate { get; set; }
    public string? VetName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
