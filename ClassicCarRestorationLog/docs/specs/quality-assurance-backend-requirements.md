# Quality Assurance - Backend Requirements

## Domain Events
- **QualityInspectionPerformed**: Restoration work quality assessed
- **ProfessionalAppraisalCompleted**: Expert evaluation received
- **ShowJudgingReceived**: Car show judging results recorded

## API Endpoints
- `POST /api/projects/{id}/inspections` - Create inspection
- `GET /api/projects/{id}/inspections` - List inspections
- `POST /api/projects/{id}/appraisals` - Record appraisal
- `POST /api/projects/{id}/show-results` - Log show judging

## Data Models

### Inspection
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "inspectionDate": "datetime",
    "areaInspected": "string",
    "qualityRating": "int", // 1-10
    "defectsFound": "array<string>",
    "correctionsNeeded": "string",
    "inspectorNotes": "string"
}
```

### Appraisal
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "appraiserName": "string",
    "appraisalDate": "datetime",
    "conditionRating": "enum[Excellent, VeryGood, Good, Fair, Poor]",
    "valueEstimate": "decimal",
    "strengths": "array<string>",
    "improvementSuggestions": "array<string>"
}
```

### ShowJudging
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "showName": "string",
    "judgingDate": "datetime",
    "category": "string",
    "scoreReceived": "decimal",
    "awardWon": "string",
    "judgeComments": "string"
}
```
