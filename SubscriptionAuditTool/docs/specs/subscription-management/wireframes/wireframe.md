# Subscription Management Wireframes

## 1. Subscriptions Dashboard

```
+------------------------------------------------------------------+
|  Subscription Audit Tool                  [+ Add Subscription]   |
+------------------------------------------------------------------+
|                                                                   |
|  +---------------+ +---------------+ +---------------+           |
|  | Monthly Cost  | | Annual Cost   | | Active Subs   |           |
|  |   $287.50     | |   $3,450      | |      23       |           |
|  +---------------+ +---------------+ +---------------+           |
|                                                                   |
|  Filter: [All ▼] [Category ▼]        Sort: [Cost (High-Low) ▼]  |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | Service      | Cost/Month | Next Bill  | Category | Status | |
|  +-------------------------------------------------------------+ |
|  | Netflix      | $15.99     | Jan 5      | Media    | 🟢     | |
|  | Spotify      | $9.99      | Jan 8      | Music    | 🟢     | |
|  | Adobe CC     | $54.99     | Jan 12     | Software | 🟢     | |
|  | NYTimes      | $17.00     | Jan 15     | News     | 🟢     | |
|  | Gym          | $39.99     | Jan 20     | Fitness  | 🟢     | |
|  | [...]        |            |            |          |        | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  Category Breakdown                                               |
|  Media:     $45.98  [████████░░] 16%                            |
|  Software:  $89.97  [████████████████░░] 31%                    |
|  Fitness:   $39.99  [██████░░] 14%                              |
|  Services:  $67.52  [██████████░░] 23%                          |
|                                                                   |
+------------------------------------------------------------------+
```

## 2. Add Subscription Form

```
+------------------------------------------------------------------+
|  Add Subscription                                          [✕]   |
+------------------------------------------------------------------+
|                                                                   |
|  Service Name *                                                   |
|  [Netflix_______________]  [Popular: Netflix Spotify Amazon...]  |
|                                                                   |
|  Cost *                  Billing Frequency *                      |
|  [$15.99__]              (•) Monthly  ( ) Quarterly  ( ) Annual  |
|                                                                   |
|  Category *              Payment Method *                         |
|  [Streaming Media   ▼]   [Visa ending 1234         ▼]           |
|                                                                   |
|  Start Date *            Next Renewal Date *                      |
|  [01/05/2024  ▼]        [02/05/2025  ▼]                         |
|                                                                   |
|  [✓] Currently in free trial                                     |
|  Trial ends: [02/05/2025  ▼]                                    |
|                                                                   |
|  Cancellation Deadline                                            |
|  [02/04/2025  ▼]  (Optional - last day to cancel before bill)   |
|                                                                   |
|  Notes                                                            |
|  [_____________________________________________________________]  |
|                                                                   |
|  [Cancel]                                  [Add Subscription]     |
|                                                                   |
+------------------------------------------------------------------+
```

## 3. Subscription Details

```
+------------------------------------------------------------------+
|  Netflix Subscription                [Edit] [Pause] [Cancel]     |
+------------------------------------------------------------------+
|                                                                   |
|  📺 Netflix Premium                                               |
|  Status: Active 🟢                                                |
|                                                                   |
|  Billing Information                                              |
|  +------------------------------------------------------------+  |
|  | Current Cost:        $15.99/month                          |  |
|  | Original Cost:       $13.99/month (Increased Jan 2024)    |  |
|  | Billing Frequency:   Monthly                               |  |
|  | Next Renewal:        Feb 5, 2025                          |  |
|  | Payment Method:      Visa ****1234                        |  |
|  | Cancel By:           Feb 4, 2025                          |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Subscription History                                             |
|  +------------------------------------------------------------+  |
|  | Date         | Event                    | Amount            |  |
|  |------------------------------------------------------------|  |
|  | Jan 5, 2025  | Renewed                  | $15.99           |  |
|  | Dec 5, 2024  | Renewed                  | $15.99           |  |
|  | Nov 5, 2024  | Renewed                  | $15.99           |  |
|  | Oct 5, 2024  | Renewed                  | $13.99           |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Total Paid to Date: $167.94                                      |
|  Monthly Savings if Cancelled: $15.99                             |
|                                                                   |
+------------------------------------------------------------------+
```
