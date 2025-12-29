# Digital Will - Backend Requirements

## API Endpoints

#### POST /api/digital-will
Create comprehensive digital will
- **Request Body**: `{ accountsCovered, beneficiaries, instructions, legalStatus }`
- **Events**: `DigitalWillCreated`

#### PUT /api/digital-will/{willId}
Update digital will
- **Request Body**: Updated will content
- **Events**: `WillUpdated`

#### POST /api/ethical-will
Record ethical will (non-legal wishes)
- **Request Body**: `{ content, wishes, valuesToPreserve }`
- **Events**: `EthicalWillRecorded`

#### POST /api/final-messages
Create final message for delivery
- **Request Body**: `{ recipients, messageContent, deliveryConditions }`
- **Events**: `FinalMessageCreated`

## Data Models

```csharp
public class DigitalWill
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int VersionNumber { get; set; }
    public List<Guid> AccountsCovered { get; set; }
    public List<Guid> Beneficiaries { get; set; }
    public string ComprehensiveInstructions { get; set; }
    public LegalStatus LegalStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }
}

public class FinalMessage
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<Guid> RecipientIds { get; set; }
    public string EncryptedMessage { get; set; }
    public DeliveryConditions Conditions { get; set; }
    public bool IsDelivered { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

## Business Logic
- Version control for will updates
- Beneficiary notification on updates
- Legal jurisdiction validation
- Comprehensive plan compilation
- PDF generation for legal documents
