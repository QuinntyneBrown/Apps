# Frontend Requirements - Networking Event Management

## User Interface Components

### Events List
**Route**: `/events`

**Display**:
- List of attended events
- Event cards with stats
- Connections made count
- Follow-up completion rate

### Log Event Form
**Fields**:
- Event name
- Event type (conference, meetup, seminar, etc.)
- Date
- Location
- Contacts met (multi-select)
- Notes
- ROI assessment

### Event Detail View
**Sections**:
- Event info
- Contacts met list
- Follow-ups generated
- Outcomes tracked
- ROI calculation

### Post-Event Follow-Up Tool
**Features**:
- Batch follow-up creation
- Personalized talking points
- Event context auto-fill
- Schedule all with one click

## State Management

```typescript
interface EventsState {
  events: NetworkingEvent[];
  contactsMetAtEvents: Map<string, Contact[]>;
  followUpCompletion: Map<string, number>;
}
```
