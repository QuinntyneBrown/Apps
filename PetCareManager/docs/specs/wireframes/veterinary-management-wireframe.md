# Veterinary Management - Wireframes

## Overview
This document provides ASCII-based wireframes for the key screens in the Veterinary Management feature. These wireframes illustrate layout, component placement, and user interaction flows.

---

## 1. Appointments Dashboard

### Desktop View (1200px+)

```
+-----------------------------------------------------------------------------------+
|  PetCareManager                    [Search]              [Notifications] [User]  |
+-----------------------------------------------------------------------------------+
|  Home > Veterinary > Appointments                                                 |
+-----------------------------------------------------------------------------------+
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  APPOINTMENTS DASHBOARD                                      [+ New Appt]  |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|  +-----------------------------+  +------------------------------------------+  |
|  |  FILTERS                    |  |  UPCOMING APPOINTMENTS                   |  |
|  |                             |  |                                          |  |
|  |  Date Range:                |  |  +------------------------------------+  |  |
|  |  [From: MM/DD/YY] - [To: ]  |  |  | Jan 15, 2025 | 10:00 AM             |  |  |
|  |                             |  |  | Max (Golden Retriever)              |  |  |
|  |  Pet:                       |  |  | Dr. Sarah Johnson                   |  |  |
|  |  [ All Pets         v ]     |  |  | Wellness Checkup     [Confirmed]    |  |  |
|  |                             |  |  | [Reschedule] [Cancel] [Directions]  |  |  |
|  |  Status:                    |  |  +------------------------------------+  |  |
|  |  [x] Scheduled              |  |                                          |  |  |
|  |  [x] Confirmed              |  |  +------------------------------------+  |  |
|  |  [ ] Completed              |  |  | Jan 18, 2025 | 2:30 PM              |  |  |
|  |  [ ] Cancelled              |  |  | Luna (Persian Cat)                  |  |  |
|  |                             |  |  | Dr. Michael Chen                    |  |  |
|  |  Type:                      |  |  | Vaccination          [Scheduled]    |  |  |
|  |  [ ] Wellness Checkup       |  |  | [Reschedule] [Cancel] [Directions]  |  |  |
|  |  [ ] Vaccination            |  |  +------------------------------------+  |  |
|  |  [ ] Surgery                |  |                                          |  |  |
|  |  [ ] Emergency              |  |  +------------------------------------+  |  |
|  |                             |  |  | Jan 22, 2025 | 11:00 AM             |  |  |
|  |  [Clear Filters]            |  |  | Buddy (Beagle)                      |  |  |
|  |                             |  |  | Dr. Sarah Johnson                   |  |  |
|  +-----------------------------+  |  | Dental Cleaning      [Scheduled]    |  |  |
|                                    |  | [Reschedule] [Cancel] [Directions]  |  |  |
|                                    |  +------------------------------------+  |  |
|                                    |                                          |  |
|                                    |  [Load More Appointments]                |  |
|                                    +------------------------------------------+  |
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  PAST APPOINTMENTS                                         [View All]       |  |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  |  +------------------------------+  +------------------------------+         |  |
|  |  | Dec 10, 2024 | Completed    |  | Nov 15, 2024 | Completed    |         |  |
|  |  | Max - Wellness Checkup      |  | Luna - Vaccination           |         |  |
|  |  | Dr. Sarah Johnson           |  | Dr. Michael Chen             |         |  |
|  |  | [View Details]              |  | [View Details]               |         |  |
|  |  +------------------------------+  +------------------------------+         |  |
|  |                                                                             |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
+-----------------------------------------------------------------------------------+
```

### Mobile View (< 768px)

```
+---------------------------+
| < PetCareManager    [User]|
+---------------------------+
| Appointments              |
+---------------------------+
|                           |
| [+] Schedule New          |
|                           |
| [Filter v] [Sort v]       |
+---------------------------+
|                           |
| UPCOMING (3)              |
|                           |
| +---------------------+   |
| | Jan 15 | 10:00 AM  |   |
| | Max                |   |
| | Wellness Checkup   |   |
| | Dr. Sarah Johnson  |   |
| | [Confirmed]        |   |
| | [View] [Reschedule]|   |
| +---------------------+   |
|                           |
| +---------------------+   |
| | Jan 18 | 2:30 PM   |   |
| | Luna               |   |
| | Vaccination        |   |
| | Dr. Michael Chen   |   |
| | [Scheduled]        |   |
| | [View] [Reschedule]|   |
| +---------------------+   |
|                           |
| +---------------------+   |
| | Jan 22 | 11:00 AM  |   |
| | Buddy              |   |
| | Dental Cleaning    |   |
| | [Scheduled]        |   |
| | [View] [Reschedule]|   |
| +---------------------+   |
|                           |
| [Load More]               |
|                           |
+===========================+
| PAST                      |
| [View All]                |
+---------------------------+
```

---

## 2. Schedule Appointment Page

### Desktop View

```
+-----------------------------------------------------------------------------------+
|  PetCareManager                    [Search]              [Notifications] [User]  |
+-----------------------------------------------------------------------------------+
|  Home > Veterinary > Schedule Appointment                                         |
+-----------------------------------------------------------------------------------+
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  SCHEDULE APPOINTMENT                                                       |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|  Step 1 of 4: Select Pet                                      [Save Draft]       |
|                                                                                   |
|  +-----------------------------------+  +-----------------------------------+     |
|  |  +----+                           |  |  +----+                           |     |
|  |  |IMG |  Max                      |  |  |IMG |  Luna                     |     |
|  |  |    |  Golden Retriever         |  |  |    |  Persian Cat              |     |
|  |  +----+  3 years old       [✓]    |  |  +----+  2 years old       [ ]    |     |
|  +-----------------------------------+  +-----------------------------------+     |
|                                                                                   |
|  +-----------------------------------+                                            |
|  |  +----+                           |                                            |
|  |  |IMG |  Buddy                    |                                            |
|  |  |    |  Beagle                   |                                            |
|  |  +----+  5 years old       [ ]    |                                            |
|  +-----------------------------------+                                            |
|                                                                                   |
|  [< Back]                                                   [Continue >]          |
|                                                                                   |
+-----------------------------------------------------------------------------------+


+-----------------------------------------------------------------------------------+
|  Step 2 of 4: Appointment Type                                                   |
|                                                                                   |
|  +------------------+  +------------------+  +------------------+                 |
|  |   [Calendar]     |  |    [Syringe]     |  |    [Scalpel]     |                 |
|  |                  |  |                  |  |                  |                 |
|  |  WELLNESS        |  |  VACCINATION     |  |  SURGERY         |                 |
|  |  CHECKUP         |  |                  |  |                  |                 |
|  |                  |  |                  |  |                  |                 |
|  |  30-45 min       |  |  15-30 min       |  |  1-3 hours       |                 |
|  |  $80-120         |  |  $50-100         |  |  $500-2000       |                 |
|  |                  |  |                  |  |                  |                 |
|  |      [✓]         |  |      [ ]         |  |      [ ]         |                 |
|  +------------------+  +------------------+  +------------------+                 |
|                                                                                   |
|  +------------------+  +------------------+  +------------------+                 |
|  |    [Tooth]       |  |   [First Aid]    |  |  [Stethoscope]   |                 |
|  |                  |  |                  |  |                  |                 |
|  |  DENTAL          |  |  EMERGENCY       |  |  CONSULTATION    |                 |
|  |  CLEANING        |  |                  |  |                  |                 |
|  |                  |  |                  |  |                  |                 |
|  |  45-60 min       |  |  Varies          |  |  30 min          |                 |
|  |  $150-300        |  |  $200+           |  |  $60-100         |                 |
|  |                  |  |                  |  |                  |                 |
|  |      [ ]         |  |      [ ]         |  |      [ ]         |                 |
|  +------------------+  +------------------+  +------------------+                 |
|                                                                                   |
|  [< Back]                                                   [Continue >]          |
|                                                                                   |
+-----------------------------------------------------------------------------------+


+-----------------------------------------------------------------------------------+
|  Step 3 of 4: Select Date, Time & Veterinarian                                   |
|                                                                                   |
|  +-----------------------------+  +------------------------------------------+   |
|  |  JANUARY 2025               |  |  AVAILABLE VETERINARIANS                 |   |
|  |                             |  |                                          |   |
|  |  S  M  T  W  T  F  S        |  |  +------------------------------------+  |   |
|  |     1  2  3  4  5  6        |  |  | +--+                               |  |   |
|  |  7  8  9 10 11 12 13        |  |  | |  | Dr. Sarah Johnson            |  |   |
|  | 14[15]16 17 18 19 20        |  |  | +--+ General Practice       [✓]   |  |   |
|  | 21 22 23 24 25 26 27        |  |  |      4.9★ (152 reviews)           |  |   |
|  | 28 29 30 31                 |  |  |      Available: 9 slots           |  |   |
|  |                             |  |  +------------------------------------+  |   |
|  |  < Dec    Today    Feb >    |  |                                          |   |
|  +-----------------------------+  |  +------------------------------------+  |   |
|                                    |  | +--+                               |  |   |
|  AVAILABLE TIME SLOTS              |  | |  | Dr. Michael Chen             |  |   |
|  (January 15, 2025)                |  | +--+ Surgery Specialist    [ ]   |  |   |
|                                    |  |      4.8★ (98 reviews)            |  |   |
|  Morning                           |  |      Available: 3 slots           |  |   |
|  [9:00 AM] [9:30 AM] [10:00 AM✓]  |  +------------------------------------+  |   |
|  [10:30 AM] [11:00 AM] [11:30 AM] |  |                                          |   |
|                                    |  |  [ ] Any Available Veterinarian      |   |
|  Afternoon                         |  +------------------------------------------+   |
|  [1:00 PM] [1:30 PM] [2:00 PM]    |                                              |
|  [2:30 PM] [3:00 PM] [3:30 PM]    |  Reason for Visit (Optional)                 |
|                                    |  +------------------------------------------+   |
|  Evening                           |  |                                          |   |
|  [4:00 PM] [4:30 PM] [5:00 PM]    |  |  Annual wellness checkup for Max...      |   |
|  [Unavailable: Fully booked]       |  |                                          |   |
|                                    |  |  (Character count: 34/500)               |   |
|                                    |  +------------------------------------------+   |
|                                                                                   |
|  [< Back]                                                   [Continue >]          |
|                                                                                   |
+-----------------------------------------------------------------------------------+


+-----------------------------------------------------------------------------------+
|  Step 4 of 4: Review & Confirm                                                   |
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  APPOINTMENT SUMMARY                                                        |  |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  |  Pet:              Max (Golden Retriever, 3 years)                    [Edit]|  |
|  |  Type:             Wellness Checkup                                   [Edit]|  |
|  |  Date & Time:      Wednesday, January 15, 2025 at 10:00 AM          [Edit]|  |
|  |  Veterinarian:     Dr. Sarah Johnson                                  [Edit]|  |
|  |  Reason:           Annual wellness checkup for Max                    [Edit]|  |
|  |                                                                             |  |
|  |  -------------------------------------------------------------------------  |  |
|  |                                                                             |  |
|  |  Clinic Location:  PetCare Veterinary Clinic                                |  |
|  |                    123 Main Street, Suite 100                               |  |
|  |                    Cityville, ST 12345                                      |  |
|  |                    [Get Directions]                                         |  |
|  |                                                                             |  |
|  |  Contact:          (555) 123-4567                              [Call Now]   |  |
|  |                                                                             |  |
|  |  -------------------------------------------------------------------------  |  |
|  |                                                                             |  |
|  |  Estimated Cost:   $80 - $120                                               |  |
|  |  Duration:         30-45 minutes                                            |  |
|  |                                                                             |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  PRE-APPOINTMENT INSTRUCTIONS                                               |  |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  |  • Please arrive 10 minutes early for check-in                             |  |
|  |  • Bring previous vaccination records if available                         |  |
|  |  • Ensure your pet is on a leash or in a carrier                           |  |
|  |  • Bring any medications your pet is currently taking                      |  |
|  |                                                                             |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  CANCELLATION POLICY                                                        |  |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  |  Free cancellation up to 24 hours before appointment. Late cancellations   |  |
|  |  may incur a $25 fee.                                                       |  |
|  |                                                                             |  |
|  |  [x] I agree to the cancellation policy                                     |  |
|  |                                                                             |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|  [ ] Add to my calendar    [ ] Send reminder 1 day before                        |
|                                                                                   |
|  [< Back]                              [Cancel]  [Schedule Appointment]          |
|                                                                                   |
+-----------------------------------------------------------------------------------+
```

---

## 3. Emergency Report Page

### Desktop View

```
+-----------------------------------------------------------------------------------+
|  PetCareManager                    [Search]              [Notifications] [User]  |
+-----------------------------------------------------------------------------------+
|  Home > Veterinary > Emergency Report                                             |
+-----------------------------------------------------------------------------------+
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  ⚠️  EMERGENCY REPORT                                                       |  |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  |  For life-threatening emergencies, call 24/7 hotline:                      |  |
|  |  [CALL NOW: 1-800-VET-HELP]                                                 |  |
|  |                                                                             |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|  SELECT PET                                                                       |
|                                                                                   |
|  +---------------+  +---------------+  +---------------+                          |
|  | +---+         |  | +---+         |  | +---+         |                          |
|  | |   |         |  | |   |         |  | |   |         |                          |
|  | |   | Max     |  | |   | Luna    |  | |   | Buddy   |                          |
|  | +---+         |  | +---+         |  | +---+         |                          |
|  |    [SELECT]   |  |    [SELECT]   |  |    [SELECT]   |                          |
|  +---------------+  +---------------+  +---------------+                          |
|                                                                                   |
|  EMERGENCY TYPE                                                                   |
|                                                                                   |
|  +---------------+  +---------------+  +---------------+  +---------------+       |
|  | [Bandaid]     |  | [Poison]      |  | [Medical]     |  | [Car]         |       |
|  |               |  |               |  |               |  |               |       |
|  | INJURY/       |  | POISONING     |  | ACUTE         |  | ACCIDENT      |       |
|  | TRAUMA  [✓]   |  |        [ ]    |  | ILLNESS [ ]   |  |        [ ]    |       |
|  +---------------+  +---------------+  +---------------+  +---------------+       |
|                                                                                   |
|  +---------------+  +---------------+  +---------------+                          |
|  | [Lungs]       |  | [Lightning]   |  | [Question]    |                          |
|  |               |  |               |  |               |                          |
|  | DIFFICULTY    |  | SEIZURE       |  | OTHER         |                          |
|  | BREATHING [ ] |  |        [ ]    |  |        [ ]    |                          |
|  +---------------+  +---------------+  +---------------+                          |
|                                                                                   |
|  SYMPTOMS (Select all that apply)                                                 |
|                                                                                   |
|  [x] Bleeding          [x] Limping          [ ] Vomiting       [ ] Lethargy      |
|  [ ] Loss of Consciousness    [ ] Difficulty Breathing    [ ] Seizures           |
|  [ ] Swelling          [ ] Whimpering       [ ] Loss of Appetite                 |
|                                                                                   |
|  DESCRIPTION *                                                                    |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  | Max was hit by a car about 15 minutes ago. He is bleeding from his        |  |
|  | left hind leg and is unable to put weight on it. He is conscious           |  |
|  | and alert but whimpering in pain.                                          |  |
|  |                                                                             |  |
|  |                                                                             |  |
|  | (Character count: 184/1000)                                [Voice Input]    |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|  URGENCY LEVEL *                                                                  |
|                                                                                   |
|  ( ) Critical - Immediate life-threatening emergency                             |
|  (•) High - Very urgent, requires immediate attention                            |
|  ( ) Medium - Urgent but pet is stable                                           |
|                                                                                   |
|  CURRENT LOCATION                                                                 |
|  +----------------------------------------------+  [Detect My Location]           |
|  | 123 Main Street, Cityville, ST 12345         |                               |
|  +----------------------------------------------+                                 |
|                                                                                   |
|  CONTACT NUMBER *                                                                 |
|  +----------------------------------------------+                                 |
|  | (555) 123-4567                               |                               |
|  +----------------------------------------------+                                 |
|                                                                                   |
|  UPLOAD PHOTOS (Optional)                                                         |
|  [📷 Take Photo] [📁 Upload Photo]                                               |
|                                                                                   |
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  NEAREST EMERGENCY CLINICS                                                  |  |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  |  1. 24/7 Emergency Vet Clinic                              2.3 miles       |  |
|  |     456 Oak Avenue, Cityville                              Est: 7 min       |  |
|  |     (555) 999-8888        [Call] [Get Directions]                          |  |
|  |     Wait Time: 15 minutes                                                   |  |
|  |                                                                             |  |
|  |  2. Animal Emergency Hospital                              3.8 miles       |  |
|  |     789 Elm Street, Cityville                              Est: 12 min      |  |
|  |     (555) 777-6666        [Call] [Get Directions]                          |  |
|  |     Wait Time: 30 minutes                                                   |  |
|  |                                                                             |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|                                                                                   |
|  [Cancel]                                      [SUBMIT EMERGENCY REPORT]          |
|                                                                                   |
+-----------------------------------------------------------------------------------+
```

### Mobile View

```
+---------------------------+
| < Emergency Report   [Help]|
+---------------------------+
| ⚠️  EMERGENCY             |
|                           |
| [📞 CALL HOTLINE]         |
| 1-800-VET-HELP            |
+---------------------------+
|                           |
| SELECT PET                |
|                           |
| +---------------------+   |
| | [IMG] Max          ✓|   |
| | Golden Retriever    |   |
| +---------------------+   |
| [Show All Pets]           |
|                           |
+---------------------------+
| EMERGENCY TYPE            |
|                           |
| [🩹 Injury/Trauma]    ✓   |
| [☠️  Poisoning]            |
| [🤒 Acute Illness]         |
| [🚗 Accident]              |
| [🫁 Difficulty Breathing]  |
| [⚡ Seizure]               |
| [❓ Other]                 |
|                           |
+---------------------------+
| SYMPTOMS                  |
|                           |
| ✓ Bleeding                |
| ✓ Limping                 |
| □ Vomiting                |
| □ Lethargy                |
| [Show All Symptoms]       |
|                           |
+---------------------------+
| DESCRIPTION *             |
| +---------------------+   |
| | Max was hit by a    |   |
| | car. Bleeding from  |   |
| | leg, can't walk...  |   |
| |                     |   |
| | [🎤 Voice]          |   |
| +---------------------+   |
|                           |
+---------------------------+
| URGENCY LEVEL *           |
|                           |
| ( ) Critical              |
| (•) High                  |
| ( ) Medium                |
|                           |
+---------------------------+
| LOCATION                  |
| +---------------------+   |
| | 123 Main St...      |   |
| | [📍 Detect Location]|   |
| +---------------------+   |
|                           |
| CONTACT *                 |
| +---------------------+   |
| | (555) 123-4567      |   |
| +---------------------+   |
|                           |
+---------------------------+
| NEAREST CLINICS           |
|                           |
| 24/7 Emergency Vet        |
| 2.3 mi | 7 min | 15m wait |
| [📞] [🗺️]                  |
|                           |
| Animal Emergency Hospital |
| 3.8 mi | 12 min | 30m wait|
| [📞] [🗺️]                  |
|                           |
+---------------------------+
|                           |
| [SUBMIT EMERGENCY]        |
|                           |
+---------------------------+
```

---

## 4. Medical Records Page

### Desktop View

```
+-----------------------------------------------------------------------------------+
|  PetCareManager                    [Search]              [Notifications] [User]  |
+-----------------------------------------------------------------------------------+
|  Home > Veterinary > Medical Records                                              |
+-----------------------------------------------------------------------------------+
|                                                                                   |
|  +-----------------------------+  +------------------------------------------+   |
|  |  +----+                     |  |  MEDICAL RECORDS                         |   |
|  |  |    |  Max                |  |                                          |   |
|  |  |IMG |  Golden Retriever   |  |  [Timeline] [Diagnoses] [Vaccinations]  |   |
|  |  |    |  3 years old        |  |   [Medications] [Documents]              |   |
|  |  +----+                     |  |                                          |   |
|  |                             |  +------------------------------------------+   |
|  |  Weight: 65 lbs             |                                                  |
|  |  Last Visit: Dec 10, 2024   |  DIAGNOSIS TIMELINE                              |
|  |  Next Visit: Jan 15, 2025   |                                                  |
|  |                             |  +----------------------------------------+     |
|  |  Health Status: [Healthy]   |  | 2025 -------------------------------- |     |
|  |  Active Conditions: 0       |  |                                        |     |
|  |                             |  |  • Jan 15, 2025                        |     |
|  |  [Switch Pet]               |  |    Wellness Checkup - Healthy          |     |
|  |  [ ] Max                    |  |    Dr. Sarah Johnson                   |     |
|  |  [ ] Luna                   |  |    [View Details v]                    |     |
|  |  [ ] Buddy                  |  |                                        |     |
|  |                             |  | 2024 -------------------------------- |     |
|  |  [Export Records]           |  |                                        |     |
|  |                             |  |  • Dec 10, 2024                        |     |
|  +-----------------------------+  |    Wellness Checkup - Healthy          |     |
|                                    |    Dr. Sarah Johnson                   |     |
|                                    |    Weight: 65 lbs                      |     |
|                                    |    [View Details v]                    |     |
|                                    |                                        |     |
|                                    |  • Oct 5, 2024                         |     |
|                                    |    Ear Infection                       |     |
|                                    |    Dr. Michael Chen                    |     |
|                                    |    Status: [Resolved]                  |     |
|                                    |    [View Details v]                    |     |
|                                    |    --------------------------------    |     |
|                                    |    Diagnosis: Bacterial ear infection  |     |
|                                    |    Severity: Moderate                  |     |
|                                    |    Treatment: Antibiotic ear drops     |     |
|                                    |    Duration: 7 days                    |     |
|                                    |    Follow-up: Oct 19, 2024 ✓          |     |
|                                    |    Outcome: Fully resolved             |     |
|                                    |    --------------------------------    |     |
|                                    |                                        |     |
|                                    |  • Jun 12, 2024                        |     |
|                                    |    Vaccination - Rabies                |     |
|                                    |    Dr. Sarah Johnson                   |     |
|                                    |    [View Certificate] [Download]       |     |
|                                    |                                        |     |
|                                    | 2023 -------------------------------- |     |
|                                    |    [Show older records]                |     |
|                                    +----------------------------------------+     |
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  ACTIVE MEDICATIONS                                                         |  |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  |  No active medications                                                      |  |
|  |                                                                             |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  VACCINATION STATUS                                           [View All]    |  |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  |  Rabies:           Jun 12, 2024  [✓ Current]      Next: Jun 12, 2027       |  |
|  |  DHPP:             Jun 12, 2024  [✓ Current]      Next: Jun 12, 2027       |  |
|  |  Bordetella:       Jan 5, 2024   [⚠ Due Soon]     Next: Jan 5, 2025        |  |
|  |                                                                             |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
+-----------------------------------------------------------------------------------+
```

---

## 5. Veterinarian Portal - Appointment Workspace

### Desktop View

```
+-----------------------------------------------------------------------------------+
|  PetCareManager Veterinarian Portal            Dr. Sarah Johnson  [Notifications]|
+-----------------------------------------------------------------------------------+
|  [Dashboard] [Schedule] [Patient Queue] [Emergencies] [Reports]                  |
+-----------------------------------------------------------------------------------+
|                                                                                   |
|  APPOINTMENT WORKSPACE                                                            |
|                                                                                   |
|  +-----------------------------+  +------------------------------------------+   |
|  |  PATIENT INFORMATION        |  |  APPOINTMENT DETAILS                     |   |
|  +-----------------------------+  +------------------------------------------+   |
|  |                             |  |                                          |   |
|  |  +----+                     |  |  Date/Time: Jan 15, 2025 @ 10:00 AM     |   |
|  |  |    | Max                 |  |  Type: Wellness Checkup                 |   |
|  |  |IMG | Golden Retriever    |  |  Status: [In Progress]                  |   |
|  |  |    | Male, 3 years       |  |  Duration: 00:23:15 [Stop Timer]        |   |
|  |  +----+                     |  |                                          |   |
|  |                             |  |  Owner: John Smith                       |   |
|  |  Weight: 65 lbs             |  |  Phone: (555) 123-4567  [Call]          |   |
|  |  Microchip: 123456789       |  |                                          |   |
|  |                             |  |  Reason: Annual wellness checkup         |   |
|  |  [View Full Medical History]|  |                                          |   |
|  |                             |  +------------------------------------------+   |
|  +-----------------------------+                                                  |
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  RECENT MEDICAL HISTORY                                                     |  |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  |  • Dec 10, 2024 - Wellness Checkup - Healthy                                |  |
|  |  • Oct 5, 2024 - Ear Infection - Resolved                                   |  |
|  |  • Jun 12, 2024 - Rabies Vaccination                                        |  |
|  |                                                                             |  |
|  |  Active Medications: None                                                   |  |
|  |  Known Allergies: Penicillin                                                |  |
|  |                                                                             |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  VITAL SIGNS                                                                |  |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  |  Weight: [65    ] lbs     Temperature: [101.5  ] °F                         |  |
|  |  Heart Rate: [90    ] bpm     Respiratory Rate: [22    ] /min               |  |
|  |                                                                             |  |
|  |  [Save Vitals]                                                              |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  VISIT NOTES                                                                |  |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  |  Physical examination completed. Patient is in excellent health.            |  |
|  |  All vitals within normal range. Teeth and gums healthy. Coat condition     |  |
|  |  good. No abnormalities detected.                                           |  |
|  |                                                                             |  |
|  |  Administered annual vaccinations. Owner advised on nutrition and exercise. |  |
|  |                                                                             |  |
|  |                                                                             |  |
|  |  [Voice to Text]  [Templates]                                [Auto-Save: On]|  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|  +-----------------------------------+  +------------------------------------+    |
|  |  SERVICES PROVIDED                |  |  DIAGNOSIS                         |    |
|  +-----------------------------------+  +------------------------------------+    |
|  |                                   |  |                                    |    |
|  |  [x] Physical Examination         |  |  Condition: [None - Healthy    v]  |    |
|  |  [x] Rabies Vaccination           |  |                                    |    |
|  |  [x] DHPP Vaccination             |  |  [+ Create New Diagnosis]          |    |
|  |  [ ] Blood Work                   |  |                                    |    |
|  |  [ ] Dental Cleaning              |  |                                    |    |
|  |  [ ] X-Ray                        |  |                                    |    |
|  |                                   |  |                                    |    |
|  |  [+ Add Service]                  |  |                                    |    |
|  +-----------------------------------+  +------------------------------------+    |
|                                                                                   |
|  +-----------------------------------+  +------------------------------------+    |
|  |  PRESCRIPTIONS                    |  |  FOLLOW-UP                         |    |
|  +-----------------------------------+  +------------------------------------+    |
|  |                                   |  |                                    |    |
|  |  No prescriptions                 |  |  [ ] Follow-up required            |    |
|  |                                   |  |                                    |    |
|  |  [+ Add Prescription]             |  |  Recommended date:                 |    |
|  +-----------------------------------+  |  [________________]                |    |
|                                         |                                    |    |
|                                         |  Notes:                            |    |
|                                         |  [____________________]            |    |
|                                         +------------------------------------+    |
|                                                                                   |
|  +-----------------------------------------------------------------------------+  |
|  |  BILLING                                                                    |  |
|  +-----------------------------------------------------------------------------+  |
|  |                                                                             |  |
|  |  Physical Examination ........................... $80.00                    |  |
|  |  Rabies Vaccination ............................. $25.00                    |  |
|  |  DHPP Vaccination ............................... $35.00                    |  |
|  |                                                                             |  |
|  |  Subtotal: $140.00                                                          |  |
|  |  Tax (8%): $11.20                                                           |  |
|  |  Total: $151.20                                                             |  |
|  |                                                                             |  |
|  +-----------------------------------------------------------------------------+  |
|                                                                                   |
|  [Save Draft]  [Print Summary]                   [Complete Appointment & Invoice]|
|                                                                                   |
+-----------------------------------------------------------------------------------+
```

---

## Navigation Flow Diagram

```
                    +------------------+
                    |   Home/Dashboard |
                    +------------------+
                            |
            +---------------+----------------+
            |               |                |
    +-------v-------+ +-----v------+ +-------v--------+
    | Appointments  | | Medical    | | Emergency      |
    | Dashboard     | | Records    | | (Quick Access) |
    +-------+-------+ +-----+------+ +-------+--------+
            |               |                |
    +-------v-------+       |        +-------v--------+
    | Schedule New  |       |        | Report         |
    | Appointment   |       |        | Emergency      |
    +-------+-------+       |        +-------+--------+
            |               |                |
    +-------v-------+       |        +-------v--------+
    | Select Pet    |       |        | Emergency      |
    +-------+-------+       |        | Submitted      |
            |               |        +----------------+
    +-------v-------+       |
    | Select Type   |       |
    +-------+-------+       |
            |               |
    +-------v-------+       |
    | Select Date/  |       |
    | Time/Vet      |       |
    +-------+-------+       |
            |               |
    +-------v-------+       |
    | Review &      |       |
    | Confirm       |       |
    +-------+-------+       |
            |               |
    +-------v-------+ +-----v----------+
    | Appointment   | | View Diagnosis |
    | Confirmed     | | Timeline       |
    +---------------+ +----------------+
```

---

## Responsive Breakpoints

### Mobile (< 768px)
- Single column layout
- Stacked components
- Bottom navigation
- Simplified filters (collapsed by default)
- Touch-optimized buttons (min 44px)
- Swipe gestures

### Tablet (768px - 1023px)
- Two column layout where appropriate
- Side drawer navigation
- Medium-sized touch targets
- Collapsible sidebars

### Desktop (1024px+)
- Multi-column layouts
- Persistent side navigation
- Hover states
- Keyboard shortcuts
- Dense information display

---

## Accessibility Notes

- All interactive elements have 44px minimum touch targets (mobile)
- Color is not the only indicator (icons + text)
- Keyboard navigation support
- Screen reader labels on all form fields
- Focus indicators visible
- High contrast mode support
- Alt text for images
- ARIA labels where needed
