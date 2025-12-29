# Frontend Requirements - Social Reading

## Pages/Views

### 1. Book Clubs Page (`/book-clubs`)
- MyBookClubsGrid
- UpcomingSessionsList
- CreateBookClubButton
- JoinBookClubSection

### 2. Schedule Session Modal
- BookSelector
- DateTimePicker
- ParticipantsMultiSelect
- DiscussionTopicsInput
- ReadingSectionsInput
- LocationPlatformInput

### 3. Session Detail Page (`/book-clubs/sessions/{id}`)
- SessionInfo header
- ParticipantsList
- DiscussionTopics
- NotesEditor
- NextBookVoting

### 4. Book Club Detail Page (`/book-clubs/{id}`)
- ClubInfo
- MembersList
- SessionHistory
- CurrentBookBanner
- ScheduleNextButton

## UI Components

### SessionCard
- Book cover
- Session date and time
- Participant count
- Location/Platform badge
- Join/RSVP button
- Reminder toggle

### BookClubCard
- Club name and description
- Member count
- Next session info
- Recent activity
- Manage button

### DiscussionNotesEditor
- Rich text editor
- Topic tags
- Insights section
- Save draft
- Share with club

## State Management
```typescript
interface SocialReadingState {
  bookClubs: BookClub[];
  upcomingSessions: BookClubSession[];
  pastSessions: BookClubParticipation[];
  currentSession: BookClubSession | null;
}
```

## Actions
- createBookClub(club)
- scheduleSession(session)
- inviteParticipants(sessionId, participants)
- recordParticipation(sessionId, notes)
- fetchUpcomingSessions()
- fetchBookClubHistory(clubId)
