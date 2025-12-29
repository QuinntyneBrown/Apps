# Emergency Repairs - Wireframes

## Emergency Dashboard

```
+------------------------------------------------------------------+
|  Emergency Repairs                                                |
+------------------------------------------------------------------+
|  🚨 ACTIVE EMERGENCIES (2)                                        |
|                                                                   |
|  [🚨 REPORT EMERGENCY] <- Large, Red, Prominent Button           |
|                                                                   |
|  Active Emergencies                                               |
|  +------------------------------------------------------------+  |
|  | 🔴 CRITICAL - Burst Pipe in Basement                       |  |
|  | Reported: Today at 2:30 PM (15 minutes ago)               |  |
|  | Status: Provider En Route                                  |  |
|  | Provider: Emergency Plumbing Co. - (555) 911-1111         |  |
|  | [📞 Call] [💬 Message] [View Details]                     |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  +------------------------------------------------------------+  |
|  | 🟠 URGENT - No Heat (HVAC Failure)                         |  |
|  | Reported: Today at 9:00 AM (5 hours ago)                  |  |
|  | Status: Temporary Fix Applied                              |  |
|  | Provider: 24/7 HVAC Services - (555) 922-2222             |  |
|  | [📞 Call] [💬 Message] [View Details]                     |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Emergency Contacts (24/7)                                        |
|  +------------------------------------------------------------+  |
|  | Plumbing: Emergency Plumbing Co. ............(555) 911-1111|  |
|  | HVAC: 24/7 HVAC Services .................... (555) 922-2222|  |
|  | Electrical: Quick Electric .................. (555) 933-3333|  |
|  | General: HandyMan Emergency ................. (555) 944-4444|  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Recent Resolutions                                               |
|  +------------------------------------------------------------+  |
|  | ✅ Roof Leak - Resolved Jan 10, 2025 - Cost: $450        |  |
|  | ✅ Power Outage - Resolved Dec 28, 2024 - Cost: $200     |  |
|  +------------------------------------------------------------+  |
|                                                                   |
+------------------------------------------------------------------+
```

## Report Emergency Form

```
+------------------------------------------------------------------+
|  🚨 Report Emergency                                 [X] Close   |
+------------------------------------------------------------------+
|                                                                   |
|  Emergency Type *                                                 |
|  +------------------------------------------------------------+  |
|  | [🚿 Plumbing] [⚡ Electrical] [🔥 HVAC] [🏠 Structural]   |  |
|  | [💧 Flooding] [⛽ Gas]  [🌧 Roof]  [❄️ No Heat]         |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Severity Level *                                                 |
|  +------------------------------------------------------------+  |
|  | [🔴 CRITICAL]  [🟠 URGENT]  [🟡 MODERATE]  [⚪ LOW]      |  |
|  |  Immediate     Same Day     24-48 Hours    This Week      |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Quick Description *                                              |
|  +------------------------------------------------------------+  |
|  | [                                                         ]|  |
|  | [ Burst pipe in basement, water flooding rapidly          ]|  |
|  | [                                                         ]|  |
|  | [🎤 Voice Input]                             200/500 chars|  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Location in Home                                                 |
|  +------------------------------------------------------------+  |
|  | [Basement                                                 ▼]|  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Photo/Video Evidence                                             |
|  +------------------------------------------------------------+  |
|  |  +--------+  +--------+  +--------+  +--------+            |  |
|  |  |[Camera]|  |[Gallery]|  | Photo |  | Video  |            |  |
|  |  |  Take  |  | Upload |  |   1    |  |   1    |            |  |
|  |  +--------+  +--------+  +--------+  +--------+            |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  [🚨 SUBMIT EMERGENCY] <- Large, Red Button                      |
|                                                                   |
|  Note: You will receive immediate confirmation and provider      |
|  contact information based on emergency severity.                 |
|                                                                   |
+------------------------------------------------------------------+
```

## Emergency Detail/Tracking View

```
+------------------------------------------------------------------+
|  [< Back]            Emergency Details                            |
+------------------------------------------------------------------+
|  🔴 CRITICAL EMERGENCY                                            |
|  Burst Pipe in Basement                                           |
|                                                                   |
|  Status Timeline                                                  |
|  +------------------------------------------------------------+  |
|  | 2:30 PM ● Emergency Reported                               |  |
|  | 2:32 PM ● Providers Notified                               |  |
|  | 2:35 PM ● Provider Assigned                                |  |
|  | 2:40 PM ● Provider En Route (ETA: 3:00 PM)                |  |
|  | _______ ○ Provider Arrived (Expected)                      |  |
|  | _______ ○ Work In Progress                                 |  |
|  | _______ ○ Emergency Resolved                               |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Assigned Provider                                                |
|  +------------------------------------------------------------+  |
|  | Emergency Plumbing Co.                                     |  |
|  | Contact: John Smith                                        |  |
|  | Phone: (555) 911-1111                                      |  |
|  |                                                            |  |
|  | [📞 CALL NOW]  [💬 Send Message]  [📍 Track Location]    |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Emergency Details                                                |
|  +------------------------------------------------------------+  |
|  | Type: Plumbing - Burst Pipe                                |  |
|  | Location: Basement                                         |  |
|  | Reported: Today at 2:30 PM                                |  |
|  | Time Elapsed: 15 minutes                                   |  |
|  |                                                            |  |
|  | Description: Burst pipe in basement, water flooding       |  |
|  | rapidly. Main water shut-off valve turned off.             |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Photos/Videos                                                    |
|  +------------------------------------------------------------+  |
|  | [Photo 1] [Photo 2] [Video 1]                              |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Temporary Solution                                               |
|  +------------------------------------------------------------+  |
|  | [ Main water shut off. Buckets placed to catch dripping   ]|  |
|  | [ water. Waiting for emergency plumber.                   ]|  |
|  | [Add/Update]                                               |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Insurance Claim                                                  |
|  +------------------------------------------------------------+  |
|  | [ ] Mark for insurance claim                               |  |
|  | Claim #: [                                               ] |  |
|  +------------------------------------------------------------+  |
|                                                                   |
+------------------------------------------------------------------+
```

---

**Version**: 1.0
**Last Updated**: 2025-12-29
