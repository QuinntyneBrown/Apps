# Community Engagement - Backend Requirements

## Domain Events
- **CarShowAttended**: Restored vehicle displayed at show
- **ClubMembershipJoined**: User joined classic car club
- **RestorationStoryShared**: Project experience shared with community

## API Endpoints
- `POST /api/projects/{id}/shows` - Log show attendance
- `GET /api/shows` - List upcoming shows
- `POST /api/clubs` - Add club membership
- `GET /api/clubs` - List memberships
- `POST /api/projects/{id}/share` - Share restoration story

## Data Models

### ShowAttendance
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "showName": "string",
    "showDate": "datetime",
    "location": "string",
    "attendance": "int",
    "awardsWon": "array<string>",
    "feedbackReceived": "string"
}
```

### ClubMembership
```csharp
{
    "id": "guid",
    "clubName": "string",
    "joinDate": "datetime",
    "membershipFee": "decimal",
    "benefits": "array<string>",
    "contactInfo": "string"
}
```
