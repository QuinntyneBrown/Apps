# Backend Requirements - Reporting

## API Endpoints

### GET /api/reports/weekly
Generate weekly BP summary report.
```json
Response: {
  "weekEnding": "2025-12-29",
  "averageBP": {"systolic": 125, "diastolic": 82},
  "readingsCount": 14,
  "readingsInGoal": 11,
  "trend": "stable",
  "highestReading": {"systolic": 138, "diastolic": 88, "date": "2025-12-27"},
  "lowestReading": {"systolic": 118, "diastolic": 76, "date": "2025-12-24"}
}
```
**Events:** WeeklyBPReportGenerated

### POST /api/reports/doctor
Generate comprehensive doctor report.
```json
Request: {
  "startDate": "2025-09-01",
  "endDate": "2025-12-29",
  "includeReadings": true,
  "includeMedications": true,
  "includeLifestyle": true,
  "format": "pdf"
}
Response: {
  "reportId": "uuid",
  "downloadUrl": "https://...",
  "expiresAt": "2025-12-30T00:00:00Z"
}
```
**Events:** DoctorReportGenerated

### GET /api/reports/monthly
Generate monthly progress report.
```json
Response: {
  "month": "2025-12",
  "averageBP": {"systolic": 124, "diastolic": 81},
  "improvementVsPrevious": {"systolic": -3, "diastolic": -2},
  "goalAchievement": 82,
  "consistency": 93,
  "insights": [...]
}
```
**Events:** MonthlyProgressReportCreated

### GET /api/reports/{reportId}/download
Download generated report (PDF).

## Domain Models

```csharp
public class Report : AggregateRoot
{
    public Guid ReportId { get; private set; }
    public Guid UserId { get; private set; }
    public ReportType Type { get; private set; }
    public DateRange Period { get; private set; }
    public string FileUrl { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime GeneratedAt { get; private set; }
}

public enum ReportType
{
    Weekly,
    Monthly,
    Doctor,
    Custom
}
```

## Report Templates

### Weekly Report (Email/PDF)
- Summary statistics
- Chart of week's readings
- Goal achievement
- Medication adherence
- Brief insights

### Doctor Report (PDF)
- Patient information
- Report period
- Complete reading table (all values)
- Average BP by time of day
- Trend charts
- Medication list and adherence
- Lifestyle factors summary
- Alerts triggered
- Statistical summary
- Notes section for doctor

### Monthly Progress Report
- Month overview
- Comparison to previous month
- Goal progress
- Achievements and milestones
- Lifestyle impact summary
- Recommendations for next month

## Business Rules

### BR-RP-001: Report Generation Limits
- Weekly reports auto-generated every Sunday
- Monthly reports auto-generated on 1st of month
- Custom reports: max 1 per hour per user

### BR-RP-002: Report Expiration
- Reports accessible for 30 days
- After expiration, can be regenerated

### BR-RP-003: Data Inclusion
- Only include data user has permission to access
- Anonymize if sharing outside HIPAA-covered entity

## PDF Generation

- Use library like iTextSharp or PdfSharp
- Include header with logo and patient info
- Professional medical report formatting
- Page numbers
- Generated date timestamp
- QR code for verification (optional)

## Database Schema

```sql
CREATE TABLE Reports (
    ReportId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ReportType NVARCHAR(50) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    FileUrl NVARCHAR(500),
    ExpiresAt DATETIME2,
    GeneratedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
```
