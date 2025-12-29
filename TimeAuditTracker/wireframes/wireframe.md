# Wireframes - Time Audit & Tracker

## 1. Dashboard / Home Screen

```
+----------------------------------------------------------+
|  Time Audit & Tracker                    [Profile] [⚙️]  |
+----------------------------------------------------------+
|  [Today] [Week] [Month]                    [+] Log Time  |
+----------------------------------------------------------+
|                                                            |
|  Daily Summary - December 29, 2025                        |
|  +------------------------------------------------------+ |
|  | Total Tracked: 14h 30m        [92% Complete]        | |
|  |                                                      | |
|  | Productive: 10h 45m (74%)    |||||||||||||||||       | |
|  | Unproductive: 3h 45m (26%)   ||||||                  | |
|  +------------------------------------------------------+ |
|                                                            |
|  Category Breakdown                                       |
|  +------------------------+  +-------------------------+  |
|  |   📊 Pie Chart        |  |  Top Categories:        |  |
|  |                        |  |  ● Work      8h 30m    |  |
|  |    [Work]  [Personal]  |  |  ● Personal  3h 15m    |  |
|  |    [Health] [Other]    |  |  ● Health    2h 45m    |  |
|  +------------------------+  +-------------------------+  |
|                                                            |
|  Timeline View                                            |
|  +------------------------------------------------------+ |
|  | 6am |8am |10am|12pm|2pm |4pm |6pm |8pm |10pm|12am    | |
|  | [Sleep][Work----][Lunch][Work--][Gym][Dinner][Relax] | |
|  |        ↑ Click to edit                               | |
|  +------------------------------------------------------+ |
|                                                            |
|  ⚠️ Budget Alert: Work category exceeded by 2h 15m       |
|  💡 Insight: You're most productive between 9am-11am     |
+----------------------------------------------------------+
```

## 2. Time Entry Form (Modal)

```
+------------------------------------------+
|  Log Time Entry                      [X] |
+------------------------------------------+
|                                          |
|  Activity Type *                         |
|  [Client Meeting__________________]  🔍  |
|  └─ Recent: Stand-up, Email, Design      |
|                                          |
|  Category *                              |
|  [▼ Work                     ] 🟦        |
|                                          |
|  Start Time *                            |
|  [Dec 29, 2025] [09:00 AM]              |
|                                          |
|  End Time *                              |
|  [Dec 29, 2025] [10:30 AM]              |
|                                          |
|  Duration: 1h 30m (auto-calculated)      |
|                                          |
|  Location (optional)                     |
|  [Office_____________________]           |
|                                          |
|  Energy Level                            |
|  1 ━━━━●━━━━━━━━━━━━━━━━━━━━━ 10       |
|  Low            Moderate          High   |
|                                          |
|  [Cancel]              [Save Entry]      |
+------------------------------------------+
```

## 3. Category Management Screen

```
+----------------------------------------------------------+
|  Category Management                      [← Back]        |
+----------------------------------------------------------+
|  [+ Create Category]         🔍 Search categories...      |
+----------------------------------------------------------+
|                                                            |
|  Predefined Categories          Custom Categories         |
|  +------------------------+    +------------------------+  |
|  | 🟦 Work                |    | 🟪 Side Projects      |  |
|  |    Budget: 40h/week    |    |    Budget: 10h/week   |  |
|  |    Used: 42h (105%) ⚠️ |    |    Used: 8h (80%)     |  |
|  |    [Edit Budget]       |    |    [Edit] [Delete]    |  |
|  +------------------------+    +------------------------+  |
|                                |                        |  |
|  | 🟩 Personal            |    | 🟨 Learning           |  |
|  |    Budget: 15h/week    |    |    Budget: 5h/week    |  |
|  |    Used: 12h (80%)     |    |    Used: 6h (120%) ⚠️ |  |
|  |    [Edit Budget]       |    |    [Edit] [Delete]    |  |
|  +------------------------+    +------------------------+  |
|                                                            |
|  | 🟧 Health              |                                |
|  |    Budget: 10h/week    |                                |
|  |    Used: 9h (90%)      |                                |
|  |    [Edit Budget]       |                                |
|  +------------------------+                                 |
+----------------------------------------------------------+
```

## 4. Analysis Dashboard

```
+----------------------------------------------------------+
|  Time Analysis                            [← Back]        |
+----------------------------------------------------------+
|  [Daily] [Weekly] [Monthly]       Dec 23-29, 2025 [◀ ▶]  |
+----------------------------------------------------------+
|                                                            |
|  Weekly Patterns                                          |
|  +------------------------------------------------------+ |
|  | 🔁 Recurring Pattern Detected                       | |
|  | Every weekday 9am-10am: Team Stand-up               | |
|  | Consistency: 95% (19/20 days)                       | |
|  +------------------------------------------------------+ |
|                                                            |
|  Time Wasters                                             |
|  +------------------------------------------------------+ |
|  | ⚠️ Social Media - 12h 30m this week                 | |
|  | Impact: High - Replacing productive work time       | |
|  | [Set Limit] [View Details]                          | |
|  +------------------------------------------------------+ |
|  | ⚠️ Unscheduled Meetings - 8h 15m this week          | |
|  | Impact: Medium - Disrupting deep work blocks        | |
|  | [Set Limit] [View Details]                          | |
|  +------------------------------------------------------+ |
|                                                            |
|  Productivity Peaks                                       |
|  +------------------------------------------------------+ |
|  | 📈 Your Peak Performance Times                      | |
|  |                                                      | |
|  | Mon-Fri: 9:00am - 11:30am (87% productive)          | |
|  | Contributing factors:                                | |
|  | • High energy levels                                | |
|  | • Fewer interruptions                               | |
|  | • Deep focus work                                   | |
|  |                                                      | |
|  | 💡 Recommendation: Schedule important tasks here    | |
|  +------------------------------------------------------+ |
|                                                            |
|  Productivity Heatmap                                     |
|  +------------------------------------------------------+ |
|  |     Mon Tue Wed Thu Fri Sat Sun                     | |
|  | 6am [█] [▓] [░] [█] [▓] [░] [░]                    | |
|  | 9am [█] [█] [█] [█] [█] [░] [░]  ■ High            | |
|  | 12p [▓] [▓] [▓] [▓] [▓] [▓] [░]  ▓ Medium          | |
|  | 3pm [▓] [░] [▓] [█] [▓] [▓] [░]  □ Low             | |
|  | 6pm [░] [░] [░] [░] [░] [█] [█]                    | |
|  +------------------------------------------------------+ |
+----------------------------------------------------------+
```

## 5. Goals Screen

```
+----------------------------------------------------------+
|  Time Goals                               [← Back]        |
+----------------------------------------------------------+
|  [+ Create New Goal]                                      |
+----------------------------------------------------------+
|                                                            |
|  Active Goals                                             |
|  +------------------------------------------------------+ |
|  | 🎯 Increase Deep Work Time                          | |
|  | Target: 20 hours/week | Deadline: Jan 31, 2025      | |
|  | Baseline: 12 hours/week                              | |
|  |                                                      | |
|  | Progress: 18h/week                                   | |
|  | ████████████████████░░░░░ 90%                       | |
|  |                                                      | |
|  | ✅ On Track | Next Milestone: 100% (2h away)        | |
|  | [View Details] [Update]                              | |
|  +------------------------------------------------------+ |
|                                                            |
|  +------------------------------------------------------+ |
|  | 🎯 Reduce Social Media Time                         | |
|  | Target: < 5 hours/week | Deadline: Dec 31, 2025     | |
|  | Baseline: 15 hours/week                              | |
|  |                                                      | |
|  | Progress: 8h/week                                    | |
|  | ████████████░░░░░░░░░░░░░ 50%                       | |
|  |                                                      | |
|  | ⚠️ Off Track | Behind by 3 hours                    | |
|  | [View Details] [Update]                              | |
|  +------------------------------------------------------+ |
|                                                            |
|  Milestones Achieved                                      |
|  +------------------------------------------------------+ |
|  | 🏆 25% Milestone - Increase Deep Work (Dec 15)      | |
|  | 🏆 50% Milestone - Increase Deep Work (Dec 22)      | |
|  | 🏆 75% Milestone - Increase Deep Work (Dec 28)      | |
|  +------------------------------------------------------+ |
+----------------------------------------------------------+
```

## 6. Schedule Optimization Screen

```
+----------------------------------------------------------+
|  Schedule Optimization                    [← Back]        |
+----------------------------------------------------------+
|                                                            |
|  Optimization Suggestions                                 |
|  +------------------------------------------------------+ |
|  | 💡 Suggestion #1                                    | |
|  | Move email processing to afternoon                   | |
|  |                                                      | |
|  | Current: Email scattered throughout day (2h 30m)     | |
|  | Proposed: Batch email 2pm-3:30pm, 4:30pm-5pm        | |
|  |                                                      | |
|  | Expected Benefit:                                    | |
|  | • Gain 45min of deep work time                      | |
|  | • Reduce context switching                          | |
|  | • Improve focus quality                             | |
|  |                                                      | |
|  | Confidence: High (85%)                               | |
|  | [Implement] [Dismiss]                                | |
|  +------------------------------------------------------+ |
|                                                            |
|  +------------------------------------------------------+ |
|  | 💡 Suggestion #2                                    | |
|  | Protect morning hours for deep work                  | |
|  |                                                      | |
|  | Create time block: Mon-Fri 9am-11:30am              | |
|  | Protection Level: High (no meetings, no distractions)| |
|  |                                                      | |
|  | Expected Benefit:                                    | |
|  | • Leverage your productivity peak                   | |
|  | • Increase focus work by 12.5h/week                 | |
|  |                                                      | |
|  | Confidence: Very High (92%)                          | |
|  | [Implement] [Dismiss]                                | |
|  +------------------------------------------------------+ |
|                                                            |
|  Active Time Blocks                                       |
|  +------------------------------------------------------+ |
|  | 🔒 Deep Work Block                                  | |
|  | Mon-Fri 9:00am - 11:30am                            | |
|  | Protection: High | Recurrence: Weekly                | |
|  | [Edit] [Remove]                                      | |
|  +------------------------------------------------------+ |
+----------------------------------------------------------+
```

## 7. Comparison View

```
+----------------------------------------------------------+
|  Period Comparison                        [← Back]        |
+----------------------------------------------------------+
|  Compare:                                                  |
|  Period 1: [Dec 16-22] ▼    vs    Period 2: [Dec 23-29] ▼|
+----------------------------------------------------------+
|                                                            |
|  Side-by-Side Comparison                                  |
|  +------------------------+  +-------------------------+  |
|  | Dec 16-22              |  | Dec 23-29               |  |
|  |                        |  |                         |  |
|  | Total: 38h 45m         |  | Total: 42h 15m (+9%) ✅ |  |
|  | Productive: 68%        |  | Productive: 74% (+6%) ✅|  |
|  |                        |  |                         |  |
|  | Top Categories:        |  | Top Categories:         |  |
|  | Work: 24h              |  | Work: 28h (+17%) ✅     |  |
|  | Personal: 10h          |  | Personal: 9h (-10%) ⬇️  |  |
|  | Health: 4h 45m         |  | Health: 5h 15m (+11%) ✅|  |
|  +------------------------+  +-------------------------+  |
|                                                            |
|  Key Changes                                              |
|  +------------------------------------------------------+ |
|  | ✅ Improvements:                                     | |
|  | • 6% increase in productive time                    | |
|  | • Better work-life balance                          | |
|  | • More consistent exercise routine                  | |
|  |                                                      | |
|  | ⚠️ Regressions:                                     | |
|  | • 10% decrease in personal time                     | |
|  | • Increased late-night work sessions                | |
|  +------------------------------------------------------+ |
|                                                            |
|  [Export Comparison Report]                                |
+----------------------------------------------------------+
```

## 8. Mobile View - Quick Entry

```
+------------------------+
| Time Tracker      [⋮]  |
+------------------------+
| [Today] [Week] [Month] |
+------------------------+
|                        |
| Total: 8h 30m          |
| [███████░░░] 71%       |
|                        |
| [+] Quick Log          |
+------------------------+
|                        |
| Timeline               |
| +-----------------+    |
| | 6am             |    |
| | [Sleep-----]    |    |
| | 9am             |    |
| | [Work------]    |    |
| | 12pm            |    |
| | [Lunch]         |    |
| | 2pm             |    |
| | [Work------]    |    |
| | 6pm             |    |
| | [Gym]           |    |
| | 8pm             |    |
| | [Relax----]     |    |
| +-----------------+    |
|                        |
| ⚠️ 2h unlogged         |
| [Fill Gap]             |
+------------------------+
```

## Navigation Flow

1. **Dashboard** (Home) - Overview of today's time tracking
2. **Log Time** - Quick entry form (modal or slide-up)
3. **Timeline** - Detailed view of logged time
4. **Categories** - Manage categories and budgets
5. **Analysis** - Patterns, wasters, productivity insights
6. **Goals** - Time management goals and progress
7. **Optimization** - Suggestions and time blocks
8. **Compare** - Period comparisons
9. **Settings** - Integrations, preferences, alerts

## Responsive Breakpoints

- **Mobile (< 768px)**: Single column, stacked cards, bottom navigation
- **Tablet (768px - 1024px)**: Two columns, sidebar navigation
- **Desktop (> 1024px)**: Full layout, multi-panel views
