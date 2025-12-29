# Frontend Requirements - Notes & Knowledge Management

## User Interface Components

### Notes Section
**Location**: Contact detail view

**Display**:
- List of notes (chronological)
- Note type badges
- Search notes
- Filter by type/tags

### Add Note Form
**Fields**:
- Note content (rich text)
- Note type
- Privacy level
- Tags

### Conversation Topics
**Display**:
- Topics discussed with contact
- Interest level indicators
- Last discussed date
- Quick talking points

### Notes Search
**Route**: `/notes/search`

**Features**:
- Full-text search across all notes
- Filter by contact, type, date
- Highlight search terms
- Export results

## State Management

```typescript
interface NotesState {
  notes: Map<string, ContactNote[]>;
  topics: Map<string, ConversationTopic[]>;
  searchResults: ContactNote[];
}
```
