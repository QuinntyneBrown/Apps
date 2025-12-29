# Frontend Requirements - Notes and Highlights

## Key Components

### Highlight Tool
- Text selection interface
- Color picker for highlights
- Context menu for quick highlight
- Highlight list view

### Note Editor
- Rich text editor
- Note type selector (summary, question, insight, action)
- Section/page reference input
- Attachment support

### Insights Library
- Grid of captured insights
- Filter by skill, topic, material
- Export to knowledge base
- Review mode for spaced repetition

### Reading Review
- Combined view of highlights and notes
- Chronological or by importance sort
- Export to PDF/Markdown
- Share with colleagues

## State Management
```typescript
interface NotesState {
  highlights: Highlight[];
  notes: Note[];
  insights: Insight[];
  activeHighlight: Highlight | null;
  editingNote: Note | null;
}
```
