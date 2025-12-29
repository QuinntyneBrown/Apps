# Frontend Requirements - Follow-Up Management

## User Interface Components

### Follow-Up Dashboard
**Route**: `/followups`

**Layout**:
- Tabs: Due Today, Upcoming, Completed, Missed, Suggestions
- List of follow-ups grouped by date
- Priority indicators (color-coded)
- Quick complete button
- Bulk actions

**Follow-Up Card**:
- Contact name and photo
- Due date (with overdue indicator)
- Priority badge
- Reason and context
- Quick actions: Complete, Snooze, Edit, Delete

### Due Today Widget
**Location**: Dashboard home

**Display**:
- Count of follow-ups due today
- List of top 5 urgent follow-ups
- Quick complete checkboxes
- View all button

### Schedule Follow-Up Form
**Modal/Route**: `/followups/new`

**Fields**:
- Contact* (autocomplete)
- Due date* (date picker with quick options: tomorrow, next week, etc.)
- Follow-up type (call, email, coffee, check-in)
- Reason
- Context notes
- Priority

### Follow-Up Suggestions
**Route**: `/followups/suggestions`

**Layout**:
- AI-generated follow-up recommendations
- Reason for suggestion
- Suggested timing
- Talking points
- Accept/Dismiss buttons

**Suggestion Card**:
- Contact details
- Days since last interaction
- Relationship strength indicator
- Suggested approach
- One-click schedule

### Completion Form
**Modal**

**Fields**:
- Completion date (default: today)
- Method used
- Outcome notes
- Schedule next follow-up (checkbox + date)

## State Management

```typescript
interface FollowUpsState {
  followUps: {
    dueToday: FollowUp[];
    upcoming: FollowUp[];
    completed: FollowUp[];
    missed: FollowUp[];
    loading: boolean;
  };
  suggestions: {
    items: FollowUpSuggestion[];
    loading: boolean;
  };
  stats: {
    completionRate: number;
    avgResponseTime: number;
  };
}
```

## User Experience Features

### Notifications
- Browser notifications for due follow-ups
- Email digest of upcoming follow-ups
- Mobile push notifications

### Quick Actions
- Swipe to complete (mobile)
- Right-click context menu
- Keyboard shortcuts

### Smart Features
- Auto-suggest follow-up date based on contact
- Pre-fill context from last interaction
- Batch follow-up creation after events

### Gamification
- Follow-up streak tracker
- Completion rate badge
- Weekly goals
