# Reporting - Frontend Requirements

## Pages

### Reports Dashboard (`/reports`)
- **Quick Reports**: This Week, This Month, This Year
- **Custom Report Builder**: Date range, metrics selection
- **Scheduled Reports**: Manage automated reports
- **Export Center**: Download previous reports

### Weekly Report View
- **Summary Stats**: MPG, cost, miles (large display)
- **Best/Worst Fill-Ups**: Highlight extremes
- **MPG Trend**: Mini chart for the week
- **Week-over-Week**: Comparison to last week
- **Key Insights**: Automated observations
- **Share Button**: Social media, email

### Annual Report View
- **Year in Review**: Hero stats (total spent, miles, MPG)
- **Monthly Breakdown**: Each month's performance
- **Achievements**: All milestones reached
- **Cost Analysis**: Where money went
- **Top Insights**: Biggest improvements/declines
- **Year-over-Year**: If multiple years available
- **Download PDF**: Formatted annual summary

### Custom Report Builder
- **Date Range Picker**: Start and end dates
- **Metric Selector**: Checkboxes for included data
- **Vehicle Selector**: Single or multiple vehicles
- **Grouping**: By month, week, trip type, etc.
- **Format**: PDF, CSV, Excel
- **Generate Button**

### Scheduled Reports
- **Create Schedule**: Report type, frequency, delivery
- **Active Schedules**: List with edit/delete
- **Delivery Preferences**: Email, push notification
- **Sample Preview**: See what report looks like

## UI Components

### ReportCard
- Report icon and title
- Period covered
- Key metrics preview
- Actions: View, Download, Share

### ReportVisualization
- Chart/graph based on report type
- Interactive elements
- Exportable as image
- Print-friendly styling

### InsightHighlight
- Icon for insight type
- Headline insight text
- Supporting details
- Related action suggestion

## Export Features

### PDF Reports
- Professional formatting
- Company/user branding
- Charts and graphs included
- Print-optimized layout
- Table of contents for annual

### CSV/Excel
- Raw data export
- Properly formatted columns
- Multiple sheets for annual
- Compatible with tax software

## State Management

```typescript
interface ReportingState {
  weeklyReport?: WeeklyReport;
  annualReport?: AnnualReport;
  customReports: CustomReport[];
  schedules: ReportSchedule[];
  loading: boolean;
  exportProgress?: number;
}
```
