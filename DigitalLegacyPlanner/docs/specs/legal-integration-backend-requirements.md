# Legal Integration - Backend Requirements

## API Endpoints

#### POST /api/legal/attach-document
Attach legal document
- **Request Body**: `{ documentType, file, validityDate, jurisdiction }`
- **Events**: `LegalDocumentAttached`

#### POST /api/legal/notify-attorney
Share plan with attorney
- **Request Body**: `{ attorneyInfo, planVersion }`
- **Events**: `AttorneyNotified`

## Data Models

```csharp
public class LegalDocument
{
    public Guid Id { get; set; }
    public DocumentType Type { get; set; }
    public string FileUrl { get; set; }
    public DateTime ValidityDate { get; set; }
    public string Jurisdiction { get; set; }
}
```
