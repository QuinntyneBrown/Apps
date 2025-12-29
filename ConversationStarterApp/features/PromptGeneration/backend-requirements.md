# Backend Requirements - Prompt Generation

## API Endpoints
- GET /api/prompts/generate - Generate prompt based on filters
- GET /api/prompts/{id} - Get specific prompt
- POST /api/prompts/favorites - Add to favorites
- POST /api/prompts/custom - Create custom prompt

## Domain Events
- PromptGenerated
- PromptViewed
- PromptSkipped
- PromptFavorited
- CustomPromptCreated

## Database Schema
```sql
CREATE TABLE ConversationPrompts (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    PromptText NVARCHAR(500),
    Category VARCHAR(50),
    DifficultyLevel INT,
    Context VARCHAR(50),
    UsageCount INT,
    IsActive BIT
);

CREATE TABLE UserFavorites (
    UserId UNIQUEIDENTIFIER,
    PromptId UNIQUEIDENTIFIER,
    FavoritedAt DATETIME2,
    PRIMARY KEY (UserId, PromptId)
);
```
