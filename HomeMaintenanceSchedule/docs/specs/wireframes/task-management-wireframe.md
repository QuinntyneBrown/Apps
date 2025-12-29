# Task Management - Wireframes

## 1. Task Dashboard (Main View)

### Desktop Layout (1200px+)

```
+------------------------------------------------------------------+
|  [Logo] HomeMaintenanceSchedule          [Search] [User Menu ▼] |
+------------------------------------------------------------------+
| Dashboard | Tasks | Providers | Seasonal | Appliances | Reports |
+------------------------------------------------------------------+
|                                                                  |
|  +-------------+  +-------------+  +-------------+  +-----------+|
|  | Active Tasks|  | Overdue     |  | This Week   |  | Completed ||
|  |             |  |             |  |             |  | This Month||
|  |     12      |  |      3      |  |      5      |  |     18    ||
|  |             |  | [!] Warning |  |             |  |           ||
|  +-------------+  +-------------+  +-------------+  +-----------+|
|                                                                  |
|  Filter by Status: [All ▼]  Priority: [All ▼]  Category: [All ▼]|
|                                                                  |
|  [Search tasks...]                         [+ New Task] [Calendar]|
|                                                                  |
|  +------------------------------------------------------------+ |
|  | Task Title          | Due Date  | Priority | Status | Cat | |
|  +------------------------------------------------------------+ |
|  | [✓] Replace HVAC... | Jan 15    | Medium   | [•••]  | HVAC| |
|  | [!] Clean Gutters   | Jan 12    | High     | Overdue| Land| |
|  | [ ] Check Smoke...  | Jan 20    | High     | Sched. | Safe| |
|  | [ ] Service Water..  | Jan 18    | Medium   | Sched. | Plum| |
|  | [ ] Inspect Roof    | Jan 25    | Low      | Sched. | Stru| |
|  +------------------------------------------------------------+ |
|                                                                  |
|  Showing 5 of 12 tasks                    < 1 2 3 >             |
|                                                                  |
+------------------------------------------------------------------+
```

### Status Indicators
- `[✓]` - Completed (Green)
- `[!]` - Overdue (Red)
- `[ ]` - Scheduled (Blue)
- `[⊙]` - In Progress (Orange)
- `[✗]` - Cancelled (Gray)

### Priority Indicators
- Critical: ⚠️ Red
- High: ⬆️ Orange
- Medium: ➡️ Yellow
- Low: ⬇️ Green

## 2. Task Creation Modal

```
+------------------------------------------------------------+
|  Create New Task                                        [X]|
+------------------------------------------------------------+
|                                                            |
|  Basic Information                                         |
|  ┌──────────────────────────────────────────────────────┐ |
|  │ Title *                                              │ |
|  │ [Replace HVAC Filter________________]                │ |
|  └──────────────────────────────────────────────────────┘ |
|                                                            |
|  ┌──────────────────────────────────────────────────────┐ |
|  │ Description                                          │ |
|  │ [Change air filter in main HVAC unit                │ |
|  │  Located in basement...____________]                 │ |
|  │                                                      │ |
|  └──────────────────────────────────────────────────────┘ |
|                                                            |
|  Category *        Priority *                              |
|  [HVAC        ▼]   ○ Low  ● Medium  ○ High  ○ Critical     |
|                                                            |
|  Scheduling                                                |
|  Scheduled Date *      Due Date *                          |
|  [📅 01/15/2025]      [📅 01/15/2025 6:00 PM]              |
|                                                            |
|  ☐ Recurring Task                                          |
|     Repeat every [3  ] [Months ▼]                          |
|                                                            |
|  Assignment                                                |
|  Assign to Provider (optional)                             |
|  [Select provider...                              ▼]       |
|                                                            |
|  Cost                                                      |
|  Estimated Cost (optional)                                 |
|  [$] [25.00___]                                            |
|                                                            |
|  Attachments                                               |
|  ┌──────────────────────────────────────────────────────┐ |
|  │                                                      │ |
|  │    Drag & drop photos here or click to upload       │ |
|  │                     [📁]                             │ |
|  │                                                      │ |
|  └──────────────────────────────────────────────────────┘ |
|                                                            |
|  [Cancel]                      [Save as Template] [Create]|
+------------------------------------------------------------+
```

## 3. Task Detail View

```
+------------------------------------------------------------------+
|  ← Back to Tasks                                    [Edit] [•••] |
+------------------------------------------------------------------+
|                                                                  |
|  Replace HVAC Filter                          [✓ Complete Task]  |
|  Status: [Scheduled]  Priority: [Medium ➡️]  Category: [HVAC]    |
|                                                                  |
|  +------------------------+  +-------------------------------+   |
|  | Details  | Costs       |  | Quick Actions                 |   |
|  | Photos   | Notes       |  | [✓] Complete                  |   |
|  | History  |             |  | [⏸] Postpone                 |   |
|  +------------------------+  | [✗] Cancel                    |   |
|                              +-------------------------------+   |
|  Details Tab                                                     |
|  ─────────────────────────────────────────────────────────────  |
|                                                                  |
|  Description                                                     |
|  Change air filter in main HVAC unit located in basement.        |
|  Current filter is 3 months old and needs replacement.           |
|                                                                  |
|  📅 Scheduled: January 15, 2025 at 10:00 AM                      |
|  ⏰ Due: January 15, 2025 at 6:00 PM                             |
|  📍 Property: Main Home (123 Main St)                            |
|  👷 Assigned Provider: None                                      |
|  🔄 Recurrence: Every 3 months                                   |
|  📅 Created: January 1, 2025                                     |
|                                                                  |
|  Estimated Cost: $25.00                                          |
|                                                                  |
+------------------------------------------------------------------+
```

## 4. Task Completion Modal

```
+------------------------------------------------------------+
|  Complete Task                                          [X]|
+------------------------------------------------------------+
|                                                            |
|  Task: Replace HVAC Filter                                 |
|  Originally Scheduled: January 15, 2025                    |
|                                                            |
|  Completion Date *                                         |
|  [📅 01/15/2025]  [🕐 3:30 PM]                             |
|                                                            |
|  Actual Cost                                               |
|  [$] [22.50___]                                            |
|  Estimated: $25.00  Savings: $2.50 ✓                       |
|                                                            |
|  Duration                                                  |
|  [0] hours [15] minutes                                    |
|                                                            |
|  Completion Notes                                          |
|  ┌──────────────────────────────────────────────────────┐ |
|  │ Filter replaced successfully. Old filter was very    │ |
|  │ dirty, indicating good timing for replacement.       │ |
|  │ No issues encountered.                               │ |
|  └──────────────────────────────────────────────────────┘ |
|                                                            |
|  Before/After Photos                                       |
|  ┌──────────────────────────────────────────────────────┐ |
|  │  [📷 Old Filter]  [📷 New Filter]  [+ Add Photo]     │ |
|  └──────────────────────────────────────────────────────┘ |
|                                                            |
|  ☐ Task took longer than expected                          |
|  ☐ Additional work required                                |
|                                                            |
|  [Cancel]                         [✓ Complete Task]        |
+------------------------------------------------------------+
```

## 5. Task Postpone Modal

```
+------------------------------------------------------------+
|  Postpone Task                                          [X]|
+------------------------------------------------------------+
|                                                            |
|  Task: Replace HVAC Filter                                 |
|                                                            |
|  Current Due Date                                          |
|  📅 January 15, 2025 at 6:00 PM                            |
|                                                            |
|  New Due Date *                                            |
|  [📅 01/20/2025]  [🕐 6:00 PM]                             |
|                                                            |
|  Reason for Postponement *                                 |
|  ┌──────────────────────────────────────────────────────┐ |
|  │ Waiting for filter delivery from supplier.           │ |
|  │ Expected to arrive January 19.                       │ |
|  └──────────────────────────────────────────────────────┘ |
|                                                            |
|  ⚠️ This task has been postponed 1 time(s)                 |
|                                                            |
|  ☑ Notify assigned service provider                        |
|                                                            |
|  [Cancel]                              [Postpone Task]     |
+------------------------------------------------------------+
```

## 6. Calendar View

```
+------------------------------------------------------------------+
|  ← Back to List                                    [Month ▼] 2025|
+------------------------------------------------------------------+
|  Filter: [All Categories ▼]  Priority: [All ▼]                  |
+------------------------------------------------------------------+
|                                                                  |
|                        January 2025                              |
|                                                                  |
|  Sun    Mon    Tue    Wed    Thu    Fri    Sat                  |
|  ────────────────────────────────────────────────────────────  |
|         1      2      3      4      5      6                     |
|         •                                  •                     |
|                                                                  |
|   7     8      9     10     11     12     13                     |
|   •                         ••     ⚠️                            |
|                                   Gutters                        |
|                                                                  |
|  14    15     16     17     18     19     20                     |
|        ••                   •            •                       |
|       HVAC                Water          Smoke                   |
|                           Heater         Detectors               |
|                                                                  |
|  21    22     23     24     25     26     27                     |
|                                  •                               |
|                                 Roof                             |
|                                                                  |
|  28    29     30     31                                          |
|                                                                  |
+------------------------------------------------------------------+
|  Legend: • Scheduled  ⚠️ Overdue  ✓ Completed  ⊙ In Progress    |
+------------------------------------------------------------------+
```

## 7. Overdue Tasks Alert Banner

```
+------------------------------------------------------------------+
|  ⚠️ You have 3 overdue tasks requiring attention [View Tasks] [X]|
+------------------------------------------------------------------+
```

## 8. Task List Item (Mobile)

```
+----------------------------------+
| ┌──────────────────────────────┐|
| │ [!] Clean Gutters            ││
| │ Due: Jan 12 (3 days overdue) ││
| │ Priority: High  Category: Land│|
| │ [Complete] [Postpone] [View] ││
| └──────────────────────────────┘|
+----------------------------------+
```

## 9. Empty State

```
+------------------------------------------------------------------+
|                                                                  |
|                            📋                                    |
|                                                                  |
|                    No tasks scheduled yet                        |
|                                                                  |
|              Get started by creating your first                  |
|               home maintenance task below.                       |
|                                                                  |
|                       [+ Create Task]                            |
|                                                                  |
+------------------------------------------------------------------+
```

## 10. Task Filters Sidebar (Desktop)

```
┌─────────────────────┐
│ FILTERS             │
├─────────────────────┤
│ Status              │
│ ☑ Scheduled         │
│ ☐ In Progress       │
│ ☑ Overdue           │
│ ☐ Completed         │
│ ☐ Cancelled         │
├─────────────────────┤
│ Priority            │
│ ☐ Critical          │
│ ☑ High              │
│ ☑ Medium            │
│ ☐ Low               │
├─────────────────────┤
│ Category            │
│ ☑ HVAC              │
│ ☑ Plumbing          │
│ ☐ Electrical        │
│ ☑ Landscaping       │
│ ☐ Appliances        │
│ ☐ Structural        │
│ ☐ Cleaning          │
│ ☑ Safety            │
├─────────────────────┤
│ Date Range          │
│ From: [📅 01/01/25] │
│ To:   [📅 01/31/25] │
├─────────────────────┤
│ [Clear] [Apply]     │
└─────────────────────┘
```

## Interaction Patterns

### Swipe Actions (Mobile)
- Swipe Right: Complete Task ✓
- Swipe Left: Options Menu (Edit, Postpone, Cancel)

### Drag & Drop
- Calendar: Drag task to reschedule
- Task List: Drag to reorder (manual sorting)

### Context Menu (Right-Click)
- Edit Task
- Complete Task
- Postpone Task
- Cancel Task
- Duplicate Task
- View History

### Keyboard Shortcuts
- `N` - New Task
- `C` - Complete selected task
- `E` - Edit selected task
- `P` - Postpone selected task
- `/` - Focus search
- `Esc` - Close modal

## Responsive Breakpoints

- **Mobile**: < 768px (Single column, bottom nav)
- **Tablet**: 768px - 1023px (Two columns, collapsible sidebar)
- **Desktop**: 1024px+ (Multi-column, full sidebar)

## Accessibility Notes

- All interactive elements have keyboard focus indicators
- ARIA labels for screen readers
- Color-blind friendly status indicators (icons + colors)
- Minimum touch target size: 44x44px
- Proper heading hierarchy (h1, h2, h3)
- Form labels associated with inputs
- Error messages announced to screen readers
