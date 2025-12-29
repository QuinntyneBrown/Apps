# Spot Management - Backend Requirements

## Domain Events
- **SpotDiscovered**: New location found
- **SpotDetailsRecorded**: Spot documented
- **SpotRated**: Quality rating assigned
- **SpotConditionsUpdated**: Current conditions logged
- **SecretSpotShared**: Spot shared with trusted user

## API Endpoints
- `POST /api/spots` - Create spot
- `GET /api/spots/nearby` - Find spots by location
- `PUT /api/spots/{id}/rate` - Rate spot
- `PUT /api/spots/{id}/conditions` - Update conditions
- `POST /api/spots/{id}/share` - Share with user

## Data Models
```csharp
FishingSpot {
    id: Guid
    name: string
    coordinates: GeoPoint
    waterType: WaterType
    access: AccessType
    structure: string[]
    depthRange: string
    averageRating: decimal
    isPrivate: boolean
    ownerId: Guid
}
```

## Business Rules
- GPS coordinates required and validated
- Duplicate spot detection (within 50m)
- Private spots only visible to owner and shared users
- Rating 1-5 stars
- Conditions updated max once per day per user
