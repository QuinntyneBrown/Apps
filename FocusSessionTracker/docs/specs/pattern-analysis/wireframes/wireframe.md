# Pattern Analysis Wireframes

## 1. Pattern Insights Dashboard - Main View

```
+------------------------------------------------------------------+
|  FOCUS SESSION TRACKER            [Insights] [History] [Settings] |
+------------------------------------------------------------------+
|                                                                    |
|  Pattern Insights                                [Analyze Now]    |
|                                                                    |
|  +------------------------------+  +---------------------------+  |
|  | OPTIMAL SESSION LENGTH       |  | PEAK FOCUS TIMES          |  |
|  | Confidence: 87%   [view]     |  | Confidence: 82%  [view]   |  |
|  |                              |  |                           |  |
|  |  Recommended: 35 minutes     |  |  Best Times:              |  |
|  |                              |  |  - 9:00 AM - 11:00 AM     |  |
|  |  Based on 105 sessions       |  |  - 2:00 PM - 4:00 PM      |  |
|  |                              |  |                           |  |
|  |  [Apply Recommendation]      |  |  [Schedule Session]       |  |
|  +------------------------------+  +---------------------------+  |
|                                                                    |
|  +------------------------------+  +---------------------------+  |
|  | DISTRACTION PATTERNS         |  | PRODUCTIVITY TRENDS       |  |
|  | 3 patterns detected [view]   |  | Trend: Upward   [view]    |  |
|  |                              |  |                           |  |
|  |  Top Distractors:            |  |  +21% in last 3 weeks     |  |
|  |  1. Email notifications (12) |  |                           |  |
|  |  2. Slack messages (8)       |  |  [################    ]   |  |
|  |  3. Mind wandering (6)       |  |                           |  |
|  |                              |  |  Focus Score: 72 -> 87    |  |
|  |  [View Mitigation Tips]      |  |  [View Details]           |  |
|  +------------------------------+  +---------------------------+  |
|                                                                    |
|  Actionable Insights                                               |
|  +--------------------------------------------------------------+  |
|  | [!] HIGH PRIORITY                                            |  |
|  | Schedule sessions during peak hours                          |  |
|  | You're 23% more productive between 9-11 AM                   |  |
|  | [Schedule Now]  [Dismiss]                                    |  |
|  +--------------------------------------------------------------+  |
|  | [i] MEDIUM PRIORITY                                          |  |
|  | Email notifications disrupt focus                            |  |
|  | 12 distractions from email in the last week                  |  |
|  | [Enable Focus Mode]  [Dismiss]                               |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
|  Last analyzed: 5 minutes ago                    [View History]   |
+------------------------------------------------------------------+
```

## 2. Pattern Insights Dashboard - Insufficient Data State

```
+------------------------------------------------------------------+
|  FOCUS SESSION TRACKER            [Insights] [History] [Settings] |
+------------------------------------------------------------------+
|                                                                    |
|  Pattern Insights                                                  |
|                                                                    |
|                    +---------------------------+                   |
|                   |                             |                  |
|                   |    [Chart with ? Icon]      |                  |
|                   |                             |                  |
|                   +---------------------------+                   |
|                                                                    |
|                   Not Enough Data Yet                              |
|                                                                    |
|         We need at least 20 completed sessions                     |
|         to identify meaningful patterns.                           |
|                                                                    |
|         Progress: [########----------] 8/20                        |
|                                                                    |
|                   [Start a Session]                                |
|                                                                    |
|  +--------------------------------------------------------------+  |
|  | What patterns will we detect?                                |  |
|  |                                                              |  |
|  | - Your optimal session length                                |  |
|  | - Best times of day for deep focus                           |  |
|  | - Common distraction sources                                 |  |
|  | - Productivity trends over time                              |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
+------------------------------------------------------------------+
```

## 3. Optimal Session Length Detail View

```
+------------------------------------------------------------------+
|  FOCUS SESSION TRACKER                         [< Back to Insights] |
+------------------------------------------------------------------+
|                                                                    |
|  Optimal Session Length Analysis                                   |
|  Detected: Dec 28, 2024 | Confidence: 87%                          |
|                                                                    |
|  +--------------------------------------------------------------+  |
|  |                                                              |  |
|  |               RECOMMENDED DURATION                           |  |
|  |                                                              |  |
|  |                      35 minutes                              |  |
|  |                                                              |  |
|  |          Based on your 105 completed sessions                |  |
|  |                                                              |  |
|  |               [Apply as Default Duration]                    |  |
|  |                                                              |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
|  Success Rate by Duration                                          |
|  +--------------------------------------------------------------+  |
|  |                                                              |  |
|  |  15 min  [#########-------]  65% (18 sessions)               |  |
|  |  25 min  [###########-----]  75% (45 sessions)               |  |
|  |  35 min  [################]  92% (38 sessions) <- Best!      |  |
|  |  45 min  [#########-------]  68% (22 sessions)               |  |
|  |  60 min  [######----------]  55% (12 sessions)               |  |
|  |                                                              |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
|  Key Insights                                                      |
|  +--------------------------------------------------------------+  |
|  | - 35-minute sessions have the highest completion rate        |  |
|  | - You tend to abandon sessions longer than 45 minutes        |  |
|  | - Consistency is strong (87% confidence)                     |  |
|  | - This duration aligns with your natural focus rhythm        |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
|  Session Type Breakdown                                            |
|  +---------------------------+  +-------------------------------+  |
|  | Pomodoro (25 min)         |  | Custom (35 min)               |  |
|  | Completion: 75%           |  | Completion: 92%               |  |
|  | Avg Score: 82             |  | Avg Score: 88                 |  |
|  +---------------------------+  +-------------------------------+  |
|                                                                    |
+------------------------------------------------------------------+
```

## 4. Peak Focus Times Detail View

```
+------------------------------------------------------------------+
|  FOCUS SESSION TRACKER                         [< Back to Insights] |
+------------------------------------------------------------------+
|                                                                    |
|  Peak Focus Times Analysis                                         |
|  Detected: Dec 28, 2024 | Confidence: 82%                          |
|                                                                    |
|  Daily Focus Heatmap                                               |
|  +--------------------------------------------------------------+  |
|  |      6  7  8  9  10 11 12 1  2  3  4  5  6  7  8  9  10 11  |  |
|  | Mon  .  .  .  ## ##  #  .  .  ## ## .  .  .  .  .  .  .  . |  |
|  | Tue  .  .  .  ## ##  #  .  .  ## #  .  .  .  .  .  .  .  . |  |
|  | Wed  .  .  .  ## ##  ## .  .  ## ## .  .  .  .  .  .  .  . |  |
|  | Thu  .  .  .  ## ##  #  .  .  ## #  .  .  .  .  .  .  .  . |  |
|  | Fri  .  .  .  ## ##  #  .  .  #  #  .  .  .  .  .  .  .  . |  |
|  |                                                              |  |
|  | Legend: . Low (0-60) # Medium (60-80) ## High (80-100)      |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
|  Your Peak Time Windows                                            |
|  +--------------------------------------------------------------+  |
|  | PRIMARY PEAK                                                 |  |
|  | 9:00 AM - 11:00 AM                                           |  |
|  | Avg Focus Score: 88 | Consistency: 85% | 42 sessions         |  |
|  |                                                              |  |
|  | You're at your sharpest in the morning. Schedule your        |  |
|  | most important work during this window.                      |  |
|  |                                                              |  |
|  | [Schedule Morning Session]                                   |  |
|  +--------------------------------------------------------------+  |
|  +--------------------------------------------------------------+  |
|  | SECONDARY PEAK                                               |  |
|  | 2:00 PM - 4:00 PM                                            |  |
|  | Avg Focus Score: 82 | Consistency: 78% | 35 sessions         |  |
|  |                                                              |  |
|  | Good for focused work after lunch. Consider a short break    |  |
|  | before starting to maximize this window.                     |  |
|  |                                                              |  |
|  | [Schedule Afternoon Session]                                 |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
|  Recommendations                                                   |
|  +--------------------------------------------------------------+  |
|  | - Block calendar for deep work during 9-11 AM               |  |
|  | - Avoid meetings during peak focus times                     |  |
|  | - Schedule low-priority tasks for off-peak hours             |  |
|  | - Take a walk or break between 12-2 PM for afternoon peak    |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
+------------------------------------------------------------------+
```

## 5. Distraction Patterns Detail View

```
+------------------------------------------------------------------+
|  FOCUS SESSION TRACKER                         [< Back to Insights] |
+------------------------------------------------------------------+
|                                                                    |
|  Distraction Patterns Analysis                                     |
|  Last 30 days | 156 distractions analyzed                          |
|                                                                    |
|  Top Distraction Sources                                           |
|  +--------------------------------------------------------------+  |
|  | 1. EMAIL NOTIFICATIONS                          [Resolve]    |  |
|  |    Impact: HIGH | Frequency: 12 times/week                   |  |
|  |                                                              |  |
|  |    Typical timing: 10:30-11:00 AM, 3:00-3:30 PM              |  |
|  |                                                              |  |
|  |    Mitigation Strategy:                                      |  |
|  |    Enable email batching or focus mode during peak work      |  |
|  |    hours. Check email only at designated times.              |  |
|  |                                                              |  |
|  |    [Enable Focus Mode]  [Batch Email]  [Dismiss]             |  |
|  +--------------------------------------------------------------+  |
|  +--------------------------------------------------------------+  |
|  | 2. SLACK MESSAGES                               [Resolve]    |  |
|  |    Impact: MEDIUM | Frequency: 8 times/week                  |  |
|  |                                                              |  |
|  |    Typical timing: Throughout the day                        |  |
|  |                                                              |  |
|  |    Mitigation Strategy:                                      |  |
|  |    Set Slack status to 'Do Not Disturb' during focus         |  |
|  |    sessions. Use scheduled check-ins.                        |  |
|  |                                                              |  |
|  |    [Set DND Status]  [Snooze Notifications]  [Dismiss]       |  |
|  +--------------------------------------------------------------+  |
|  +--------------------------------------------------------------+  |
|  | 3. MIND WANDERING                               [Resolve]    |  |
|  |    Impact: MEDIUM | Frequency: 6 times/week                  |  |
|  |                                                              |  |
|  |    Typical timing: 2:00-2:30 PM (post-lunch dip)             |  |
|  |                                                              |  |
|  |    Mitigation Strategy:                                      |  |
|  |    Consider a short break or meditation before afternoon     |  |
|  |    sessions. Avoid heavy lunches.                            |  |
|  |                                                              |  |
|  |    [Schedule Break]  [View Meditation]  [Dismiss]            |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
|  Distraction Frequency Over Time                                   |
|  +--------------------------------------------------------------+  |
|  |                                                              |  |
|  |  20 |                                                        |  |
|  |     |     *                                                  |  |
|  |  15 |    * *                                                 |  |
|  |     |   *   *      *                                         |  |
|  |  10 |  *     *    * *     *                                  |  |
|  |     | *       *  *   *   * *      *                          |  |
|  |   5 |*         **     * *   *    * *                         |  |
|  |     +----+----+----+----+----+----+----+                     |  |
|  |      W1   W2   W3   W4   W5   W6   W7                        |  |
|  |                                                              |  |
|  |  Trend: Decreasing (good progress!)                          |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
+------------------------------------------------------------------+
```

## 6. Productivity Trends Detail View

```
+------------------------------------------------------------------+
|  FOCUS SESSION TRACKER                         [< Back to Insights] |
+------------------------------------------------------------------+
|                                                                    |
|  Productivity Trends Analysis                                      |
|                                                                    |
|  [Week] [Month] [Quarter]                                          |
|                                                                    |
|  +--------------------------------------------------------------+  |
|  |                                                              |  |
|  |    UPWARD TREND - MODERATE                                   |  |
|  |    +21% improvement over 3 weeks                             |  |
|  |                                                              |  |
|  |    Focus Score: 72 -> 87                                     |  |
|  |                                                              |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
|  Focus Score Trend                                                 |
|  +--------------------------------------------------------------+  |
|  | 100 |                                              *         |  |
|  |     |                                          *             |  |
|  |  90 |                                      *                 |  |
|  |     |                                  *                     |  |
|  |  80 |                              *                         |  |
|  |     |                          *                             |  |
|  |  70 |                      *                                 |  |
|  |     |                  *                                     |  |
|  |  60 |              *                                         |  |
|  |     +----+----+----+----+----+----+----+----+----+           |  |
|  |      W1   W2   W3   W4   W5   W6   W7   W8   W9             |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
|  Contributing Factors                                              |
|  +--------------------------------------------------------------+  |
|  | Positive Influences:                                         |  |
|  | + Consistent morning sessions (87% adherence)                |  |
|  | + Reduced notification distractions (-45%)                   |  |
|  | + Optimal session length adoption (35 min sessions)          |  |
|  | + Better break timing                                        |  |
|  |                                                              |  |
|  | Areas to Maintain:                                           |  |
|  | - Keep 9-11 AM blocked for focus work                        |  |
|  | - Continue using focus mode                                  |  |
|  | - Maintain 35-minute session default                         |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
|  Metrics Comparison                                                |
|  +---------------------------+  +-------------------------------+  |
|  | Focus Score               |  | Completion Rate               |  |
|  | Start: 72                 |  | Start: 68%                    |  |
|  | End: 87                   |  | End: 85%                      |  |
|  | Change: +21%              |  | Change: +25%                  |  |
|  +---------------------------+  +-------------------------------+  |
|  +---------------------------+  +-------------------------------+  |
|  | Sessions Completed        |  | Avg Session Quality           |  |
|  | Start: 12/week            |  | Start: 3.2 stars              |  |
|  | End: 18/week              |  | End: 4.1 stars                |  |
|  | Change: +50%              |  | Change: +28%                  |  |
|  +---------------------------+  +-------------------------------+  |
|                                                                    |
+------------------------------------------------------------------+
```

## 7. Mobile - Pattern Insights Dashboard

```
+---------------------------+
| INSIGHTS             [=]  |
+---------------------------+
|                           |
| Overview                  |
| [Analyze Now]             |
|                           |
| +---------------------+   |
| | Optimal Length      |   |
| | 35 min (87%)        |   |
| | [Apply]             |   |
| +---------------------+   |
|                           |
| +---------------------+   |
| | Peak Times          |   |
| | 9-11 AM (82%)       |   |
| | [Schedule]          |   |
| +---------------------+   |
|                           |
| +---------------------+   |
| | Distractions        |   |
| | 3 patterns          |   |
| | [View]              |   |
| +---------------------+   |
|                           |
| +---------------------+   |
| | Trends              |   |
| | Upward +21%         |   |
| | [Details]           |   |
| +---------------------+   |
|                           |
| Insights (2)              |
| +---------------------+   |
| | [!] Schedule peak   |   |
| | hours               |   |
| | [Action] [Dismiss]  |   |
| +---------------------+   |
|                           |
+---------------------------+
```

## 8. Loading State - Pattern Analysis

```
+------------------------------------------------------------------+
|  FOCUS SESSION TRACKER            [Insights] [History] [Settings] |
+------------------------------------------------------------------+
|                                                                    |
|  Pattern Insights                                                  |
|                                                                    |
|  +--------------------------------------------------------------+  |
|  |                                                              |  |
|  |              Analyzing your focus patterns...                |  |
|  |                                                              |  |
|  |              [==============              ]  65%             |  |
|  |                                                              |  |
|  |              Detecting optimal session length                |  |
|  |                                                              |  |
|  +--------------------------------------------------------------+  |
|                                                                    |
|  +------------------+  +------------------+  +------------------+  |
|  | [Shimmer Effect] |  | [Shimmer Effect] |  | [Shimmer Effect] |  |
|  |                  |  |                  |  |                  |  |
|  | ~~~~~~~~~~~~~~   |  | ~~~~~~~~~~~~~~   |  | ~~~~~~~~~~~~~~   |  |
|  | ~~~~~~~~~~~~~~   |  | ~~~~~~~~~~~~~~   |  | ~~~~~~~~~~~~~~   |  |
|  | ~~~~~~~~~~~~~~   |  | ~~~~~~~~~~~~~~   |  | ~~~~~~~~~~~~~~   |  |
|  |                  |  |                  |  |                  |  |
|  +------------------+  +------------------+  +------------------+  |
|                                                                    |
+------------------------------------------------------------------+
```
