# Reminders Wireframes

## 1. Reminders Dashboard

```
+------------------------------------------------------------------+
|  Reminders Management                     [+ Create Reminder]    |
+------------------------------------------------------------------+
|                                                                   |
|  +---------------+ +---------------+ +---------------+           |
|  | Active        | | Sent Today    | | Failed        |           |
|  |     15        | |      8        | |      1        |           |
|  +---------------+ +---------------+ +---------------+           |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | Bill Name       | Type      | Schedule    | Channels | ⚙   | |
|  +-------------------------------------------------------------+ |
|  | Electric Co.    | Due Soon  | 3 days      | 📧 📱   |...  | |
|  | Rent            | Upcoming  | 7 days      | 📧      |...  | |
|  | Car Insurance   | Due Soon  | 2 days      | 📧 📱 🔔|...  | |
|  | Netflix         | Upcoming  | 5 days      | 🔔      |...  | |
|  +-------------------------------------------------------------+ |
|                                                                   |
+------------------------------------------------------------------+
```

## 2. Create Reminder Form

```
+------------------------------------------------------------------+
|  Create Reminder                                           [✕]   |
+------------------------------------------------------------------+
|                                                                   |
|  Select Bill *                                                    |
|  [Electric Company                                          ▼]   |
|                                                                   |
|  Reminder Type *                                                  |
|  ( ) Upcoming Payment (7+ days before)                           |
|  (•) Due Soon (2-6 days before)                                  |
|  ( ) Overdue (after due date)                                    |
|                                                                   |
|  Send Reminder                                                    |
|  [   3   ] days before due date                                  |
|  |-----|-----|-----|-----|-----|-----|------|                     |
|  1     5     10    15    20    25    30                          |
|                                                                   |
|  Notification Channels *                                          |
|  [✓] Email      (john@example.com) ✓ Verified                   |
|  [✓] SMS        (+1 555-0123) ✓ Verified                        |
|  [✓] Push       Mobile app notifications                         |
|                                                                   |
|  Preview:                                                         |
|  +----------------------------------------------------------+    |
|  | Reminder: Electric Company bill due in 3 days           |    |
|  | Amount: $145.00 | Due Date: Jan 15, 2025                 |    |
|  +----------------------------------------------------------+    |
|                                                                   |
|  [Test Reminder]          [Cancel]          [Create Reminder]    |
|                                                                   |
+------------------------------------------------------------------+
```

## 3. Notification Preferences

```
+------------------------------------------------------------------+
|  Notification Preferences                                         |
+------------------------------------------------------------------+
|                                                                   |
|  Contact Information                                              |
|  Email:  [john@example.com_____________] ✓ Verified              |
|  Phone:  [+1 555-0123__________________] ✓ Verified              |
|                                                                   |
|  Quiet Hours                                                      |
|  [✓] Enable quiet hours                                          |
|  Don't send notifications between:                                |
|  [10:00 PM ▼] and [8:00 AM ▼]                                   |
|                                                                   |
|  Default Notification Channels                                    |
|  Upcoming bills:     [✓] Email  [✓] SMS  [ ] Push               |
|  Overdue bills:      [✓] Email  [✓] SMS  [✓] Push               |
|  Payment confirmed:  [✓] Email  [ ] SMS  [✓] Push               |
|                                                                   |
|  [Save Preferences]                                               |
|                                                                   |
+------------------------------------------------------------------+
```
