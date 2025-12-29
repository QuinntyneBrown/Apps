# Backend Requirements - Security Auditing

## API Endpoints
- POST /api/audits/perform - Run comprehensive security audit
- GET /api/audits/history - Retrieve audit history
- GET /api/security/score - Get current security score
- GET /api/compliance/check/{accountId} - Check account compliance
- GET /api/vulnerabilities - List identified vulnerabilities

## Domain Events
```csharp
public class SecurityAuditPerformed : DomainEvent
{
    public Guid AuditId { get; set; }
    public List<Guid> AccountsAudited { get; set; }
    public int IssuesFound { get; set; }
    public decimal SecurityScore { get; set; }
    public DateTime Timestamp { get; set; }
}

public class VulnerabilityIdentified : DomainEvent
{
    public Guid VulnerabilityId { get; set; }
    public Guid AccountId { get; set; }
    public string VulnerabilityType { get; set; }
    public string Severity { get; set; }
    public List<string> RemediationSteps { get; set; }
}

public class SecurityScoreCalculated : DomainEvent
{
    public Guid ScoreId { get; set; }
    public decimal OverallScore { get; set; }
    public Dictionary<string, decimal> CategoryScores { get; set; }
    public string Trend { get; set; }
}
```

## Database Schema
```sql
CREATE TABLE SecurityAudits (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    AuditDate DATETIME2 NOT NULL,
    AccountsAudited INT NOT NULL,
    IssuesFound INT NOT NULL,
    SecurityScore DECIMAL(5,2) NOT NULL,
    AuditResults NVARCHAR(MAX), -- JSON
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE Vulnerabilities (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    AccountId UNIQUEIDENTIFIER NOT NULL,
    VulnerabilityType VARCHAR(100) NOT NULL,
    Severity VARCHAR(20) NOT NULL,
    Description NVARCHAR(1000),
    RemediationSteps NVARCHAR(MAX), -- JSON
    Status VARCHAR(20) NOT NULL,
    IdentifiedDate DATETIME2 NOT NULL,
    ResolvedDate DATETIME2
);

CREATE TABLE SecurityScores (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    CalculationDate DATETIME2 NOT NULL,
    OverallScore DECIMAL(5,2) NOT NULL,
    PasswordScore DECIMAL(5,2),
    MFAScore DECIMAL(5,2),
    BreachScore DECIMAL(5,2),
    RecoveryScore DECIMAL(5,2),
    Trend VARCHAR(20)
);
```

## Business Logic
- Calculate security score based on: password strength, 2FA adoption, breach exposure, password age
- Identify vulnerabilities by checking: weak passwords, missing 2FA, password reuse, old passwords
- Generate compliance reports against security best practices
- Track score improvements over time
