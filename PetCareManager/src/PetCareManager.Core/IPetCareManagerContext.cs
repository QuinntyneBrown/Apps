// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace PetCareManager.Core;

public interface IPetCareManagerContext
{
    DbSet<Pet> Pets { get; set; }
    DbSet<VetAppointment> VetAppointments { get; set; }
    DbSet<Medication> Medications { get; set; }
    DbSet<Vaccination> Vaccinations { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
