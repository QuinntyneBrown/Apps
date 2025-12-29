# Autopay Wireframes

## 1. Autopay Dashboard

```
+------------------------------------------------------------------+
|  Autopay Management                       [+ Enable Autopay]     |
+------------------------------------------------------------------+
|                                                                   |
|  +---------------+ +---------------+ +---------------+           |
|  | Enabled       | | Pending       | | Est. Savings  |           |
|  |     8         | |      2        | |   $120/month  |           |
|  +---------------+ +---------------+ +---------------+           |
|                                                                   |
|  ⚠ 2 payments pending your approval                    [Review]  |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | Bill          | Payment Method | Next Exec  | Status  | ⚙   | |
|  +-------------------------------------------------------------+ |
|  | Electric Co.  | Chase (...67) | Jan 12     | 🟢 On   |[...]| |
|  | Rent          | Chase (...67) | Jan 1      | 🟢 On   |[...]| |
|  | Car Insurance | Visa (...89)  | Jan 8      | 🟠 Paused|[...]| |
|  | Netflix       | Chase (...67) | Jan 5      | 🟢 On   |[...]| |
|  +-------------------------------------------------------------+ |
|                                                                   |
+------------------------------------------------------------------+
```

## 2. Enable Autopay Form

```
+------------------------------------------------------------------+
|  Enable Autopay                                            [✕]   |
+------------------------------------------------------------------+
|                                                                   |
|  Select Bill *                                                    |
|  [Electric Company - $145.00                                ▼]   |
|                                                                   |
|  Payment Method *                                                 |
|  [Chase Checking (...4567)                                  ▼]   |
|                                                                   |
|  Execution Timing                                                 |
|  Execute [0  ▼] days before due date                            |
|                                                                   |
|  Safety Limits                                                    |
|  [ ] Set maximum payment amount                                  |
|  [✓] Require approval if bill changes by more than:              |
|      [  10  ] %                                                   |
|  |-----|-----|-----|-----|-----|-----|------|                     |
|  0     5     10    15    20    25    30                          |
|                                                                   |
|  Failure Handling                                                 |
|  [✓] Disable autopay after 3 consecutive failures                |
|  [✓] Send notification on each failure                           |
|                                                                   |
|  Summary:                                                         |
|  • Next payment: January 15, 2025                                |
|  • Amount: $145.00 (± 10% approval threshold)                   |
|  • Method: Chase Checking (...4567)                              |
|                                                                   |
|  [Cancel]                                    [Enable Autopay]    |
|                                                                   |
+------------------------------------------------------------------+
```

## 3. Pending Approvals

```
+------------------------------------------------------------------+
|  Pending Autopay Approvals                              [Approve All]|
+------------------------------------------------------------------+
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | Electric Company - Amount Change Detected                   | |
|  | ----------------------------------------------------------- | |
|  | Previous Amount: $142.50                                    | |
|  | New Amount:      $145.00 (+1.8%)                           | |
|  | Due Date: January 15, 2025                                  | |
|  | Payment Method: Chase Checking (...4567)                    | |
|  |                                                             | |
|  | [Deny]                                           [Approve]  | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | Water Bill - Amount Change Detected                         | |
|  | ----------------------------------------------------------- | |
|  | Previous Amount: $40.00                                     | |
|  | New Amount:      $52.00 (+30%)                             | |
|  | Due Date: January 20, 2025                                  | |
|  | Payment Method: Chase Checking (...4567)                    | |
|  |                                                             | |
|  | [Deny]                                           [Approve]  | |
|  +-------------------------------------------------------------+ |
|                                                                   |
+------------------------------------------------------------------+
```
