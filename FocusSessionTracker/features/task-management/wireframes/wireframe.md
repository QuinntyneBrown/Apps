# Task Management Wireframes

## 1. Task List Page - Empty State

```
+----------------------------------------------------------------+
|  FOCUS SESSION TRACKER              [Tasks] [Sessions] [User] |
+----------------------------------------------------------------+
|                                                                |
|  Tasks                                    [+ New Task]         |
|  +----------------------------------------------------------+  |
|  |  [All] [Active] [Completed] [Archived]                  |  |
|  +----------------------------------------------------------+  |
|                                                                |
|                                                                |
|                     [Icon: Checklist]                          |
|                                                                |
|                      No tasks yet                              |
|               Create your first task to get started            |
|                                                                |
|                    [+ Create Task]                             |
|                                                                |
|                                                                |
+----------------------------------------------------------------+
```

## 2. Task List Page - With Tasks

```
+----------------------------------------------------------------+
|  FOCUS SESSION TRACKER              [Tasks] [Sessions] [User] |
+----------------------------------------------------------------+
|                                                                |
|  Tasks                                    [+ New Task]         |
|  +----------------------------------------------------------+  |
|  |  [All] [Active] [Completed] [Archived]                  |  |
|  +----------------------------------------------------------+  |
|                                                                |
|  Filters: [Priority v] [Project v] [Search...............] [x]|
|  Sort by: [Priority v]                                         |
|                                                                |
|  +----------------------------------------------------------+  |
|  | ! Complete project documentation              [Edit][...]  |  |
|  |   Status: In Progress | Priority: HIGH                    |  |
|  |   [################-----------] 65%                       |  |
|  |   2 of 3 sessions | 75 min total | Due: Dec 30           |  |
|  |   #documentation #feature                                 |  |
|  +----------------------------------------------------------+  |
|  +----------------------------------------------------------+  |
|  | ! Review pull requests                        [Edit][...]  |  |
|  |   Status: Not Started | Priority: MEDIUM                  |  |
|  |   [-------------------------] 0%                          |  |
|  |   0 of 2 sessions | 0 min total                          |  |
|  |   #code-review #team                                      |  |
|  +----------------------------------------------------------+  |
|  +----------------------------------------------------------+  |
|  | ✓ Fix authentication bug                      [Edit][...]  |  |
|  |   Status: Completed | Priority: HIGH                      |  |
|  |   [#########################] 100%                        |  |
|  |   3 of 3 sessions | 90 min total | Completed: Dec 27     |  |
|  |   #bug #security                                          |  |
|  +----------------------------------------------------------+  |
|                                                                |
|  [< Prev]  Page 1 of 3  [Next >]                              |
|                                                                |
+----------------------------------------------------------------+
```

## 3. Create Task Modal

```
+------------------------------------------+
|  Create New Task                      [X] |
+------------------------------------------+
|                                          |
|  Task Title *                            |
|  +------------------------------------+  |
|  | Complete project documentation     |  |
|  +------------------------------------+  |
|  185/200 characters                      |
|                                          |
|  Description                             |
|  +------------------------------------+  |
|  | Write comprehensive documentation  |  |
|  | for the new task management...     |  |
|  |                                    |  |
|  +------------------------------------+  |
|  0/2000 characters                       |
|                                          |
|  Priority *                              |
|  +------------------------------------+  |
|  | ( ) Low   (x) Medium   ( ) High    |  |
|  +------------------------------------+  |
|                                          |
|  Estimated Sessions                      |
|  +------------------------------------+  |
|  | [3]        sessions                |  |
|  +------------------------------------+  |
|                                          |
|  Due Date                                |
|  +------------------------------------+  |
|  | [Dec 30, 2024]            [Calendar]|  |
|  +------------------------------------+  |
|                                          |
|  Tags                                    |
|  +------------------------------------+  |
|  | [documentation] [feature]    [+Add]|  |
|  +------------------------------------+  |
|                                          |
|  Project                                 |
|  +------------------------------------+  |
|  | Select project...             [v] |  |
|  +------------------------------------+  |
|                                          |
|        [Cancel]    [Create Task]         |
|                                          |
+------------------------------------------+
```

## 4. Task Detail Page

```
+----------------------------------------------------------------+
|  FOCUS SESSION TRACKER              [Tasks] [Sessions] [User] |
+----------------------------------------------------------------+
|  < Back to Tasks                                               |
|                                                                |
|  ! Complete project documentation                  [Edit][Archive]|
|                                                                |
|  +----------------------------------------------------------+  |
|  |  Status: In Progress | Priority: HIGH | Due: Dec 30      |  |
|  |  Created: Dec 25 | Updated: Today at 2:30 PM              |  |
|  +----------------------------------------------------------+  |
|                                                                |
|  Description                                                   |
|  +----------------------------------------------------------+  |
|  |  Write comprehensive documentation for the new task      |  |
|  |  management feature including API docs, user guides,     |  |
|  |  and developer documentation.                            |  |
|  +----------------------------------------------------------+  |
|                                                                |
|  Progress: 65%                                                 |
|  +----------------------------------------------------------+  |
|  |  [################-----------]                           |  |
|  +----------------------------------------------------------+  |
|                                                                |
|  Time Tracking                                                 |
|  +----------------------------------------------------------+  |
|  |  Sessions Completed: 2 of 3 estimated                    |  |
|  |  Total Time Spent: 1h 15m                                |  |
|  |  Average Session: 37 min                                 |  |
|  +----------------------------------------------------------+  |
|                                                                |
|  Session History                                               |
|  +----------------------------------------------------------+  |
|  |  O  Dec 28, 2024 - 2:00 PM         [View Session]        |  |
|  |  |  Duration: 40 min | Progress: +35%                    |  |
|  |  |  "Completed sections 3-5 and started on examples"     |  |
|  |  |                                                        |  |
|  |  O  Dec 27, 2024 - 10:00 AM        [View Session]        |  |
|  |  |  Duration: 35 min | Progress: +30%                    |  |
|  |  |  "Finished introduction and setup sections"           |  |
|  |  |                                                        |  |
|  |  [Current Progress: 65%]                                 |  |
|  +----------------------------------------------------------+  |
|                                                                |
|  Tags: [#documentation] [#feature]                             |
|                                                                |
|                     [Assign to Session]                        |
|                                                                |
+----------------------------------------------------------------+
```

## 5. Assign Task to Session Modal

```
+------------------------------------------+
|  Assign Task to Session               [X] |
+------------------------------------------+
|                                          |
|  Task: Complete project documentation    |
|  Current Progress: 65%                   |
|                                          |
|  +------------------------------------+  |
|  |  You have an active session        |  |
|  |  Started: 5 minutes ago            |  |
|  |  Remaining: 20 minutes             |  |
|  |                                    |  |
|  |  [Assign to Current Session]       |  |
|  +------------------------------------+  |
|                                          |
|  - OR -                                  |
|                                          |
|  +------------------------------------+  |
|  |  [Start New Session with Task]     |  |
|  +------------------------------------+  |
|                                          |
|               [Cancel]                   |
|                                          |
+------------------------------------------+
```

## 6. Update Progress Modal (During Session)

```
+------------------------------------------+
|  Update Task Progress                 [X] |
+------------------------------------------+
|                                          |
|  Task: Complete project documentation    |
|  Session Duration: 40 minutes            |
|                                          |
|  Progress Made                           |
|  +------------------------------------+  |
|  |  [############-----------] 65%     |  |
|  +------------------------------------+  |
|  Drag slider to update                   |
|                                          |
|  Progress Notes                          |
|  +------------------------------------+  |
|  | Completed sections 3-5. Started on |  |
|  | the examples section. Made good    |  |
|  | progress on API documentation.     |  |
|  +------------------------------------+  |
|  0/1000 characters                       |
|                                          |
|  Time Spent: 40 minutes (auto-tracked)   |
|                                          |
|        [Skip]    [Save Progress]         |
|                                          |
+------------------------------------------+
```

## 7. Task Completion Modal

```
+------------------------------------------+
|  Task Completed!                 [Confetti]|
+------------------------------------------+
|                                          |
|           [Icon: Celebration]            |
|                                          |
|    Complete project documentation        |
|                                          |
|  +------------------------------------+  |
|  |  Total Sessions: 3                 |  |
|  |  Total Time: 1h 55m                |  |
|  |  Completed: Dec 28, 2024           |  |
|  +------------------------------------+  |
|                                          |
|  Completion Notes (optional)             |
|  +------------------------------------+  |
|  | All documentation sections are     |  |
|  | complete and reviewed. Ready for   |  |
|  | publication.                       |  |
|  +------------------------------------+  |
|                                          |
|    [View Task]    [Archive Task]         |
|                                          |
+------------------------------------------+
```

## 8. Task Filters Panel (Desktop Sidebar)

```
+----------------------+
|  Filters             |
+----------------------+
|                      |
|  Status              |
|  +----------------+  |
|  | [x] All        |  |
|  | [ ] Not Started|  |
|  | [x] In Progress|  |
|  | [ ] Completed  |  |
|  | [ ] Archived   |  |
|  +----------------+  |
|                      |
|  Priority            |
|  +----------------+  |
|  | [x] High       |  |
|  | [x] Medium     |  |
|  | [ ] Low        |  |
|  +----------------+  |
|                      |
|  Project             |
|  +----------------+  |
|  | [All Projects v]| |
|  +----------------+  |
|                      |
|  Tags                |
|  +----------------+  |
|  | Select tags... |  |
|  | [documentation]|  |
|  | [feature]      |  |
|  +----------------+  |
|                      |
|  Due Date            |
|  +----------------+  |
|  | [ ] Overdue    |  |
|  | [ ] This Week  |  |
|  | [ ] This Month |  |
|  | [ ] Custom     |  |
|  +----------------+  |
|                      |
|  [Clear All]         |
|                      |
+----------------------+
```

## 9. Mobile Task List View

```
+------------------------+
| FOCUS TRACKER     [=]  |
+------------------------+
| Tasks          [+ New] |
+------------------------+
|                        |
| [All] [Active] [Done]  |
|                        |
| [Search tasks...]      |
|                        |
| +--------------------+ |
| |!  Write docs       | |
| |   In Progress      | |
| |   [#######---] 65% | |
| |   2/3 | 75m | 12/30| |
| +--------------------+ |
| +--------------------+ |
| |!  Review PRs       | |
| |   Not Started      | |
| |   [----------]  0% | |
| |   0/2 | 0m         | |
| +--------------------+ |
| +--------------------+ |
| |✓  Fix auth bug     | |
| |   Completed        | |
| |   [##########] 100%| |
| |   3/3 | 90m        | |
| +--------------------+ |
|                        |
|        [Load More]     |
|                        |
+------------------------+
| [Home][Tasks][Stats]   |
+------------------------+
```

## 10. Mobile Task Detail View

```
+------------------------+
| < Tasks                |
+------------------------+
|! Complete docs    [...]|
+------------------------+
|                        |
| Status: In Progress    |
| Priority: HIGH         |
| Due: Dec 30, 2024      |
|                        |
| [#########---] 65%     |
|                        |
| 2 of 3 sessions        |
| 1h 15m total time      |
|                        |
| Description            |
| +--------------------+ |
| | Write comprehensive| |
| | documentation for  | |
| | the new feature... | |
| +--------------------+ |
|                        |
| Session History        |
| +--------------------+ |
| | O Dec 28, 2:00 PM  | |
| |   40m | +35%       | |
| | O Dec 27, 10:00 AM | |
| |   35m | +30%       | |
| +--------------------+ |
|                        |
| Tags                   |
| [#docs] [#feature]     |
|                        |
|   [Assign to Session]  |
|                        |
+------------------------+
```

## 11. Quick Add Task (Floating Action Button)

```
+------------------------+
|                        |
|                        |
|  [Task List Content]   |
|                        |
|                        |
|                        |
|                        |
|                   [+]  |  <- Floating Action Button
|                        |
+------------------------+
                |
                v
+------------------------+
| Quick Add Task    [X]  |
+------------------------+
|                        |
| Title                  |
| [..................]   |
|                        |
| Priority               |
| [Low][Med][High]       |
|                        |
| [Cancel] [Create]      |
|                        |
+------------------------+
```

## 12. Task Context Menu (Three Dots)

```
+--------------------+
| Edit Task          |
|--------------------|
| Assign to Session  |
|--------------------|
| Mark as Complete   |
|--------------------|
| Duplicate Task     |
|--------------------|
| Archive Task       |
|--------------------|
| Delete Task        |
+--------------------+
```

## 13. Task Progress Chart (Analytics View)

```
+----------------------------------------------------------+
|  Task Analytics                                          |
+----------------------------------------------------------+
|                                                          |
|  Completion Rate                                         |
|  +----------------------------------------------------+  |
|  |  Completed: 15 tasks  [###############] 75%        |  |
|  |  In Progress: 3 tasks [###] 15%                    |  |
|  |  Not Started: 2 tasks [##] 10%                     |  |
|  +----------------------------------------------------+  |
|                                                          |
|  Average Time per Task                                   |
|  +----------------------------------------------------+  |
|  |  [===========================]  2h 15m              |  |
|  +----------------------------------------------------+  |
|                                                          |
|  Tasks by Priority                                       |
|  +----------------------------------------------------+  |
|  |     High    |    Medium    |      Low               |  |
|  |  [######]   |  [########]  |   [####]               |  |
|  |     6       |      8       |     4                  |  |
|  +----------------------------------------------------+  |
|                                                          |
+----------------------------------------------------------+
```
