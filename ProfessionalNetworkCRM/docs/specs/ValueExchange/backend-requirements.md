# Backend Requirements - Value Exchange

## Domain Events
- ValueProvided
- ValueReceived
- ReciprocityBalanceCalculated

## API Endpoints

### POST /api/value-exchanges/provided
Log value provided to contact.

**Request Body**:
```json
{
  "contactId": "guid",
  "valueType": "introduction|advice|resource|referral|support",
  "description": "string",
  "date": "datetime"
}
```

**Response**: `201 Created`
**Events Published**: `ValueProvided`

### POST /api/value-exchanges/received
Log value received from contact.

**Response**: `201 Created`
**Events Published**: `ValueReceived`

### GET /api/value-exchanges/{contactId}/balance
Get reciprocity balance.

**Response**: `200 OK`
```json
{
  "contactId": "guid",
  "valueProvided": "integer",
  "valueReceived": "integer",
  "balance": "number",
  "balanceStatus": "balanced|owe|owed",
  "recommendations": ["string"]
}
```

**Events Published**: `ReciprocityBalanceCalculated`

## Data Models

### ValueExchange Entity
```csharp
public class ValueExchange : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid ContactId { get; private set; }
    public ValueType Type { get; private set; }
    public ValueDirection Direction { get; private set; }
    public string Description { get; private set; }
    public DateTime Date { get; private set; }
}
```

### ReciprocityBalance Value Object
```csharp
public class ReciprocityBalance
{
    public int ValueProvided { get; private set; }
    public int ValueReceived { get; private set; }
    public decimal Balance { get; private set; }

    public static ReciprocityBalance Calculate(
        List<ValueExchange> exchanges
    ) { }
}
```
