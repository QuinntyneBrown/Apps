# Frontend Requirements - Reflection Journaling

## Pages/Views

### Reflection Prompts Library
**Route**: `/reflections/prompts`

**Components**:
- PromptGrid
- PromptCard
- CategoryFilter
- RandomPromptButton

**Features**:
- Browse prompts by category
- Random prompt generator
- Save favorites
- Start reflection from prompt

### Create Reflection Page
**Route**: `/reflections/create`

**Components**:
- RichTextEditor
- PromptDisplay (if from prompt)
- PrivacySelector
- ThemeTagger
- ConflictToggle

**Features**:
- Rich text editing with formatting
- Auto-save draft
- Word count
- Theme suggestions as you type
- Privacy level selection
- Mark as conflict-related

### Growth Timeline
**Route**: `/reflections/timeline`

**Components**:
- TimelineView
- GrowthMomentCard
- ProgressVisualization
- FilterControls

**Features**:
- Visual timeline of growth moments
- Filter by theme, type, date
- Highlight breakthroughs
- Progress trend visualization

### Weekly Review Interface
**Route**: `/reflections/weekly-review`

**Components**:
- WeeklyReviewForm
- HighlightsSection
- ChallengesSection
- GoalsSection
- SatisfactionRatingSlider

**Features**:
- Guided sections for highlights, challenges, goals
- Review previous week's goals
- Satisfaction rating (1-10)
- Save as draft
- Require both partners for joint review

### Reflection Detail View
**Component**: `ReflectionDetailPage`

**Display**:
- Full reflection content
- Associated prompt (if any)
- Themes tags
- Privacy status
- Creation date
- Edit/delete options (for author)

## Components

### RichTextEditor
**Features**:
- Bold, italic, underline
- Bullet and numbered lists
- Headings
- Character/word count
- Markdown support

### PromptCard
**Display**:
- Prompt text
- Category badge
- Difficulty indicator
- "Start Reflection" button

### GrowthMomentCard
**Display**:
- Date
- Description
- What changed
- Themes
- Partners involved

### ThemeTagger
**Features**:
- Autocomplete existing themes
- Create new themes
- Visual tag display
- Remove tags

## State Management

### reflectionSlice
```typescript
{
  reflections: {
    items: [],
    loading: boolean,
    error: string | null
  },
  prompts: {
    available: [],
    current: Prompt | null
  },
  draft: {
    content: string,
    metadata: {}
  },
  timeline: {
    growthMoments: [],
    loading: boolean
  },
  weeklyReview: {
    current: WeeklyReview | null,
    history: [],
    isDue: boolean
  }
}
```

## Real-time Features
- Collaborative editing for joint reflections
- Auto-save drafts every 30 seconds
- Notification when spouse completes weekly review

## Responsive Design
- Mobile: Full-screen editor
- Desktop: Side-by-side prompt and editor
- Tablet: Swipe between prompts
