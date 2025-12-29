# Income Stream Management Wireframes

## 1. Income Streams Dashboard

```
+------------------------------------------------------------------+
|  SideHustleIncomeTracker                 [Search...] [@User ▼]  |
+------------------------------------------------------------------+
|  Dashboard | Income Streams | Clients | Expenses | Reports | ⚙ |
+------------------------------------------------------------------+
|                                                                   |
|  Income Streams                                  [+ Add Stream]  |
|                                                                   |
|  +-------------+ +-------------+ +-------------+ +-------------+ |
|  | Active      | | Total       | | Highest     | | This Month  | |
|  | Streams     | | Revenue     | | Earning     | | Revenue     | |
|  |     5       | |  $8,450/mo  | |  $3,200/mo  | |   $9,100    | |
|  +-------------+ +-------------+ +-------------+ +-------------+ |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | [All ▼] [Business Type ▼] [Sort: Revenue ▼]    [⊞][≡]     | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | 📝 Freelance Writing                         [Active]   ⚙  | |
|  |     Content Creation • Started Jan 2024                    | |
|  |     Expected: $3,000/mo | Actual: $3,200/mo (+6.7%)        | |
|  |     Total Revenue: $38,400 | 12 months active               | |
|  |     [View Details] [Close Stream]                          | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | 💻 Web Design Consulting                     [Active]   ⚙  | |
|  |     Freelancing • Started Mar 2024                         | |
|  |     Expected: $2,000/mo | Actual: $1,850/mo (-7.5%)        | |
|  |     Total Revenue: $18,500 | 10 months active               | |
|  |     [View Details] [Close Stream]                          | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | 🎨 Etsy Shop - Handmade Crafts            [Reactivated] ⚙  | |
|  |     Crafts • Started Jun 2023 • Reactivated Nov 2024       | |
|  |     Expected: $500/mo | Actual: $645/mo (+29%)             | |
|  |     Total Revenue: $7,825 | 14 months active               | |
|  |     [View Details] [Close Stream]                          | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | 📚 Online Tutoring                           [Active]   ⚙  | |
|  |     Tutoring • Started Sep 2024                            | |
|  |     Expected: $1,200/mo | Actual: $1,350/mo (+12.5%)       | |
|  |     Total Revenue: $5,400 | 4 months active                | |
|  |     [View Details] [Close Stream]                          | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | 🏠 Airbnb Rental                             [Active]   ⚙  | |
|  |     Rental • Started Feb 2024                              | |
|  |     Expected: $1,800/mo | Actual: $1,905/mo (+5.8%)        | |
|  |     Total Revenue: $20,955 | 11 months active              | |
|  |     [View Details] [Close Stream]                          | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  [View Closed Streams (3)] [Export to CSV]                      |
|                                                                   |
+------------------------------------------------------------------+
```

## 2. Add New Income Stream Form

```
+------------------------------------------------------------------+
|  Add New Income Stream                                     [✕]   |
+------------------------------------------------------------------+
|                                                                   |
|  Stream Name *                                                   |
|  +------------------------------------------------------------+  |
|  | Freelance Photography                                      |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Business Type *                                                 |
|  +------------------------------------------------------------+  |
|  | [Services                                              ▼]  |  |
|  +------------------------------------------------------------+  |
|  • Freelancing    • Consulting      • E-Commerce                |
|  • Content Creation • Tutoring      • Crafts & Handmade         |
|  • Services       • Rental Income   • Other                     |
|                                                                   |
|  Description                                                     |
|  +------------------------------------------------------------+  |
|  | Wedding and event photography services for local clients. |  |
|  | Includes engagement shoots, day-of coverage, and editing. |  |
|  |                                                            |  |
|  +------------------------------------------------------------+  |
|  0 / 1000 characters                                            |
|                                                                   |
|  Start Date *                                                    |
|  +------------------------------------------------------------+  |
|  | [01/15/2025                                          📅]  |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Expected Monthly Revenue                                        |
|  +------------------------------------------------------------+  |
|  | $ 2,500.00                                                 |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Tags (optional)                                                 |
|  +------------------------------------------------------------+  |
|  | [photography] [creative] [weekends]                    +  |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  +------------------------------------------------------------+  |
|  |                                                            |  |
|  |                  [Cancel]  [Create Stream]                |  |
|  |                                                            |  |
|  +------------------------------------------------------------+  |
|                                                                   |
+------------------------------------------------------------------+
```

## 3. Income Stream Details View

```
+------------------------------------------------------------------+
|  SideHustleIncomeTracker                 [Search...] [@User ▼]  |
+------------------------------------------------------------------+
|  Dashboard | Income Streams | Clients | Expenses | Reports | ⚙ |
+------------------------------------------------------------------+
|  < Back to Income Streams                                        |
|                                                                   |
|  📝 Freelance Writing                             [Active]       |
|  Content Creation • Started January 2024                         |
|                                                                   |
|  [Edit Details] [Close Stream] [Generate Report] [⚙ More]       |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | [Overview] [Performance] [Clients] [Activity]              | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  Overview                                                        |
|                                                                   |
|  +---------------+ +---------------+ +---------------+           |
|  | Expected Rev  | | Actual Rev    | | Difference    |           |
|  | $3,000/mo     | | $3,200/mo     | | +$200 (6.7%)  |           |
|  +---------------+ +---------------+ +---------------+           |
|                                                                   |
|  +---------------+ +---------------+ +---------------+           |
|  | Total Revenue | | Total Expenses| | Net Profit    |           |
|  | $38,400       | | $2,150        | | $36,250       |           |
|  +---------------+ +---------------+ +---------------+           |
|                                                                   |
|  +---------------+ +---------------+ +---------------+           |
|  | Active Months | | Active Clients| | Invoices Paid |           |
|  | 12            | | 8             | | 47 / 50       |           |
|  +---------------+ +---------------+ +---------------+           |
|                                                                   |
|  Stream Information                                              |
|  +-------------------------------------------------------------+ |
|  | Start Date:        January 15, 2024                        | |
|  | Business Type:     Content Creation                        | |
|  | Status:            Active                                  | |
|  | Tags:              writing, content, remote                | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  Description                                                     |
|  +-------------------------------------------------------------+ |
|  | Freelance content writing for tech companies and SaaS     | |
|  | startups. Includes blog posts, white papers, case studies,| |
|  | and technical documentation.                              | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  Recent Activity                                                 |
|  +-------------------------------------------------------------+ |
|  | Dec 28 • Payment received: $850 from TechCorp            💰| |
|  | Dec 26 • Invoice sent: $1,200 to StartupXYZ              📧| |
|  | Dec 20 • Expense recorded: $45 - Software subscription   💸| |
|  | Dec 15 • Payment received: $650 from BlogCo              💰| |
|  | Dec 10 • Milestone: $38,000 total revenue!               🎉| |
|  +-------------------------------------------------------------+ |
|                                                                   |
+------------------------------------------------------------------+
```

## 4. Close Income Stream Modal

```
+------------------------------------------------------------------+
|  Close Income Stream                                       [✕]   |
+------------------------------------------------------------------+
|                                                                   |
|  ⚠️  You are about to close "Freelance Writing"                 |
|                                                                   |
|  This will mark the income stream as inactive and remove it      |
|  from your active streams list. All historical data will be      |
|  preserved and accessible in Stream History.                     |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | Stream Performance Summary                                 | |
|  | ---------------------------------------------------------- | |
|  | Total Revenue Earned:        $38,400.00                   | |
|  | Total Expenses:              $2,150.00                    | |
|  | Net Profit:                  $36,250.00                   | |
|  | Duration Active:             12 months                    | |
|  | Average Monthly Revenue:     $3,200.00                    | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  End Date *                                                      |
|  +------------------------------------------------------------+  |
|  | [12/31/2024                                          📅]  |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Reason for Closing (optional)                                   |
|  +------------------------------------------------------------+  |
|  | Transitioning to full-time employment in this field.      |  |
|  | No longer need freelance income.                          |  |
|  |                                                            |  |
|  +------------------------------------------------------------+  |
|  0 / 500 characters                                             |
|                                                                   |
|  ☐ I may want to reactivate this stream in the future           |
|                                                                   |
|  +------------------------------------------------------------+  |
|  |                                                            |  |
|  |                    [Cancel]  [Close Stream]                |  |
|  |                                                            |  |
|  +------------------------------------------------------------+  |
|                                                                   |
+------------------------------------------------------------------+
```

## 5. Income Stream History View

```
+------------------------------------------------------------------+
|  SideHustleIncomeTracker                 [Search...] [@User ▼]  |
+------------------------------------------------------------------+
|  Dashboard | Income Streams | Clients | Expenses | Reports | ⚙ |
+------------------------------------------------------------------+
|                                                                   |
|  < Back to Active Income Streams                                 |
|                                                                   |
|  Income Stream History                                           |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | Filter: [All Time ▼] [Business Type ▼]         [Export ▼] | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  Showing 3 closed income streams                                 |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | 🛍️ Amazon FBA Store                          [Closed]     | |
|  |     E-Commerce                                             | |
|  |     Active: Mar 2023 - Aug 2024 (17 months)                | |
|  |     Total Revenue: $24,500 | Net Profit: $8,750           | |
|  |     Closure Reason: Too time-intensive, low margins        | |
|  |     [View Details] [Reactivate Stream]                     | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | 🎓 SAT Prep Tutoring                         [Closed]     | |
|  |     Tutoring                                               | |
|  |     Active: Aug 2022 - May 2024 (21 months)                | |
|  |     Total Revenue: $18,900 | Net Profit: $17,500          | |
|  |     Closure Reason: Seasonal demand ended                  | |
|  |     [View Details] [Reactivate Stream]                     | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | 📱 Mobile App Consulting                     [Closed]     | |
|  |     Consulting                                             | |
|  |     Active: Jan 2023 - Jun 2023 (6 months)                 | |
|  |     Total Revenue: $15,600 | Net Profit: $14,200          | |
|  |     Closure Reason: Client project completed               | |
|  |     [View Details] [Reactivate Stream]                     | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | Total Closed Streams: 3                                    | |
|  | Total Historical Revenue: $59,000                          | |
|  | Total Historical Profit: $40,450                           | |
|  +-------------------------------------------------------------+ |
|                                                                   |
+------------------------------------------------------------------+
```

## 6. Reactivate Stream Confirmation

```
+------------------------------------------------------------------+
|  Reactivate Income Stream                                  [✕]   |
+------------------------------------------------------------------+
|                                                                   |
|  🔄 Reactivate "Etsy Shop - Handmade Crafts"?                   |
|                                                                   |
|  This will return the income stream to your active list and      |
|  allow you to continue tracking revenue and expenses.            |
|                                                                   |
|  +-------------------------------------------------------------+ |
|  | Previous Performance                                       | |
|  | ---------------------------------------------------------- | |
|  | Previously Active: Jun 2023 - Sep 2024 (15 months)        | |
|  | Revenue During Period: $7,180                             | |
|  | Average Monthly Revenue: $479                             | |
|  | Closed On: September 15, 2024                             | |
|  +-------------------------------------------------------------+ |
|                                                                   |
|  Reactivation Date                                               |
|  +------------------------------------------------------------+  |
|  | [12/29/2024                                          📅]  |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Update Expected Monthly Revenue?                                |
|  +------------------------------------------------------------+  |
|  | $ 645.00                                                   |  |
|  +------------------------------------------------------------+  |
|  💡 Based on previous average: $479/month                       |
|                                                                   |
|  +------------------------------------------------------------+  |
|  |                                                            |  |
|  |                  [Cancel]  [Reactivate Stream]             |  |
|  |                                                            |  |
|  +------------------------------------------------------------+  |
|                                                                   |
+------------------------------------------------------------------+
```

## 7. Edit Income Stream Form

```
+------------------------------------------------------------------+
|  Edit Income Stream                                        [✕]   |
+------------------------------------------------------------------+
|                                                                   |
|  Stream Name *                                                   |
|  +------------------------------------------------------------+  |
|  | Freelance Writing & Content Strategy                       |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Business Type *                                                 |
|  +------------------------------------------------------------+  |
|  | [Content Creation                                      ▼]  |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Description                                                     |
|  +------------------------------------------------------------+  |
|  | Freelance content writing and content strategy consulting |  |
|  | for tech companies and SaaS startups. Includes blog posts,|  |
|  | white papers, case studies, technical documentation, and  |  |
|  | content calendar planning.                                |  |
|  +------------------------------------------------------------+  |
|  248 / 1000 characters                                          |
|                                                                   |
|  Expected Monthly Revenue                                        |
|  +------------------------------------------------------------+  |
|  | $ 3,500.00                                                 |  |
|  +------------------------------------------------------------+  |
|  💡 Current actual: $3,200/mo                                   |
|                                                                   |
|  Tags                                                            |
|  +------------------------------------------------------------+  |
|  | [writing] [content] [remote] [strategy] [saas]         +  |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Start Date: January 15, 2024 (cannot be changed)               |
|  Status: Active (use Close or Reactivate to change)             |
|                                                                   |
|  +------------------------------------------------------------+  |
|  |                                                            |  |
|  |                    [Cancel]  [Save Changes]                |  |
|  |                                                            |  |
|  +------------------------------------------------------------+  |
|                                                                   |
+------------------------------------------------------------------+
```
