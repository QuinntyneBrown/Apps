# Frontend Requirements - Communication Tools

## Pages/Views

### Daily Prompt Page
**Route**: `/communication/daily-prompt`

**Components**:
- PromptDisplay
- PromptAnswerForm
- SpouseAnswerView (if they've answered)
- NextPromptButton

**Features**:
- Display daily conversation prompt
- Answer prompt individually
- View spouse's answer (if shared)
- Navigate to previous prompts
- Skip to next prompt

### Joint Journal Editor
**Route**: `/communication/joint-journal/create`

**Components**:
- CollaborativeEditor
- OnlineIndicator (show if spouse is viewing)
- SaveDraftButton
- CompleteButton

**Features**:
- Real-time collaborative editing
- Show cursor positions of both authors
- Track contributions by each person
- Auto-save drafts
- Mark as complete when both agree

### Communication Goals Dashboard
**Route**: `/communication/goals`

**Components**:
- ActiveGoalsList
- GoalProgressCard
- SetNewGoalButton
- CompletedGoalsArchive

**Features**:
- Display active communication goals
- Track progress toward each goal
- Set new goals together
- View completed goals history
- Get reminders about goals

### Communication Wins Gallery
**Route**: `/communication/wins`

**Components**:
- WinCard
- RecordWinButton
- SkillsFilter
- CelebrationAnimation

**Features**:
- Browse recorded communication successes
- Record new wins
- Filter by skills used
- Celebrate milestones
- Share wins with spouse

## Components

### PromptDisplay
**Props**: `prompt` (object)
**Display**: Prompt text, category, difficulty
**Styling**: Card with category color

### CollaborativeEditor
**Features**:
- Rich text editing
- Real-time sync
- Cursor indicators
- Change highlighting
- Version history

**Props**:
- `journalId`
- `currentUserId`
- `spouseId`
- `onSave`
- `onComplete`

### GoalProgressCard
**Display**:
- Goal description
- Target behavior
- Progress indicator
- Success criteria checklist
- Timeline countdown

### WinCard
**Display**:
- What happened (summary)
- Skills used (tags)
- Outcome
- Celebration notes
- Date recorded

## State Management

### communicationSlice
```typescript
{
  prompts: {
    daily: Prompt | null,
    history: [],
    loading: boolean
  },
  responses: {
    mine: [],
    spouses: []
  },
  jointJournals: {
    active: JointJournal | null,
    drafts: [],
    completed: []
  },
  goals: {
    active: [],
    completed: [],
    progress: {}
  },
  wins: {
    items: [],
    recentCount: 0
  }
}
```

## Real-time Features

### WebSocket Events
- `joint-journal.edit`: Sync edits in real-time
- `joint-journal.cursor`: Show spouse's cursor position
- `prompt.answered`: Notify when spouse answers prompt
- `goal.progress`: Update goal progress live

## Responsive Design
- Mobile: Stacked layout for prompts and answers
- Desktop: Side-by-side view for collaborative editing
- Tablet: Optimized for joint viewing during conversations

## Accessibility
- Screen reader support for collaborative editing
- Keyboard shortcuts for editor actions
- High contrast mode for readability
