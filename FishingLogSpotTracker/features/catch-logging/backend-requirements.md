# Catch Logging - Backend Requirements

## Domain Events
- **FishCaught**: Fish caught and logged
- **PersonalBestRecorded**: New personal record
- **FishReleased**: Fish returned to water
- **FishKept**: Fish retained
- **CatchPhotoTaken**: Photo captured

## API Endpoints
- `POST /api/trips/{id}/catches` - Log catch
- `GET /api/catches` - List catches
- `POST /api/catches/{id}/photo` - Upload photo
- `GET /api/catches/personal-bests` - Get records

## Data Models
```csharp
Catch {
    id: Guid
    tripId: Guid
    species: string
    length: decimal
    weight: decimal
    catchTime: DateTime
    location: GeoPoint
    lureUsed: string
    waterDepth: decimal
    photoUrl: string
    released: boolean
}
```

## Business Rules
- Species must be from validated list
- Length/weight must be positive
- Photos compressed on upload
- Personal bests auto-detected
- Legal limits enforced per jurisdiction
