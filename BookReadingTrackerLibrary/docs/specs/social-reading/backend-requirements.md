# Backend Requirements - Social Reading

## API Endpoints

- **POST /api/book-clubs/sessions** - Schedule session (BookClubSessionScheduled event)
- **PUT /api/book-clubs/sessions/{id}** - Update session
- **DELETE /api/book-clubs/sessions/{id}** - Cancel session
- **POST /api/book-clubs/sessions/{id}/participate** - Record participation (BookDiscussionParticipated event)
- **POST /api/book-clubs/sessions/{id}/invite** - Invite participants
- **GET /api/book-clubs/sessions** - Get upcoming sessions
- **GET /api/book-clubs/sessions/{id}/discussion-notes** - Get notes
- **POST /api/book-clubs** - Create book club
- **GET /api/book-clubs** - Get user's book clubs

## Domain Models

```csharp
public class BookClubSession
{
    public Guid Id { get; set; }
    public Guid BookClubId { get; set; }
    public Guid BookId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public List<string> Participants { get; set; }
    public List<string> DiscussionTopics { get; set; }
    public string ReadingSections { get; set; }
    public string Location { get; set; }
    public string Platform { get; set; }
    public string Organizer { get; set; }
    public SessionStatus Status { get; set; }
}

public enum SessionStatus { Scheduled, InProgress, Completed, Cancelled }

public class BookClubParticipation
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public Guid BookId { get; set; }
    public DateTime SessionDate { get; set; }
    public List<string> Participants { get; set; }
    public string DiscussionNotes { get; set; }
    public string InsightsShared { get; set; }
    public Guid? NextBookId { get; set; }
    public ParticipationLevel ParticipationLevel { get; set; }
}

public enum ParticipationLevel { Observer, Active, Leader }

public class BookClub
{
    public Guid Id { get; set; }
    public string ClubName { get; set; }
    public string Description { get; set; }
    public List<string> Members { get; set; }
    public string Organizer { get; set; }
    public DateTime CreatedDate { get; set; }
}
```

## Business Rules
1. Session reminders sent 24h and 1h before
2. Max 50 participants per session
3. Discussion notes saved per participant
4. Book club history maintained indefinitely
