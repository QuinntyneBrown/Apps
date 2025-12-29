# Frontend Requirements - Relationship Intelligence

## User Interface Components

### Relationship Dashboard
**Route**: `/relationships`

**Widgets**:
1. **Network Health Score**:
   - Overall network strength (0-100)
   - Trend indicator
   - Comparison to last month

2. **Relationship Distribution**:
   - Pie chart: Strong/Medium/Weak
   - Count per category
   - Clickable to filter

3. **Trending Relationships**:
   - Strengthening relationships (green arrow)
   - Weakening relationships (red arrow)
   - Stable relationships

4. **At-Risk Contacts**:
   - List of relationships becoming dormant
   - Days since last interaction
   - Quick follow-up action

5. **Strong Ties**:
   - Top 10 strongest relationships
   - Strength score bars
   - Quick view contact

### Relationship Strength Indicator
**Location**: Contact cards, detail view

**Visualization**:
- Circular progress bar (0-100)
- Color-coded: Green (75+), Yellow (40-74), Red (<40)
- Trend arrow (up/down/stable)
- Tooltip with factor breakdown

### Relationship Timeline
**Location**: Contact detail view

**Display**:
- Line chart of strength over time
- Interaction events overlaid
- Milestone markers
- Trend prediction (dotted line)

### Weak Ties Alert
**Location**: Dashboard, notifications

**Card Display**:
- Contact photo and name
- Current strength score
- Days since last interaction
- Decline percentage
- Re-engagement suggestions
- Quick actions: Schedule Follow-up, Send Message

### Relationship Milestones
**Location**: Contact detail, notification feed

**Display**:
- Milestone type icon
- Date and description
- Celebration prompt
- Suggested action (thank you note, special outreach)

### Network Insights
**Route**: `/relationships/insights`

**Insights**:
- "You have 5 relationships at risk of becoming dormant"
- "Your network strength has improved 12% this month"
- "You have 3 upcoming relationship anniversaries"
- "You haven't connected with mentors in 45 days"

**Recommendations**:
- Contacts to prioritize this week
- Suggested re-engagement strategies
- Network diversification opportunities

## State Management

```typescript
interface RelationshipIntelligenceState {
  networkHealth: {
    score: number;
    trend: 'up' | 'down' | 'stable';
    distribution: {
      strong: number;
      medium: number;
      weak: number;
    };
  };
  atRiskContacts: Contact[];
  strongTies: Contact[];
  milestones: Milestone[];
  insights: Insight[];
  loading: boolean;
}
```

## User Experience Features

### Visualizations
- Interactive charts (hover for details)
- Animated transitions
- Color-coded indicators
- Responsive design

### Notifications
- Weekly relationship health report
- Milestone celebration alerts
- At-risk contact warnings
- Strength improvement celebrations

### Gamification
- Relationship builder badge
- Network health streak
- Milestone achievements

### Smart Suggestions
- Re-engagement email templates
- Talking points for weak ties
- Celebration message templates for milestones

## Analytics Events
- Relationship strength viewed
- At-risk contact actioned
- Milestone celebrated
- Re-engagement initiated
