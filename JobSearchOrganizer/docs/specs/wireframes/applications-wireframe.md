# Applications - Wireframes

## 1. Applications Dashboard (Kanban View)

```
┌─────────────────────────────────────────────────────────────────────────────┐
│ JobSearchOrganizer                    [Search...]  [🔍]   [+ New Application]│
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                              │
│  Applications                                     [Kanban] [List] [Grid]    │
│                                                                              │
│  ┌──────────────────────────────────────────────────────────────────────┐  │
│  │ Stats: 45 Total | 3 Draft | 8 Submitted | 5 Interviewing | 15 Rejected│  │
│  └──────────────────────────────────────────────────────────────────────┘  │
│                                                                              │
│  Filters: Status [All ▾] Company [All ▾] Date [Last 30 days ▾] [Clear]    │
│                                                                              │
│ ┌────────┬────────┬────────┬────────┬────────┬────────┬────────┬────────┐ │
│ │ DRAFT  │SUBMIT  │UNDER   │PHONE   │TECH    │INTERV  │BACKGR  │OFFER   │ │
│ │  (3)   │TED(8)  │REVIEW  │SCREEN  │ASSESS  │IEWING  │CHECK   │EXTENDED│ │
│ │        │        │ (12)   │  (4)   │  (3)   │  (5)   │  (1)   │  (2)   │ │
│ ├────────┼────────┼────────┼────────┼────────┼────────┼────────┼────────┤ │
│ │┌──────┐│┌──────┐│┌──────┐│┌──────┐│┌──────┐│┌──────┐│┌──────┐│┌──────┐│ │
│ ││Sr Sw ││││Jr Sw ││││Full  ││││DevOps││││Data  ││││Sr SW ││││Product││││Sr SW││ │
│ ││Eng   ││││Eng   ││││Stack ││││Eng   ││││Eng   ││││Eng   ││││Mgr   ││││Eng  ││ │
│ ││      ││││      ││││Dev   ││││      ││││      ││││      ││││      ││││     ││ │
│ ││Tech  ││││Acme  ││││StartX││││Cloud ││││DataIn││││Tech  ││││BigCo ││││MegaC││ │
│ ││Corp  ││││Corp  ││││YZ    ││││Tech  ││││c     ││││Corp  ││││      ││││orp  ││ │
│ ││      ││││      ││││      ││││      ││││      ││││      ││││      ││││     ││ │
│ ││Dec 26││││Dec 20││││Dec 18││││Dec 15││││Dec 10││││Dec 22││││Dec 5 ││││Dec 1││ │
│ ││      ││││      ││││      ││││      ││││      ││││      ││││      ││││     ││ │
│ ││[View]││││[View]││││[View]││││[View]││││[View]││││[View]││││[View]││││[View││ │
│ │└──────┘│└──────┘│└──────┘│└──────┘│└──────┘│└──────┘│└──────┘│└──────┘│ │
│ │        │        │        │        │        │        │        │        │ │
│ │┌──────┐│┌──────┐│┌──────┐│┌──────┐│        │┌──────┐│        │        │ │
│ ││FE Dev││││BE Eng││││Mobile││││QA Eng││        ││FE Dev││        │        │ │
│ ││...   ││││...   ││││Dev   ││││...   ││        ││...   ││        │        │ │
│ │└──────┘│└──────┘│└──────┘│└──────┘│        │└──────┘│        │        │ │
│ │        │        │        │        │        │        │        │        │ │
│ │┌──────┐│        │        │        │        │        │        │        │ │
│ ││ML Eng││        │        │        │        │        │        │        │ │
│ ││...   ││        │        │        │        │        │        │        │ │
│ │└──────┘│        │        │        │        │        │        │        │ │
│ └────────┴────────┴────────┴────────┴────────┴────────┴────────┴────────┘ │
│                                                                              │
│ ┌────────────────────────────────────────────────────────────────────────┐ │
│ │                      ⚠ REJECTED (15) | WITHDRAWN (2)                   │ │
│ └────────────────────────────────────────────────────────────────────────┘ │
│                                                                              │
└─────────────────────────────────────────────────────────────────────────────┘
```

## 2. Start New Application

```
┌─────────────────────────────────────────────────────────────────────────────┐
│ JobSearchOrganizer                                           [Profile ▾]    │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                              │
│  < Back to Applications                                                      │
│                                                                              │
│  Start New Application                                                       │
│  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━  │
│                                                                              │
│  Select Job Listing *                                                        │
│  ┌───────────────────────────────────────────────────────────────────┐     │
│  │ Senior Software Engineer - Tech Corp                           ▾  │     │
│  └───────────────────────────────────────────────────────────────────┘     │
│                                                                              │
│  Recently Saved Listings:                                                    │
│  ┌──────────────────────────────────────────────────────────────────┐      │
│  │ ○ Senior Software Engineer - Tech Corp (High Priority)          │      │
│  │ ○ Full Stack Developer - StartupXYZ (Medium Priority)           │      │
│  │ ○ DevOps Engineer - CloudTech (High Priority)                   │      │
│  │ ○ Frontend Developer - WebCo (Medium Priority)                  │      │
│  └──────────────────────────────────────────────────────────────────┘      │
│                                                                              │
│  Target Submission Date (optional)                                           │
│  ┌───────────────────────────────────────────────────────────────────┐     │
│  │ 01/15/2026                                                 [📅]   │     │
│  └───────────────────────────────────────────────────────────────────┘     │
│                                                                              │
│  Initial Notes (optional)                                                    │
│  ┌───────────────────────────────────────────────────────────────────┐     │
│  │ Need to tailor resume for Azure cloud experience.                │     │
│  │ Prepare portfolio showcasing microservices projects.             │     │
│  │ Review company values and recent news.                            │     │
│  └───────────────────────────────────────────────────────────────────┘     │
│  0/2000 characters                                                           │
│                                                                              │
│  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━  │
│                                                                              │
│  [Cancel]                                        [Start Application]         │
│                                                                              │
└─────────────────────────────────────────────────────────────────────────────┘
```

## 3. Submit Application Form

```
┌─────────────────────────────────────────────────────────────────────────────┐
│ JobSearchOrganizer                                           [Profile ▾]    │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                              │
│  < Back to Application                                                       │
│                                                                              │
│  Submit Application: Senior Software Engineer at Tech Corp                   │
│  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━  │
│                                                                              │
│  Submission Details                                                          │
│                                                                              │
│  Submission Method *                                                         │
│  ┌───────────────────────────────────────────────────────────────────┐     │
│  │ ⦿ Online Portal    ○ Email    ○ In Person    ○ Recruiter    ○ Referral   │
│  └───────────────────────────────────────────────────────────────────┘     │
│                                                                              │
│  Confirmation Number (optional)                                              │
│  ┌───────────────────────────────────────────────────────────────────┐     │
│  │ APP-2025-12345                                                     │     │
│  └───────────────────────────────────────────────────────────────────┘     │
│                                                                              │
│  Submitted Date & Time *                                                     │
│  ┌─────────────────────────────┐  ┌──────────────────────────────────┐     │
│  │ 12/28/2025              [📅]│  │ 10:30 AM                    [🕐]│     │
│  └─────────────────────────────┘  └──────────────────────────────────┘     │
│                                                                              │
│  Documents Submitted * (Select at least one)                                 │
│  ┌──────────────────────────────────────────────────────────────────┐      │
│  │ ☑ Resume                          ☑ Cover Letter                 │      │
│  │ ☑ Portfolio                       ☐ References                   │      │
│  │ ☐ Transcripts                     ☐ Writing Samples              │      │
│  │ ☐ Certifications                  ☐ Other: _______________        │      │
│  └──────────────────────────────────────────────────────────────────┘      │
│                                                                              │
│  Submission Notes (optional)                                                 │
│  ┌───────────────────────────────────────────────────────────────────┐     │
│  │ Submitted through company careers portal.                         │     │
│  │ Tailored resume to highlight Azure and .NET experience.           │     │
│  │ Referenced John Doe's referral in application.                    │     │
│  └───────────────────────────────────────────────────────────────────┘     │
│  156/2000 characters                                                         │
│                                                                              │
│  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━  │
│                                                                              │
│  [Cancel]           [Save as Draft]              [Submit Application]       │
│                                                                              │
└─────────────────────────────────────────────────────────────────────────────┘
```

## 4. Application Detail View

```
┌─────────────────────────────────────────────────────────────────────────────┐
│ JobSearchOrganizer                                           [Profile ▾]    │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                              │
│  < Back to Applications                                                      │
│                                                                              │
│  ┌─────────────────────────────────────────────────────────────────────┐   │
│  │                           [INTERVIEWING] Status                      │   │
│  │                                                                      │   │
│  │ Senior Software Engineer                                            │   │
│  │ Tech Corp • San Francisco, CA                                       │   │
│  │                                                                      │   │
│  │ [Update Status] [Add Note] [Withdraw] [Schedule Interview] [More▾] │   │
│  └─────────────────────────────────────────────────────────────────────┘   │
│                                                                              │
│  ┌──────────────────────┐  ┌─────────────────────────────────────────┐    │
│  │ Application Info     │  │ Status Timeline                          │    │
│  │                      │  │                                          │    │
│  │ 📋 Application ID:   │  │ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ │    │
│  │ APP-2025-12345       │  │                                          │    │
│  │                      │  │ ● Interviewing - Dec 26, 2025           │    │
│  │ 📅 Started:          │  │   Updated by: Jane Smith, HR            │    │
│  │ Dec 20, 2025         │  │   Second round interview scheduled      │    │
│  │                      │  │   Next: Technical interview on Jan 5    │    │
│  │ 📤 Submitted:        │  │                                          │    │
│  │ Dec 22, 2025         │  │ ● Phone Screen - Dec 24, 2025           │    │
│  │ (6 days ago)         │  │   Updated by: John Doe, Recruiter      │    │
│  │                      │  │   Phone screen completed successfully   │    │
│  │ 🎯 Method:           │  │   Next: HR will contact for next steps │    │
│  │ Online Portal        │  │                                          │    │
│  │                      │  │ ● Under Review - Dec 23, 2025           │    │
│  │ 📄 Documents:        │  │   Application received and processing   │    │
│  │ • Resume             │  │                                          │    │
│  │ • Cover Letter       │  │ ● Submitted - Dec 22, 2025              │    │
│  │ • Portfolio          │  │   Via online portal                     │    │
│  │                      │  │   Confirmation: APP-2025-12345          │    │
│  │ 🔗 Job Listing       │  │                                          │    │
│  │ [View Details →]     │  │ ○ Started - Dec 20, 2025                │    │
│  │                      │  │   Application draft created             │    │
│  └──────────────────────┘  │                                          │    │
│                             │ [Show full timeline →]                   │    │
│                             └─────────────────────────────────────────┘    │
│                                                                              │
│  ┌─────────────────────────────────────────────────────────────────────┐   │
│  │ Notes & Next Steps                                        [Edit]    │   │
│  │                                                                      │   │
│  │ Tailored resume to highlight Azure cloud experience. Referenced     │   │
│  │ John Doe's referral in application. Strong fit for role based on    │   │
│  │ phone screen feedback.                                              │   │
│  │                                                                      │   │
│  │ Next Steps:                                                          │   │
│  │ • Prepare for technical interview (Jan 5, 2026)                     │   │
│  │ • Review system design patterns                                     │   │
│  │ • Practice coding challenges in C# and .NET                         │   │
│  │ • Research company's tech stack and recent projects                 │   │
│  └─────────────────────────────────────────────────────────────────────┘   │
│                                                                              │
│  ┌─────────────────────────────────────────────────────────────────────┐   │
│  │ Related Items                                                        │   │
│  │                                                                      │   │
│  │ Interviews:                                                          │   │
│  │ • Phone Screen - Dec 24, 2025 (Completed) [View]                    │   │
│  │ • Technical Interview - Jan 5, 2026 (Scheduled) [View]              │   │
│  │ [+ Schedule New Interview]                                          │   │
│  │                                                                      │   │
│  │ Networking:                                                          │   │
│  │ • John Doe (Referral provided) [View Contact]                       │   │
│  │                                                                      │   │
│  │ Offers: None yet                                                     │   │
│  └─────────────────────────────────────────────────────────────────────┘   │
│                                                                              │
└─────────────────────────────────────────────────────────────────────────────┘
```

## 5. Update Status Dialog

```
┌──────────────────────────────────────────────────────────────┐
│ Update Application Status                              [×]   │
├──────────────────────────────────────────────────────────────┤
│                                                               │
│ Current Status: Phone Screen                                 │
│                                                               │
│ New Status *                                                  │
│ ┌──────────────────────────────────────────────────────┐    │
│ │ Interviewing                                      ▾  │    │
│ └──────────────────────────────────────────────────────┘    │
│                                                               │
│ Available next statuses:                                      │
│ • Technical Assessment                                        │
│ • Interviewing                                                │
│ • Rejected                                                    │
│                                                               │
│ Updated By (optional)                                         │
│ ┌──────────────────────────────────────────────────────┐    │
│ │ Jane Smith, HR Manager                                │    │
│ └──────────────────────────────────────────────────────┘    │
│                                                               │
│ Status Notes (optional)                                       │
│ ┌──────────────────────────────────────────────────────┐    │
│ │ Phone screen went well. Scheduling second round      │    │
│ │ interview with engineering team.                     │    │
│ └──────────────────────────────────────────────────────┘    │
│ 82/2000 characters                                           │
│                                                               │
│ Next Steps (optional)                                         │
│ ┌──────────────────────────────────────────────────────┐    │
│ │ Prepare for technical interview on Jan 5, 2026       │    │
│ └──────────────────────────────────────────────────────┘    │
│ 47/500 characters                                            │
│                                                               │
│ Update Date                                                   │
│ ┌──────────────────────────────────────────────────────┐    │
│ │ 12/28/2025  10:30 AM                          [📅]   │    │
│ └──────────────────────────────────────────────────────┘    │
│                                                               │
│             [Cancel]              [Update Status]            │
│                                                               │
└──────────────────────────────────────────────────────────────┘
```

## 6. Application Statistics Dashboard

```
┌─────────────────────────────────────────────────────────────────────────────┐
│ JobSearchOrganizer                                           [Profile ▾]    │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                              │
│  Application Statistics                                                      │
│                                                                              │
│  ┌────────────────────┐ ┌────────────────────┐ ┌────────────────────┐     │
│  │  TOTAL APPS        │ │   SUCCESS RATE     │ │  AVG RESPONSE TIME │     │
│  │       45           │ │       33%          │ │      4.5 days      │     │
│  │  ↑ 8 this month    │ │  ↑ 5% from last mo │ │   ↓ 1 day faster   │     │
│  └────────────────────┘ └────────────────────┘ └────────────────────┘     │
│                                                                              │
│  ┌────────────────────────────────────────┐ ┌─────────────────────────┐   │
│  │ Applications by Status                 │ │ Application Funnel      │   │
│  │                                        │ │                         │   │
│  │ Draft              ███  3              │ │ Started      █████ 45   │   │
│  │ Submitted          ████████  8         │ │ Submitted    ████  40   │   │
│  │ Under Review       ████████████  12    │ │ Reviewing    ███   35   │   │
│  │ Phone Screen       ████  4             │ │ Interviewing ██    15   │   │
│  │ Tech Assessment    ███  3              │ │ Offers       █     7    │   │
│  │ Interviewing       █████  5            │ │                         │   │
│  │ Background Check   █  1                │ │ Success: 15% (7/45)     │   │
│  │ Offer Extended     ██  2               │ │                         │   │
│  │ Rejected           ███████████████ 15  │ │                         │   │
│  │ Withdrawn          ██  2               │ │                         │   │
│  └────────────────────────────────────────┘ └─────────────────────────┘   │
│                                                                              │
│  ┌─────────────────────────────────────────────────────────────────────┐   │
│  │ Applications Over Time                                              │   │
│  │                                                                      │   │
│  │    20┤                                                      ●       │   │
│  │    15┤                            ●                    ●            │   │
│  │    10┤               ●       ●              ●     ●                 │   │
│  │     5┤      ●   ●                                                   │   │
│  │     0└─────────────────────────────────────────────────────────     │   │
│  │      Oct   Nov   Dec   Jan   Feb   Mar   Apr   May   Jun           │   │
│  └─────────────────────────────────────────────────────────────────────┘   │
│                                                                              │
│  ┌──────────────────────────┐ ┌─────────────────────────────────────┐     │
│  │ Rejection Reasons        │ │ Response Time by Company            │     │
│  │                          │ │                                     │     │
│  │ Qualifications  8        │ │ Tech Corp        3.2 days          │     │
│  │ Position Filled 4        │ │ StartupXYZ       2.1 days          │     │
│  │ Not Qualified   2        │ │ CloudTech        6.7 days          │     │
│  │ Other           1        │ │ MegaCorp Inc     9.3 days          │     │
│  │                          │ │ Average          4.5 days          │     │
│  └──────────────────────────┘ └─────────────────────────────────────┘     │
│                                                                              │
└─────────────────────────────────────────────────────────────────────────────┘
```

## Notes

### Design Principles
- **Process Clarity**: Clear visual representation of application pipeline
- **Status Tracking**: Detailed history and timeline for each application
- **Efficiency**: Drag-and-drop for quick status updates
- **Documentation**: Comprehensive notes and next steps tracking
- **Analytics**: Insights into application success rates and patterns

### Color Coding
- **Draft**: Gray
- **Submitted/Under Review**: Blue shades
- **Screening/Assessment**: Purple/Cyan shades
- **Interviewing**: Yellow/Orange
- **Offer**: Green
- **Rejected**: Red
- **Withdrawn**: Dark gray

### Kanban Interactions
- Drag cards between columns to update status
- Click card to view details
- Hover to show quick actions
- Collapsible columns for space management
- Separate section for rejected/withdrawn to reduce clutter

### Progressive Disclosure
- Summary cards in Kanban
- Full details on dedicated page
- Expandable status timeline
- Collapsible sections for notes and related items
