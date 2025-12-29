# Screening Management Wireframes

## Screening Dashboard

```
+----------------------------------------------------------+
|  ANNUAL HEALTH SCREENING REMINDER                    [?]  |
+----------------------------------------------------------+
|  [Dashboard] [Screenings] [Appointments] [Profile]        |
+----------------------------------------------------------+
|                                                           |
|  +-- Overdue Screenings (2) ---------------------------+ |
|  | ! Colonoscopy         Due: 3 months ago  [Schedule] | |
|  | ! Mammogram           Due: 1 month ago   [Schedule] | |
|  +-----------------------------------------------------+ |
|                                                           |
|  +-- Upcoming Screenings -------------------------------+ |
|  | o Blood Pressure      Scheduled: Jan 15   [View]    | |
|  | o Annual Physical     Scheduled: Feb 1    [View]    | |
|  +-----------------------------------------------------+ |
|                                                           |
|  +-- Recent Completions --------------------------------+ |
|  | v Cholesterol Panel   Completed: Dec 10   [Results] | |
|  | v Eye Exam            Completed: Nov 5    [Results] | |
|  +-----------------------------------------------------+ |
|                                                           |
|                              [+ Schedule New Screening]   |
+----------------------------------------------------------+
```

## Schedule Screening Form

```
+----------------------------------------------------------+
|  Schedule Health Screening                          [X]   |
+----------------------------------------------------------+
|                                                           |
|  Screening Type *                                         |
|  +----------------------------------------------------+  |
|  | Select screening type...                        v  |  |
|  +----------------------------------------------------+  |
|                                                           |
|  Healthcare Provider                                      |
|  +----------------------------------------------------+  |
|  | Search providers...                                |  |
|  +----------------------------------------------------+  |
|                                                           |
|  Date *                    Time                           |
|  +---------------------+   +-----------------------+      |
|  | MM/DD/YYYY      [#] |   | Select time...    v  |      |
|  +---------------------+   +-----------------------+      |
|                                                           |
|  Location                                                 |
|  +----------------------------------------------------+  |
|  | Enter address or facility name                     |  |
|  +----------------------------------------------------+  |
|                                                           |
|  +-- Preparation Required -----------------------------+ |
|  | - Fast for 12 hours before appointment             | |
|  | - Bring list of current medications                | |
|  | - Arrive 15 minutes early                          | |
|  +-----------------------------------------------------+ |
|                                                           |
|            [Cancel]                    [Schedule]         |
+----------------------------------------------------------+
```

## Screening Detail View

```
+----------------------------------------------------------+
|  < Back to Screenings                                     |
+----------------------------------------------------------+
|                                                           |
|  ANNUAL PHYSICAL EXAM                                     |
|  Status: [SCHEDULED]                                      |
|                                                           |
|  +-- Appointment Details ------------------------------+ |
|  | Date:     February 1, 2025 at 9:00 AM              | |
|  | Provider: Dr. Smith, Primary Care                   | |
|  | Location: Main Street Medical Center                | |
|  |           123 Main St, City, ST 12345              | |
|  +-----------------------------------------------------+ |
|                                                           |
|  +-- Preparation Checklist ----------------------------+ |
|  | [ ] Fast for 12 hours before                       | |
|  | [ ] Bring insurance card                           | |
|  | [ ] List of current medications                    | |
|  | [ ] Questions for doctor                           | |
|  +-----------------------------------------------------+ |
|                                                           |
|  +-- Insurance ----------------------------------------+ |
|  | Coverage: Preventive Care - 100% covered           | |
|  | Copay:    $0                                       | |
|  +-----------------------------------------------------+ |
|                                                           |
|  [Reschedule]  [Cancel Appointment]  [Mark Complete]      |
+----------------------------------------------------------+
```
