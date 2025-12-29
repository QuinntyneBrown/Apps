# Frontend Requirements - Appreciation Expression

## Pages/Views

### Express Appreciation Page
**Route**: `/appreciation/express`

**Components**:
- AppreciationForm
- TypeSelector (Words/Actions/Qualities)
- IntensitySlider (1-5 hearts)
- SpecificInstanceTextarea

**Features**:
- Visual type selection with icons
- Interactive intensity selector
- Character counter
- Suggestions based on spouse's love language
- Preview before sending

### Appreciation Timeline
**Route**: `/appreciation/timeline`

**Components**:
- AppreciationCard
- ReciprocityIndicator
- FilterControls

**Features**:
- Chronological display of sent and received appreciations
- Visual indicators for reciprocated appreciations
- Filter by type, direction, date
- Respond to appreciation inline

### Love Language Dashboard
**Route**: `/love-language`

**Components**:
- LanguageBreakdownChart (pie or radar chart)
- PrimaryLanguageCard
- PartnerLanguageCard
- TipsAndSuggestions

**Features**:
- Visual representation of language breakdown
- Confidence score display
- Spouse's love language (if analyzed)
- Actionable suggestions
- Educational content about each language

## Components

### AppreciationCard
**Display**:
- Expresser/recipient
- Type badge (Words/Actions/Qualities)
- Intensity (heart icons)
- Specific instance content
- Timestamp
- Reciprocation status

### IntensitySelector
**Component**: Heart rating system (1-5)
**Interactive**: Click or drag to select
**Visual feedback**: Filled vs outlined hearts

### TypeBadge
**Variants**:
- Words: Speech bubble icon, orange
- Actions: Hand icon, blue
- Qualities: Star icon, purple

## State Management

### appreciationSlice
```typescript
{
  appreciations: {
    items: [],
    loading: boolean,
    error: string | null
  },
  loveLanguage: {
    profile: LoveLanguageProfile | null,
    suggestions: [],
    loading: boolean
  },
  reciprocity: {
    balance: number,
    metrics: {}
  }
}
```

## Real-time Features
- Live notification when appreciation received
- Update timeline when spouse reciprocates
- Love language profile updates

## Responsive Design
- Mobile: Stacked form, swipe between types
- Desktop: Side-by-side comparison of love languages
