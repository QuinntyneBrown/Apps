# Break Management Wireframes

## 1. Break Recommendation Banner

```
+----------------------------------------------------------+
|  FOCUS SESSION TRACKER                    [User] [Settings] |
+----------------------------------------------------------+
|                                                            |
|  +------------------------------------------------------+ |
|  | TIME FOR A BREAK!                                    | |
|  |                                                      | |
|  | You've completed 2 sessions. Take a 5-minute break   | |
|  | to maintain your focus and energy.                   | |
|  |                                                      | |
|  |  [  START BREAK  ]         Skip this break           | |
|  +------------------------------------------------------+ |
|                                                            |
+----------------------------------------------------------+
```

## 2. Break Timer Page - Active State

```
+----------------------------------------------------------+
|  FOCUS SESSION TRACKER                    [User] [Settings] |
+----------------------------------------------------------+
|                                                            |
|                    BREAK IN PROGRESS                       |
|                                                            |
|                    +------------------+                    |
|                   /  ##########       \                    |
|                  | ##            ##    |                   |
|                  |      03:42          |                   |
|                  | ##            ##    |                   |
|                   \  ##########       /                    |
|                    +------------------+                    |
|                         Short Break                        |
|                                                            |
|  +------------------------------------------------------+ |
|  | Suggested Activities                                 | |
|  |                                                      | |
|  | [Walk] [Coffee] [Stretch] [Meditate] [Water] [Snack]| |
|  +------------------------------------------------------+ |
|                                                            |
|            [  EXTEND +5 MIN  ]  [  END BREAK  ]            |
|                                                            |
+----------------------------------------------------------+
```

## 3. Activity Logger Modal

```
+------------------------------------------+
|  Log Break Activity                   [X] |
+------------------------------------------+
|                                          |
|  What did you do during your break?      |
|                                          |
|  [x] Walk      [ ] Coffee  [ ] Stretch   |
|  [ ] Meditate  [ ] Water   [ ] Snack     |
|  [ ] Other                               |
|                                          |
|  Notes (optional):                       |
|  +------------------------------------+  |
|  | Took a walk around the office,   |  |
|  | got some fresh air               |  |
|  +------------------------------------+  |
|                                          |
|  How effective was this break?           |
|                                          |
|         [*] [*] [*] [*] [ ]              |
|            4 out of 5 stars              |
|                                          |
|        [Cancel]    [Log Activity]        |
|                                          |
+------------------------------------------+
```

## 4. Break Extension Modal

```
+------------------------------------------+
|  Extend Break                         [X] |
+------------------------------------------+
|                                          |
|  How much longer do you need?            |
|                                          |
|  Additional Minutes:                     |
|                                          |
|  1 ----●------------------------- 15     |
|           (5 minutes)                    |
|                                          |
|  Reason (optional):                      |
|  +------------------------------------+  |
|  | Still feeling tired, need more   |  |
|  | time to recharge                 |  |
|  +------------------------------------+  |
|                                          |
|  New break duration: 10 minutes          |
|                                          |
|        [Cancel]    [Extend Break]        |
|                                          |
+------------------------------------------+
```

## 5. Skip Break Confirmation Dialog

```
+------------------------------------------+
|  Skip Break?                          [X] |
+------------------------------------------+
|                                          |
|  ⚠️  Warning: You've skipped 3 breaks   |
|     this week. Taking regular breaks     |
|     helps prevent burnout.               |
|                                          |
|  Why are you skipping this break?        |
|  +------------------------------------+  |
|  | In flow state                  [v] |  |
|  +------------------------------------+  |
|                                          |
|  Options:                                |
|  - In flow state, want to continue       |
|  - Not feeling tired                     |
|  - Deadline pressure                     |
|  - Other                                 |
|                                          |
|  Additional notes (optional):            |
|  +------------------------------------+  |
|  |                                    |  |
|  +------------------------------------+  |
|                                          |
|    [Take Break Anyway]  [Skip Break]     |
|                                          |
+------------------------------------------+
```

## 6. Break Completion Summary

```
+----------------------------------------------------------+
|  FOCUS SESSION TRACKER                    [User] [Settings] |
+----------------------------------------------------------+
|                                                            |
|                 BREAK COMPLETE! 🎉                         |
|                                                            |
|         You took a 5-minute break                          |
|                                                            |
|  +------------------------------------------------------+ |
|  | Activities During Break:                             | |
|  |                                                      | |
|  | 🚶 Walk - Took a walk around the office             | |
|  |    Effectiveness: ⭐⭐⭐⭐                            | |
|  |                                                      | |
|  | 💧 Water - Drank a glass of water                   | |
|  |    Effectiveness: ⭐⭐⭐⭐⭐                          | |
|  +------------------------------------------------------+ |
|                                                            |
|            Your break streak: 5 days 🔥                    |
|                                                            |
|              [  READY TO FOCUS  ]                          |
|                                                            |
+----------------------------------------------------------+
```

## 7. Break History Page

```
+----------------------------------------------------------+
|  FOCUS SESSION TRACKER                    [User] [Settings] |
+----------------------------------------------------------+
|  Break History                                             |
+----------------------------------------------------------+
|                                                            |
|  Date Range: [Last 7 Days  v]  Type: [All  v]  [Export]   |
|                                                            |
|  Statistics                                                |
|  +------------------------------------------------------+ |
|  | Total Breaks: 24 | Avg Duration: 5.2 min | Streak: 5d| |
|  | Skip Rate: 8%    | Most Effective: Walk                | |
|  +------------------------------------------------------+ |
|                                                            |
|  +------------------------------------------------------+ |
|  | Dec 28, 2024                                         | |
|  +------------------------------------------------------+ |
|  | 3:30 PM | Short (5 min) | Walk, Coffee | ⭐⭐⭐⭐     | |
|  | 2:00 PM | Long (15 min) | Meditate    | ⭐⭐⭐⭐⭐    | |
|  | 10:00 AM | Short (7 min) | Extended   | ⭐⭐⭐        | |
|  +------------------------------------------------------+ |
|  | Dec 27, 2024                                         | |
|  +------------------------------------------------------+ |
|  | 4:15 PM | Short (5 min) | Walk, Water | ⭐⭐⭐⭐⭐    | |
|  | 2:30 PM | Skipped      | Deadline pressure | --      | |
|  | 11:00 AM | Long (15 min) | Stretch    | ⭐⭐⭐⭐     | |
|  +------------------------------------------------------+ |
|                                                            |
|  [< Prev]  Page 1 of 4  [Next >]                          |
|                                                            |
+----------------------------------------------------------+
```

## 8. Break Settings Page

```
+----------------------------------------------------------+
|  FOCUS SESSION TRACKER                    [User] [Settings] |
+----------------------------------------------------------+
|  Break Settings                                            |
+----------------------------------------------------------+
|                                                            |
|  Break Pattern                                             |
|  +------------------------------------------------------+ |
|  | [x] Pomodoro (5 min after every session,             | |
|  |              15 min after 4 sessions)                | |
|  | [ ] Custom Pattern                                   | |
|  +------------------------------------------------------+ |
|                                                            |
|  Default Break Durations                                   |
|  +------------------------------------------------------+ |
|  | Short Break:  [  5  ] minutes                        | |
|  | Long Break:   [ 15  ] minutes                        | |
|  +------------------------------------------------------+ |
|                                                            |
|  Reminders                                                 |
|  +------------------------------------------------------+ |
|  | [x] Show break recommendations after sessions        | |
|  | [x] Notify when break is almost over (1 min)         | |
|  | [x] Warn about burnout risk (5+ skipped breaks)      | |
|  +------------------------------------------------------+ |
|                                                            |
|  Activity Preferences                                      |
|  +------------------------------------------------------+ |
|  | Favorite Activities (shown first):                   | |
|  | [x] Walk  [x] Coffee  [ ] Meditate                   | |
|  | [x] Water [ ] Snack   [x] Stretch                    | |
|  +------------------------------------------------------+ |
|                                                            |
|              [Cancel]    [Save Settings]                   |
|                                                            |
+----------------------------------------------------------+
```

## 9. Break Timer - Extended State

```
+----------------------------------------------------------+
|  FOCUS SESSION TRACKER                    [User] [Settings] |
+----------------------------------------------------------+
|                                                            |
|               BREAK EXTENDED ⏰                            |
|                                                            |
|                    +------------------+                    |
|                   /  ##########       \                    |
|                  | ##   ORANGE   ##    |  (orange border)  |
|                  |      02:15          |                   |
|                  | ##            ##    |                   |
|                   \  ##########       /                    |
|                    +------------------+                    |
|                   Short Break + 5 min                      |
|                                                            |
|  Extended 1 time (max 2 more extensions)                   |
|                                                            |
|              [  EXTEND +5 MIN  ]  [  END BREAK  ]          |
|                                                            |
+----------------------------------------------------------+
```

## 10. Mobile Break Timer View

```
+------------------------+
| FOCUS TRACKER     [=]  |
+------------------------+
|                        |
|   BREAK IN PROGRESS    |
|                        |
|    +------------+      |
|   /  ########   \      |
|  | ##        ##  |     |
|  |    03:42      |     |
|   \  ########   /      |
|    +------------+      |
|                        |
|     Short Break        |
|                        |
|  Activities:           |
|  [Walk] [Coffee]       |
|  [Water] [Stretch]     |
|                        |
|  [EXTEND]  [END BREAK] |
|                        |
+------------------------+
```

## 11. Break Activity Quick Log (During Break)

```
+----------------------------------------------------------+
|  FOCUS SESSION TRACKER                    [User] [Settings] |
+----------------------------------------------------------+
|                                                            |
|                    Break Time: 03:42                       |
|                                                            |
|  +------------------------------------------------------+ |
|  | Quick Log Activity                                   | |
|  |                                                      | |
|  | [🚶 Walk] [☕ Coffee] [🧘 Meditate]                  | |
|  | [💧 Water] [🍎 Snack] [🤸 Stretch]                  | |
|  |                                                      | |
|  | Recently logged: Walk (⭐⭐⭐⭐)                      | |
|  +------------------------------------------------------+ |
|                                                            |
+----------------------------------------------------------+
```

## 12. Break Effectiveness Insights

```
+----------------------------------------------------------+
|  FOCUS SESSION TRACKER                    [User] [Settings] |
+----------------------------------------------------------+
|  Break Insights                                            |
+----------------------------------------------------------+
|                                                            |
|  Your Most Effective Break Activities                      |
|                                                            |
|  +------------------------------------------------------+ |
|  | 1. 🚶 Walk              Avg: ⭐⭐⭐⭐⭐  (12 times)   | |
|  | 2. 🧘 Meditation        Avg: ⭐⭐⭐⭐⭐  (8 times)    | |
|  | 3. 🤸 Stretch           Avg: ⭐⭐⭐⭐   (15 times)    | |
|  | 4. ☕ Coffee            Avg: ⭐⭐⭐⭐   (20 times)    | |
|  | 5. 💧 Water             Avg: ⭐⭐⭐⭐   (18 times)    | |
|  +------------------------------------------------------+ |
|                                                            |
|  Recommendation:                                           |
|  Try combining Walk + Water for maximum effectiveness      |
|                                                            |
|  Break Timing Insights                                     |
|  +------------------------------------------------------+ |
|  | Best break duration for you: 5-7 minutes             | |
|  | Most productive after: Short breaks                  | |
|  | Warning: 15+ min breaks reduce focus recovery        | |
|  +------------------------------------------------------+ |
|                                                            |
+----------------------------------------------------------+
```
