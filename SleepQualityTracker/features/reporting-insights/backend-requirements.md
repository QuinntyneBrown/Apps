# Backend Requirements - Reporting & Insights

## API Endpoints

### GET /api/reports/weekly/{userId}
Get weekly sleep report
- **Query Parameters**: weekEndingDate
- **Events**: `WeeklySleepReportGenerated`

### GET /api/reports/monthly/{userId}
Get monthly sleep report

### POST /api/reports/generate
Generate report on demand

### GET /api/insights/{userId}
Get personalized insights
- **Events**: `SleepInsightGenerated`

### POST /api/reports/export
Export sleep data
- **Query Parameters**: format (CSV, PDF), dateRange
- **Response**: Downloadable file

## Domain Models
### WeeklySleepReport: Id, UserId, WeekEndingDate, AvgDuration, AvgQuality, PatternsIdentified, Achievements, Recommendations
### SleepInsight: Id, UserId, InsightType, Recommendation, ExpectedBenefit, Priority, GeneratedAt

## Business Logic
- Generate weekly reports every Monday morning
- Minimum 7 days data for meaningful insights
- Prioritize recommendations by impact potential
- Include: avg duration, quality score, patterns, achievements, recommendations
- Export includes all historical data with timestamps

## Events
WeeklySleepReportGenerated, SleepInsightGenerated with report data

## Database Schema
WeeklySleepReports table, SleepInsights table

## Performance
- Report generation: < 30 seconds
- Insights calculation: < 10 seconds
- Export generation: < 1 minute for 1 year data
