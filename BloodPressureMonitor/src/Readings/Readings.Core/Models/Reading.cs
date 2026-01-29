namespace Readings.Core.Models;

public class Reading
{
    public Guid ReadingId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public int Systolic { get; private set; }
    public int Diastolic { get; private set; }
    public int Pulse { get; private set; }
    public DateTime RecordedAt { get; private set; }
    public string? Notes { get; private set; }

    private Reading() { }

    public Reading(Guid tenantId, Guid userId, int systolic, int diastolic, int pulse, DateTime recordedAt, string? notes = null)
    {
        if (systolic <= 0)
            throw new ArgumentException("Systolic pressure must be positive.", nameof(systolic));
        if (diastolic <= 0)
            throw new ArgumentException("Diastolic pressure must be positive.", nameof(diastolic));
        if (pulse <= 0)
            throw new ArgumentException("Pulse must be positive.", nameof(pulse));

        ReadingId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Systolic = systolic;
        Diastolic = diastolic;
        Pulse = pulse;
        RecordedAt = recordedAt;
        Notes = notes;
    }

    public void Update(int? systolic = null, int? diastolic = null, int? pulse = null, DateTime? recordedAt = null, string? notes = null)
    {
        if (systolic.HasValue)
        {
            if (systolic.Value <= 0)
                throw new ArgumentException("Systolic pressure must be positive.", nameof(systolic));
            Systolic = systolic.Value;
        }

        if (diastolic.HasValue)
        {
            if (diastolic.Value <= 0)
                throw new ArgumentException("Diastolic pressure must be positive.", nameof(diastolic));
            Diastolic = diastolic.Value;
        }

        if (pulse.HasValue)
        {
            if (pulse.Value <= 0)
                throw new ArgumentException("Pulse must be positive.", nameof(pulse));
            Pulse = pulse.Value;
        }

        if (recordedAt.HasValue)
            RecordedAt = recordedAt.Value;

        if (notes != null)
            Notes = notes;
    }
}
