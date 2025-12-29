# Fuel Economy Tracker - Wireframes

## Overview
This document describes the wireframe layouts for the Fuel Economy Tracker application's main screens.

## 1. Dashboard (Home Page)

### Layout Structure
```
+--------------------------------------------------+
|  [Logo] Fuel Economy Tracker      [Notifications]|
|  [Vehicle Selector ▼]              [Profile]     |
+--------------------------------------------------+
|                                                  |
|  +--------------------------------------------+  |
|  |          Current Average MPG               |  |
|  |                                            |  |
|  |              32.5 MPG                      |  |
|  |           ↑ +2.3 from last month           |  |
|  |                                            |  |
|  |  [View Details] [Add Fill-Up]              |  |
|  +--------------------------------------------+  |
|                                                  |
|  Quick Stats                                     |
|  +------------+ +------------+ +-------------+   |
|  | Last 7 Days| |Last 30 Days| |Personal Best|   |
|  |   31.2 MPG | |  30.8 MPG  | |   38.4 MPG  |   |
|  +------------+ +------------+ +-------------+   |
|                                                  |
|  MPG Trend (Last 30 Days)                        |
|  +--------------------------------------------+  |
|  |         Chart: Line graph showing          |  |
|  |         MPG variations over time           |  |
|  |         with EPA reference line            |  |
|  +--------------------------------------------+  |
|                                                  |
|  Recent Fill-Ups                                 |
|  +--------------------------------------------+  |
|  | Dec 28 | 12.5 gal | $45.00 | 33.2 MPG ✓  |  |
|  | Dec 21 | 11.8 gal | $42.00 | 32.8 MPG    |  |
|  | Dec 14 | 13.2 gal | $47.50 | 31.5 MPG    |  |
|  +--------------------------------------------+  |
|  [View All History]                              |
|                                                  |
|  Quick Actions                                   |
|  [➕ Add Fill-Up] [🎯 Set Goal] [📊 Reports]   |
+--------------------------------------------------+
|  [Home] [History] [Economy] [Budget] [More]     |
+--------------------------------------------------+
```

### Key Elements
- Vehicle selector dropdown (for multi-vehicle users)
- Large, prominent current average MPG display
- Trend indicator (up/down arrow with change amount)
- Quick stats cards for different time periods
- Interactive MPG trend chart
- Recent fill-ups list with key details
- Quick action buttons for common tasks
- Bottom navigation bar

## 2. Add Fuel Purchase Page

### Layout Structure
```
+--------------------------------------------------+
|  ← Add Fuel Purchase                    [Cancel]|
+--------------------------------------------------+
|                                                  |
|  Basic Information                               |
|  +--------------------------------------------+  |
|  | Date: [Dec 29, 2025 ▼]                     |  |
|  | Odometer Reading: [45,328 miles] *         |  |
|  | Gallons Purchased: [12.5] *                |  |
|  | Total Cost: [$48.75] *                     |  |
|  | Cost/Gallon: $3.90 (calculated)            |  |
|  +--------------------------------------------+  |
|                                                  |
|  Fuel Details                                    |
|  +--------------------------------------------+  |
|  | Fuel Grade: ○Regular ●Premium ○Diesel      |  |
|  | Fill Type:  ●Full Fill ○Partial Fill       |  |
|  +--------------------------------------------+  |
|                                                  |
|  Station Information                             |
|  +--------------------------------------------+  |
|  | Station: [Search or select...       🔍]   |  |
|  | Recent Stations:                           |  |
|  | • Shell - Main St                          |  |
|  | • Chevron - Oak Ave                        |  |
|  | [📍 Use Current Location]                  |  |
|  +--------------------------------------------+  |
|                                                  |
|  Payment & Notes                                 |
|  +--------------------------------------------+  |
|  | Payment: ○Cash ●Credit ○Debit ○App        |  |
|  | Notes: [Optional notes...]                 |  |
|  +--------------------------------------------+  |
|                                                  |
|  Calculated Results                              |
|  +--------------------------------------------+  |
|  | 🎯 Estimated MPG: 32.8                     |  |
|  | 📏 Miles since last fill: 410              |  |
|  | ⭐ Close to personal best! (+1.2 MPG)      |  |
|  +--------------------------------------------+  |
|                                                  |
|  [        Save Fill-Up        ]                  |
|  [       Add & Start New      ]                  |
|                                                  |
+--------------------------------------------------+
```

### Key Elements
- Clear form sections with visual grouping
- Auto-calculated cost per gallon
- Radio buttons for fuel grade and fill type
- Station search with recent stations quick-select
- GPS location integration
- Real-time MPG estimation
- Prominent save button
- Option to save and add another

## 3. Fuel History Page

### Layout Structure
```
+--------------------------------------------------+
|  ← Fuel Purchase History           [➕ Add New] |
+--------------------------------------------------+
|  [🔍 Search]  [📅 Filters]  [↕️ Sort]  [📤 Export]|
|                                                  |
|  Summary (This Month)                            |
|  +------------+ +------------+ +-------------+   |
|  | Avg MPG    | | Total Spent| | Fill-ups    |   |
|  |   32.5     | |  $195.00   | |     4       |   |
|  +------------+ +------------+ +-------------+   |
|                                                  |
|  Purchase List                                   |
|  +--------------------------------------------+  |
|  | December 28, 2025                 [⋮]       |  |
|  | Shell Station - Main Street                |  |
|  | 12.5 gal × $3.90 = $48.75                  |  |
|  | Odometer: 45,328 mi | MPG: 33.2 🟢         |  |
|  +--------------------------------------------+  |
|  +--------------------------------------------+  |
|  | December 21, 2025                 [⋮]       |  |
|  | Chevron - Oak Avenue                       |  |
|  | 11.8 gal × $3.56 = $42.00                  |  |
|  | Odometer: 44,918 mi | MPG: 32.8 🟢         |  |
|  +--------------------------------------------+  |
|  +--------------------------------------------+  |
|  | December 14, 2025      ⭐ Partial [⋮]      |  |
|  | BP Station - Highway 101                   |  |
|  | 8.2 gal × $3.75 = $30.75                   |  |
|  | Odometer: 44,625 mi | MPG: -- --           |  |
|  +--------------------------------------------+  |
|                                                  |
|  [Load More...]                                  |
|                                                  |
+--------------------------------------------------+
|  [Home] [History] [Economy] [Budget] [More]     |
+--------------------------------------------------+
```

### Key Elements
- Search, filter, sort, and export controls
- Monthly summary cards at top
- Each purchase card shows:
  - Date and station name
  - Volume, price, and total cost
  - Odometer reading and calculated MPG
  - Color-coded MPG badges (green=good, yellow=average, red=poor)
  - Menu for edit/delete actions
- Special badges (partial fill, personal best)
- Infinite scroll or pagination
- Quick add button in header

## 4. Economy Stats Page

### Layout Structure
```
+--------------------------------------------------+
|  ← Fuel Economy Statistics                      |
+--------------------------------------------------+
|                                                  |
|  +--------------------------------------------+  |
|  |       Your Current Average MPG             |  |
|  |                                            |  |
|  |              32.5 MPG                      |  |
|  |                                            |  |
|  |  [EPA: 30 MPG] You're 8.3% above EPA! ✨   |  |
|  +--------------------------------------------+  |
|                                                  |
|  Time Period: [Week|Month|3M|Year|All]           |
|                                                  |
|  MPG Trend Chart                                 |
|  +--------------------------------------------+  |
|  |  40 -                            *-- EPA    |  |
|  |  35 -      *---*---*                       |  |
|  |  30 -  *--/    \   *---*                   |  |
|  |  25 -                                      |  |
|  |     Dec  Jan  Feb  Mar  Apr  May           |  |
|  +--------------------------------------------+  |
|                                                  |
|  Statistics Breakdown                            |
|  +--------------------------------------------+  |
|  | Personal Best: 38.4 MPG (Mar 15, 2025)     |  |
|  | Lifetime Average: 31.8 MPG                 |  |
|  | Best Month: March 2025 (35.2 MPG)          |  |
|  | Improvement from Year Start: +12%          |  |
|  +--------------------------------------------+  |
|                                                  |
|  Current Goal                                    |
|  +--------------------------------------------+  |
|  | Target: 35 MPG by June 30, 2025            |  |
|  | Progress: [████████░░] 78%                 |  |
|  | On track to achieve goal! 🎯               |  |
|  | [View Goal Details]                        |  |
|  +--------------------------------------------+  |
|                                                  |
|  Insights & Recommendations                      |
|  +--------------------------------------------+  |
|  | 💡 Your MPG improves 15% on highway trips  |  |
|  | ⚠️ City driving averaging 5 MPG below EPA  |  |
|  | ⭐ Consistent improvement over 3 months    |  |
|  +--------------------------------------------+  |
|                                                  |
|  [Set New Goal] [View Detailed Analysis]         |
|                                                  |
+--------------------------------------------------+
|  [Home] [History] [Economy] [Budget] [More]     |
+--------------------------------------------------+
```

### Key Elements
- Large, prominent current average display
- Comparison to EPA rating with percentage
- Time period selector for chart
- Interactive trend chart with EPA reference
- Key statistics summary
- Active goal progress tracker
- AI-generated insights and recommendations
- Action buttons for goal setting and detailed analysis

## Design Principles

### Visual Hierarchy
1. Most important info (current MPG) is largest and most prominent
2. Supporting data in cards with clear labels
3. Actions clearly identified with buttons
4. Consistent spacing and alignment

### Color Coding
- Green: Good performance (above average/target)
- Yellow: Average performance (near target)
- Red: Poor performance (below average/target)
- Blue: Informational elements
- Gray: Supporting text and secondary elements

### Responsive Design
- Mobile-first approach
- Cards stack vertically on mobile
- Side-by-side layout on tablet/desktop
- Touch-friendly tap targets (44×44px minimum)
- Readable font sizes (16px body minimum)

### Accessibility
- High contrast ratios for text
- Clear focus indicators
- Semantic HTML structure
- ARIA labels for screen readers
- Keyboard navigation support
