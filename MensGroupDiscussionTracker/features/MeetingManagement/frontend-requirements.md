# Frontend Requirements - Meeting Management

## Pages
- **Meeting Calendar** (`/meetings`): Calendar view of scheduled meetings
- **Meeting Detail** (`/meetings/{id}`): Full meeting info with RSVP
- **Schedule Meeting** (`/meetings/new`): Form to create new meeting

## Components
- **MeetingCard**: Display meeting info, RSVP status, attendance count
- **RSVPButton**: Quick RSVP with status indicator
- **AttendanceTracker**: Mark attendance during meeting
- **MeetingCalendar**: Calendar widget with meeting markers

## State Management
```typescript
{
  meetings: {
    upcoming: Meeting[],
    past: Meeting[],
    current: Meeting | null
  },
  myRsvps: { [meetingId]: 'yes' | 'no' | 'maybe' }
}
```

## Key Features
- One-click RSVP
- Meeting reminders (browser notifications)
- Live attendance counter
- Virtual meeting link integration
