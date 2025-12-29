# Wireframes - Sleep Quality Tracker

## Dashboard Page

```
+----------------------------------------------------------+
|  Sleep Quality Tracker          [User Menu] [Logout]    |
+----------------------------------------------------------+
| [Dashboard] [Sleep Log] [Goals] [Habits] [Reports]      |
+----------------------------------------------------------+
|                                                          |
| +----------------------+  +----------------------------+ |
| | Last Night's Sleep   |  | Sleep Quality Trend       | |
| |                      |  |                            | |
| | 7h 32m              |  |   [Line Chart]             | |
| | Quality: 85         |  |   Shows 30-day trend       | |
| | [View Details]      |  |   with quality scores      | |
| +----------------------+  +----------------------------+ |
|                                                          |
| +----------------------+  +----------------------------+ |
| | Current Streak       |  | Sleep Debt                 | |
| |                      |  |                            | |
| | 🔥 7 days           |  | Current: 2h 15m            | |
| | Goal: 8h/night      |  | [Debt Trend Chart]         | |
| | [View Goals]        |  | Severity: Mild             | |
| +----------------------+  +----------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Recovery Score                                       | |
| |                                                      | |
| | [Gauge Chart: 78/100]                               | |
| | Status: Ready for moderate training                 | |
| +------------------------------------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Recent Sleep Sessions                                | |
| | +--------------------------------------------------+ | |
| | | Date       | Duration | Quality | Sleep/Wake     | | |
| | | 2025-12-28 | 7h 32m   | 85      | 11:00P - 6:32A | | |
| | | 2025-12-27 | 8h 15m   | 92      | 10:30P - 6:45A | | |
| | | 2025-12-26 | 6h 45m   | 68      | 11:45P - 6:30A | | |
| | +--------------------------------------------------+ | |
| | [View All Sessions]                                  | |
| +------------------------------------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Quick Actions                                        | |
| | [Log Sleep] [Log Habit] [Log Nap] [Log Dream]       | |
| +------------------------------------------------------+ |
+----------------------------------------------------------+
```

## Sleep Session Entry Form

```
+----------------------------------------------------------+
|  Log Sleep Session                            [X Close]  |
+----------------------------------------------------------+
|                                                          |
| Sleep Date:                                              |
| [Date Picker: 12/28/2025]                               |
|                                                          |
| Bedtime:                                                 |
| [Time Picker: 11:00 PM]                                 |
|                                                          |
| Wake Time:                                               |
| [Time Picker: 6:30 AM]                                  |
|                                                          |
| Total Duration: 7h 30m (calculated)                      |
|                                                          |
| Sleep Quality Rating:                                    |
| [1]--[2]--[3]--[4]--[5]--[6]--[7]--[8]--[9]--[10]      |
|  Poor                    Average                Excellent|
|                          Selected: 8                     |
|                                                          |
| Notes (optional):                                        |
| [Text Area]                                             |
|                                                          |
| [Add Sleep Stages] [Add Interruptions]                  |
|                                                          |
|                         [Cancel]  [Save Session]         |
+----------------------------------------------------------+
```

## Sleep Quality Detail View

```
+----------------------------------------------------------+
|  Sleep Session - December 28, 2025            [< Back]   |
+----------------------------------------------------------+
|                                                          |
| +------------------------------------------------------+ |
| | Overall Quality Score                                | |
| |                                                      | |
| |          [Circular Gauge: 85/100]                   | |
| |                   Excellent                          | |
| +------------------------------------------------------+ |
|                                                          |
| Sleep Duration: 7h 32m | Bedtime: 11:00 PM              |
| Wake Time: 6:32 AM     | Efficiency: 94%                |
|                                                          |
| +------------------------------------------------------+ |
| | Quality Score Breakdown                              | |
| |                                                      | |
| | Duration Score:    90  [████████████████░░░░]  30%  | |
| | Efficiency Score:  94  [██████████████████░░]  25%  | |
| | Stage Score:       82  [████████████████░░░░]  25%  | |
| | Consistency:       75  [███████████████░░░░░]  10%  | |
| | User Rating:       80  [████████████████░░░░]  10%  | |
| +------------------------------------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Sleep Stages Timeline                                | |
| |                                                      | |
| | [Stacked Area Chart]                                | |
| | 11PM   12AM   1AM    2AM    3AM    4AM    5AM   6AM | |
| | [Awake] [Light Sleep] [Deep Sleep] [REM]           | |
| |                                                      | |
| | Light Sleep: 3h 45m (50%)                           | |
| | Deep Sleep:  1h 52m (25%)                           | |
| | REM Sleep:   1h 35m (21%)                           | |
| | Awake:       20m (4%)                               | |
| +------------------------------------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Sleep Interruptions                                  | |
| | - 2:15 AM - 5 minutes - Bathroom                    | |
| | - 4:30 AM - 15 minutes - Unknown                    | |
| +------------------------------------------------------+ |
|                                                          |
| [Edit Session] [Delete Session] [Add Dream Entry]       |
+----------------------------------------------------------+
```

## Sleep Goals Page

```
+----------------------------------------------------------+
|  Sleep Goals                                             |
+----------------------------------------------------------+
| [Dashboard] [Sleep Log] [Goals] [Habits] [Reports]      |
+----------------------------------------------------------+
|                                                          |
| +------------------------------------------------------+ |
| | Your Sleep Goal                                      | |
| |                                                      | |
| | Target Duration: 8 hours per night                   | |
| | Target Bedtime:  10:30 PM                           | |
| | Target Wake:     6:30 AM                            | |
| |                                                      | |
| |                                         [Edit Goal]  | |
| +------------------------------------------------------+ |
|                                                          |
| +----------------------+  +----------------------------+ |
| | Current Streak       |  | This Week's Progress       | |
| |                      |  |                            | |
| |      🔥 7 days      |  | [5/7 days met goal]        | |
| |                      |  |                            | |
| | Best Streak: 21 days |  | M  T  W  T  F  S  S       | |
| |                      |  | ✓  ✓  ✗  ✓  ✓  ✓  ✓      | |
| +----------------------+  +----------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Goal Achievement Calendar                            | |
| |                                                      | |
| |              December 2025                           | |
| | S   M   T   W   T   F   S                           | |
| | 1   2   3   4   5   6   7                           | |
| | ✓   ✓   ✗   ✓   ✓   ✓   ✗                          | |
| | 8   9  10  11  12  13  14                           | |
| | ✓   ✓   ✓   ✓   ✓   ✓   ✓                          | |
| | ...                                                  | |
| |                                                      | |
| | ✓ Met Goal  ✗ Missed Goal  ○ No Data               | |
| +------------------------------------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Goal Statistics                                      | |
| | - Total Days Tracked: 28                            | |
| | - Goals Met: 23 (82%)                               | |
| | - Goals Missed: 5 (18%)                             | |
| | - Average Sleep Duration: 7h 45m                    | |
| | - Consistency Score: 78/100                         | |
| +------------------------------------------------------+ |
+----------------------------------------------------------+
```

## Habit Correlation Page

```
+----------------------------------------------------------+
|  Habit Correlation                                       |
+----------------------------------------------------------+
| [Dashboard] [Sleep Log] [Goals] [Habits] [Reports]      |
+----------------------------------------------------------+
|                                                          |
| +------------------------------------------------------+ |
| | Quick Log Habit                                      | |
| |                                                      | |
| | Habit: [Caffeine ▼] Time: [2:30 PM] Amount: [2 cups]| |
| |                                          [Log Habit] | |
| +------------------------------------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Identified Correlations                              | |
| |                                                      | |
| | ⚠️ Strong Negative Correlation                       | |
| | Caffeine after 2:00 PM reduces sleep quality by 15% | |
| | Confidence: 87%                                      | |
| | Recommendation: Avoid caffeine after 1:00 PM         | |
| |                                                      | |
| | ✅ Strong Positive Correlation                       | |
| | Morning exercise (6-8 AM) improves quality by 12%   | |
| | Confidence: 82%                                      | |
| | Recommendation: Schedule workouts before 8 AM        | |
| |                                                      | |
| | ⚠️ Moderate Negative Correlation                     | |
| | Screen time after 9 PM reduces quality by 8%        | |
| | Confidence: 74%                                      | |
| | Recommendation: Stop screens 1 hour before bedtime  | |
| +------------------------------------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Caffeine Impact Analysis                             | |
| |                                                      | |
| | [Scatter Plot: Caffeine Time vs Sleep Quality]      | |
| |                                                      | |
| | Optimal Cutoff Time: 1:00 PM                        | |
| | Your Average Cutoff: 2:30 PM                        | |
| | Potential Quality Improvement: +12 points           | |
| +------------------------------------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Recent Habits                                        | |
| | Date       | Habit      | Time   | Impact            | |
| | 12/28/2025 | Caffeine   | 2:30PM | -10 quality pts  | |
| | 12/28/2025 | Exercise   | 7:00AM | +15 quality pts  | |
| | 12/28/2025 | Alcohol    | 8:00PM | -5 quality pts   | |
| +------------------------------------------------------+ |
+----------------------------------------------------------+
```

## Weekly Report Page

```
+----------------------------------------------------------+
|  Weekly Sleep Report                                     |
+----------------------------------------------------------+
| [Dashboard] [Sleep Log] [Goals] [Habits] [Reports]      |
+----------------------------------------------------------+
|                                                          |
| Week: December 22-28, 2025        [< Previous] [Next >] |
|                                                          |
| +------------------------------------------------------+ |
| | Sleep Summary                                        | |
| |                                                      | |
| | Average Duration:    7h 42m  (Goal: 8h)             | |
| | Average Quality:     82/100  (↑ 5 pts from last wk) | |
| | Total Sleep Time:    53h 54m                        | |
| | Sleep Efficiency:    91%                            | |
| | Goals Met:          5/7 days (71%)                  | |
| +------------------------------------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Daily Breakdown                                      | |
| |                                                      | |
| | [Bar Chart: Sleep Duration by Day]                  | |
| | M    T    W    T    F    S    S                     | |
| | 7.5h 8.2h 6.8h 7.9h 8.1h 7.8h 7.6h                  | |
| +------------------------------------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Achievements This Week                               | |
| | ✓ Maintained 7-day sleep goal streak                | |
| | ✓ Achieved consistent bedtime (variance < 30 min)   | |
| | ✓ Average quality above 80                          | |
| +------------------------------------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Patterns Identified                                  | |
| | - Friday night sleep quality 18% lower than average | |
| | - Weekend sleep duration 45 min longer than weekday | |
| | - Best sleep quality on Tuesday (92/100)            | |
| +------------------------------------------------------+ |
|                                                          |
| +------------------------------------------------------+ |
| | Top Recommendations                                  | |
| |                                                      | |
| | 1. HIGH PRIORITY                                    | |
| |    Avoid caffeine after 1 PM on weekdays            | |
| |    Expected benefit: +10 quality points             | |
| |                                                      | |
| | 2. MEDIUM PRIORITY                                  | |
| |    Maintain Friday bedtime consistency              | |
| |    Expected benefit: +8 quality points              | |
| |                                                      | |
| | 3. LOW PRIORITY                                     | |
| |    Increase deep sleep percentage with exercise     | |
| |    Expected benefit: Better recovery                | |
| +------------------------------------------------------+ |
|                                                          |
| [Export as PDF] [Export as CSV] [Email Report]          |
+----------------------------------------------------------+
```

## Mobile Responsive Views

### Mobile Dashboard (Portrait)

```
+-------------------------+
| Sleep Quality Tracker   |
| [☰ Menu]    [User Icon] |
+-------------------------+
|                         |
| Last Night's Sleep      |
| +---------------------+ |
| |      7h 32m        | |
| |   Quality: 85      | |
| |   [View Details]   | |
| +---------------------+ |
|                         |
| Current Streak          |
| +---------------------+ |
| |     🔥 7 days      | |
| |   Goal: 8h/night   | |
| +---------------------+ |
|                         |
| Quality Trend (30d)     |
| +---------------------+ |
| | [Mini Line Chart]  | |
| +---------------------+ |
|                         |
| Sleep Debt              |
| +---------------------+ |
| | Current: 2h 15m    | |
| | Severity: Mild     | |
| +---------------------+ |
|                         |
| Quick Actions           |
| [Log Sleep]             |
| [Log Habit]             |
| [Log Nap]               |
|                         |
+-------------------------+
| [🏠] [📊] [🎯] [⚙️]    |
+-------------------------+
```

## Design Notes

### Color Scheme
- Primary: #3F51B5 (Indigo)
- Secondary: #00BCD4 (Cyan)
- Success: #4CAF50 (Green)
- Warning: #FF9800 (Orange)
- Error: #F44336 (Red)
- Background: #FAFAFA (Light Gray)
- Text: #212121 (Dark Gray)

### Typography
- Headings: Roboto, Bold
- Body: Roboto, Regular
- Data/Numbers: Roboto Mono

### Quality Score Colors
- Poor (0-40): #F44336 (Red)
- Fair (41-60): #FF9800 (Orange)
- Good (61-85): #4CAF50 (Green)
- Excellent (86-100): #2196F3 (Blue)

### Interactive Elements
- All buttons have hover states
- Form inputs show focus indicators
- Cards have subtle shadows
- Interactive charts with tooltips
- Smooth transitions and animations

### Accessibility
- Minimum contrast ratio 4.5:1
- All icons have text labels
- Keyboard navigation support
- Screen reader compatible
- Touch targets minimum 44x44px
