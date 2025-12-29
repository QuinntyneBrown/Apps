# Frontend Requirements - Contact Segmentation

## User Interface Components

### Segments Manager
**Route**: `/segments`

**Display**:
- List of saved segments
- Contact count per segment
- Last used date
- Quick filter by segment

### Create Segment Form
**Fields**:
- Segment name
- Criteria builder (visual query builder)
- Preview of matching contacts
- Save segment

### Bulk Outreach Tool
**Features**:
- Select segment
- Message template editor
- Personalization field insertion
- Preview messages
- Schedule or send

## State Management

```typescript
interface SegmentationState {
  segments: Segment[];
  currentSegment: Segment | null;
  campaigns: Campaign[];
}
```
