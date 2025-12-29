# Frontend Requirements - Interaction Tracking

## User Interface Components

### Interaction Timeline
**Location**: Contact detail view, Dashboard

**Layout**:
- Chronological timeline of interactions
- Filter by interaction type
- Search interactions by topic/notes
- Visual indicators for interaction type (icons)
- Sentiment indicators (color-coded)
- Expandable interaction cards

**Interaction Card Display**:
- Type icon and label
- Date and time
- Duration (if applicable)
- Location/medium
- Topics discussed (tags)
- Notes excerpt (expandable)
- Sentiment indicator
- Follow-up flag
- Edit/Delete actions

### Log Interaction Form
**Route**: `/interactions/new` or modal

**Form Fields**:
- Contact selector (autocomplete)
- Interaction type* (dropdown with icons)
- Date and time* (datetime picker)
- Duration (time input in minutes)
- Location/Medium
- Topics discussed (tag input)
- Notes (rich text editor)
- Sentiment (emoji selector: positive/neutral/negative)
- Follow-up needed (checkbox)
- Follow-up notes (textarea, conditional)

**Quick Log Mode**:
- Minimal fields for fast entry
- Contact, type, date, brief note
- Expand for full details

**Actions**:
- Save
- Save and Schedule Follow-up
- Cancel

### Schedule Meeting Interface
**Route**: `/meetings/new` or modal

**Form Fields**:
- Contact*
- Date and time*
- Location (with suggestions: coffee shop, office, virtual)
- Meeting purpose
- Agenda items (dynamic list)
- Preparation notes
- Sync to calendar (checkbox)

**Calendar Integration**:
- View available time slots
- Suggest times based on past meetings
- Send calendar invite
- Set reminder

### Meeting Completion Form
**Trigger**: After scheduled meeting time

**Form Fields**:
- Actual date/time
- Attendees (if others present)
- Duration
- Topics covered
- Outcomes
- Action items (creates follow-up tasks)
- Next steps
- Relationship impact (dropdown: strengthened|neutral|weakened)

**Auto-population**:
- Pre-fill from scheduled meeting
- Carry over agenda as topics

### Interaction Analytics Dashboard
**Route**: `/analytics/interactions`

**Widgets**:
- Total interactions this month
- Interactions by type (pie chart)
- Interaction frequency trend (line chart)
- Most interacted contacts (leaderboard)
- Average interaction duration
- Sentiment distribution

**Filters**:
- Date range
- Contact filter
- Interaction type filter

## State Management

```typescript
interface InteractionsState {
  interactions: {
    items: Interaction[];
    loading: boolean;
    error: string | null;
  };
  meetings: {
    upcoming: Meeting[];
    past: Meeting[];
    loading: boolean;
  };
  filters: {
    types: string[];
    dateRange: DateRange;
    sentiment: string;
  };
}
```

## User Experience Features

### Smart Suggestions
- Suggest interaction type based on contact relationship
- Auto-suggest topics from previous interactions
- Recommend follow-up timing based on history

### Quick Actions
- Quick log from contact list
- Voice-to-text for notes
- Photo attachment for meeting notes

### Notifications
- Upcoming meeting reminders
- Post-meeting completion prompts
- Weekly interaction summary

### Mobile Optimizations
- Voice recording for meeting notes
- Quick log widget
- Camera for whiteboard/notes capture
