# Interviews - Frontend Requirements

## Overview
The Interviews UI provides a calendar-based interface for scheduling, preparing for, and tracking job interviews.

## Pages/Views

### 1. Interviews Calendar
**Route:** `/interviews`

**Features:**
- Monthly calendar view with scheduled interviews
- Timeline view for upcoming interviews
- Interview prep checklist
- Quick actions (reschedule, complete, thank you note)

### 2. Schedule Interview Form
**Route:** `/interviews/new`

**Form Fields:**
- Application Selection* (from active applications)
- Interview Type* (dropdown)
- Date & Time* (datetime picker with timezone)
- Duration* (number input, minutes)
- Location/Video Link (text input)
- Interviewers (multi-input)
- Preparation Checklist (checkboxes)

### 3. Interview Detail View
**Route:** `/interviews/:id`

**Sections:**
- Interview header (type, company, date/time)
- Interviewer information
- Preparation checklist and notes
- Post-interview feedback
- Thank you note tracking
- Related application details

### 4. Interview Preparation
**Features:**
- Research checklist
- Questions to prepare
- Materials needed
- Company information
- Interviewer LinkedIn profiles

### 5. Post-Interview Follow-up
**Features:**
- Performance self-rating
- Interview notes
- Next steps tracking
- Thank you note reminder
- Follow-up schedule

## Components

### InterviewCalendar
- Month/week/day views
- Interview markers with color coding
- Click to view details
- Drag to reschedule

### InterviewCard
- Interview type badge
- Date/time display
- Company and role
- Preparation status
- Quick actions

### PreparationChecklist
- Research company (checkbox)
- Prepare questions (checkbox)
- Review resume (checkbox)
- Gather materials (checkbox)
- Progress indicator

### InterviewTimeline
- Chronological list of upcoming interviews
- Countdown to next interview
- Preparation reminders
- Past interviews archive

## State Management

**interviewsSlice:**
```typescript
{
  interviews: Interview[],
  selectedDate: Date,
  viewMode: 'calendar' | 'timeline' | 'list',
  filters: {
    status: string[],
    interviewType: string[]
  },
  loading: boolean,
  error: string | null
}
```

## UI/UX Requirements

- Calendar integration (Google, Outlook)
- Email reminders 24 hours before
- Push notifications for upcoming interviews
- Timezone conversion support
- Drag-and-drop rescheduling
- Mobile-responsive calendar

## Accessibility

- Keyboard navigation for calendar
- Screen reader support for date picker
- ARIA labels for interview cards
- Focus management for modals

## Testing Requirements

- Interview scheduling flow
- Calendar interactions
- Reminder notifications
- Reschedule functionality
- Thank you note tracking
