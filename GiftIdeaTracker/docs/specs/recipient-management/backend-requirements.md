# Backend Requirements - Recipient Management

## Domain Events
- RecipientAdded
- RecipientPreferencesUpdated
- RecipientSizesRecorded

## API Endpoints
- POST /api/recipients - Add recipient
- PUT /api/recipients/{id} - Update recipient
- GET /api/recipients - List recipients
- PUT /api/recipients/{id}/preferences - Update preferences
- PUT /api/recipients/{id}/sizes - Record sizes

## Data Models
```typescript
Recipient {
  id: UUID,
  name: string,
  relationship: string,
  birthDate: Date,
  importantDates: Date[],
  preferences: string[],
  interests: string[],
  sizes: {clothing, shoe, ring},
  contactInfo: {email, phone, address}
}
```

## Business Logic
- Track kids' growth for size updates
- Auto-generate birthday occasions
- Suggest gift categories based on interests
