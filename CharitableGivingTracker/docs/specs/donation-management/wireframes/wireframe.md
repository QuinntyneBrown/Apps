# Donation Management Wireframes

## 1. Donation Dashboard

```
+------------------------------------------------------------------+
|  CharitableGivingTracker                 [Search...] [@User ▼]  |
+------------------------------------------------------------------+
|  Dashboard | Donations | Organizations | Tax | Impact | Reports |
+------------------------------------------------------------------+
|                                                                   |
|  ⚠ Missing Acknowledgments (3)                         [View All]|
|  You have 3 donations over $250 that need acknowledgment letters |
|                                                                   |
|  Donation Management                       [+ Record Donation] ▼ |
|                                            [+ Setup Recurring]    |
|                                                                   |
|  +-------------+ +-------------+ +-------------+ +-------------+  |
|  | Total       | | This Year   | | Donations   | | Tax         |  |
|  | Donated     | |             | | Made        | | Deductible  |  |
|  |             | |             | |             | |             |  |
|  | $12,450     | | $3,200      | | 45          | | $11,800     |  |
|  | Lifetime    | | 2025        | | All Time    | | 2025        |  |
|  +-------------+ +-------------+ +-------------+ +-------------+  |
|                                                                   |
|  +-------------------------------------------------------------+  |
|  | Recent Donations                             [View All]     |  |
|  | ----------------------------------------------------------- |  |
|  | Jan 15 • Red Cross                   $500.00  ✓ Tax Ded.  |  |
|  |        • Healthcare • Check #1234              📄 Receipt  |  |
|  | ----------------------------------------------------------- |  |
|  | Jan 10 • Local Food Bank             $100.00  ✓ Tax Ded.  |  |
|  |        • Food Security • Credit Card           📄 Receipt  |  |
|  | ----------------------------------------------------------- |  |
|  | Jan 5  • Habitat for Humanity        $250.00  ✓ Tax Ded.  |  |
|  |        • Housing • Stock Donation      ⚠ Need Ack Letter  |  |
|  +-------------------------------------------------------------+  |
|                                                                   |
|  +-------------------------------------------------------------+  |
|  | Upcoming Recurring Donations                [Manage All]    |  |
|  | ----------------------------------------------------------- |  |
|  | Jan 20 • UNICEF                      $50.00   Monthly      |  |
|  | Feb 1  • Sierra Club                 $25.00   Monthly      |  |
|  | Feb 15 • Doctors Without Borders     $100.00  Quarterly    |  |
|  +-------------------------------------------------------------+  |
|                                                                   |
+------------------------------------------------------------------+
```

## 2. Record Donation Form

```
+------------------------------------------------------------------+
|  Record Donation                                           [✕]   |
+------------------------------------------------------------------+
|                                                                   |
|  * Required fields                                                |
|                                                                   |
|  Select Organization *                                            |
|  [Select a charity...                                       ▼]   |
|  [+ Add New Organization]                                        |
|                                                                   |
|  +----------------------------------------------------------+    |
|  | 🟢 Red Cross                                             |    |
|  | Verified 501(c)(3) • EIN: 53-0196605                    |    |
|  | Donations are tax-deductible                            |    |
|  +----------------------------------------------------------+    |
|                                                                   |
|  Donation Type *                                                  |
|  ( ) Cash Donation                                               |
|  (•) Check/Online Payment                                        |
|  ( ) Stock/Securities                                            |
|  ( ) Property/Goods                                              |
|                                                                   |
|  Amount *                         Donation Date *                |
|  [$500.00_]                       [01/15/2025] 📅                |
|                                                                   |
|  Payment Method *                 Check/Confirmation Number      |
|  [Check                     ▼]    [#1234_________________]       |
|                                                                   |
|  Category                         Tax Deductible                 |
|  [Healthcare               ▼]     [✓] Yes (verified charity)     |
|                                                                   |
|  Notes (Optional)                                                 |
|  [_____________________________________________________________] |
|  [_____________________________________________________________] |
|                                                                   |
|  Upload Receipt (Optional)                                        |
|  [Choose File] No file chosen                                    |
|  Accepted: PDF, JPG, PNG (Max 5MB)                               |
|                                                                   |
|  +----------------------------------------------------------+    |
|  | ⚠ Acknowledgment Letter Required                         |    |
|  | Donations $250 or more require a written acknowledgment  |    |
|  | from the charity to claim tax deduction.                |    |
|  | You can upload it later from donation details.          |    |
|  +----------------------------------------------------------+    |
|                                                                   |
|  +----------------------------------------------------------+    |
|  | Tax Deduction Summary                                    |    |
|  | Tax Year: 2025                                          |    |
|  | Deductible Amount: $500.00                              |    |
|  | Acknowledgment: Required (≥ $250)                       |    |
|  +----------------------------------------------------------+    |
|                                                                   |
|  +----------------------------------------------------------+    |
|  |  [Cancel]                          [Record Donation]         |
|  +----------------------------------------------------------+    |
|                                                                   |
+------------------------------------------------------------------+
```

## 3. Record Non-Cash Donation Form

```
+------------------------------------------------------------------+
|  Record Non-Cash Donation                                  [✕]   |
+------------------------------------------------------------------+
|                                                                   |
|  * Required fields                                                |
|                                                                   |
|  Select Organization *                                            |
|  [Goodwill Industries                                       ▼]   |
|                                                                   |
|  Item Description *                                               |
|  [Clothing and household items_______________________________]   |
|                                                                   |
|  Quantity *                       Fair Market Value *             |
|  [15] items                       [$450.00_]                      |
|                                                                   |
|  Valuation Method *                                               |
|  [Thrift Store Donation Guide                               ▼]   |
|  • Thrift Store Donation Guide                                   |
|  • Online Marketplace Comparison                                 |
|  • Professional Appraisal                                        |
|  • Other                                                         |
|                                                                   |
|  Donation Date *                  Category                       |
|  [01/10/2025] 📅                  [General Charity          ▼]   |
|                                                                   |
|  +----------------------------------------------------------+    |
|  | ℹ️ Appraisal Information                                  |    |
|  | For non-cash donations over $500, you must file Form    |    |
|  | 8283 with your tax return.                              |    |
|  |                                                         |    |
|  | For non-cash donations over $5,000, you must obtain    |    |
|  | a qualified appraisal.                                  |    |
|  |                                                         |    |
|  | Your donation: $450.00 - Form 8283 required            |    |
|  +----------------------------------------------------------+    |
|                                                                   |
|  Upload Itemized List (Recommended)                               |
|  [Choose File] clothing_list.pdf                  [✕]            |
|                                                                   |
|  Notes                                                            |
|  [_____________________________________________________________] |
|                                                                   |
|  +----------------------------------------------------------+    |
|  |  [Cancel]                    [Record Non-Cash Donation]      |
|  +----------------------------------------------------------+    |
|                                                                   |
+------------------------------------------------------------------+
```

## 4. Setup Recurring Donation Form

```
+------------------------------------------------------------------+
|  Setup Recurring Donation                                  [✕]   |
+------------------------------------------------------------------+
|                                                                   |
|  * Required fields                                                |
|                                                                   |
|  Select Organization *                                            |
|  [UNICEF                                                    ▼]   |
|                                                                   |
|  +----------------------------------------------------------+    |
|  | 🟢 UNICEF                                                |    |
|  | Verified 501(c)(3) • Rating: 4/4 Stars (Charity Navigator)|  |
|  | Your recurring donation: $50/month = $600/year          |    |
|  +----------------------------------------------------------+    |
|                                                                   |
|  Donation Amount *                Frequency *                    |
|  [$50.00_]                        [Monthly                  ▼]   |
|                                   • Weekly                       |
|                                   • Bi-Weekly                    |
|                                   • Monthly                      |
|                                   • Quarterly                    |
|                                   • Annually                     |
|                                                                   |
|  Start Date *                     End Date (Optional)            |
|  [01/20/2025] 📅                  [____________] 📅               |
|                                                                   |
|  Payment Method *                                                 |
|  [Credit Card (...1234)                                     ▼]   |
|  [+ Add New Payment Method]                                      |
|                                                                   |
|  Category                                                         |
|  [International Relief                                      ▼]   |
|                                                                   |
|  +----------------------------------------------------------+    |
|  | 📅 Upcoming Donations Preview                            |    |
|  | -------------------------------------------------------- |    |
|  | January 20, 2025     $50.00                             |    |
|  | February 20, 2025    $50.00                             |    |
|  | March 20, 2025       $50.00                             |    |
|  | ...and continuing monthly                               |    |
|  |                                                         |    |
|  | Projected Annual Total: $600.00                         |    |
|  +----------------------------------------------------------+    |
|                                                                   |
|  Notes (Optional)                                                 |
|  [_____________________________________________________________] |
|                                                                   |
|  [✓] Send me a reminder 3 days before each donation              |
|  [✓] Email me a receipt after each donation                      |
|                                                                   |
|  +----------------------------------------------------------+    |
|  |  [Cancel]                    [Schedule Recurring Donation]   |
|  +----------------------------------------------------------+    |
|                                                                   |
+------------------------------------------------------------------+
```

## 5. Donation History Page

```
+------------------------------------------------------------------+
|  CharitableGivingTracker                 [Search...] [@User ▼]  |
+------------------------------------------------------------------+
|  Dashboard | Donations | Organizations | Tax | Impact | Reports |
+------------------------------------------------------------------+
|                                                                   |
|  Donation History                         [+ Record] [Export ▼]  |
|                                                                   |
|  +---------------------+  +----------------------------------+   |
|  | Filters             |  | Search donations...        [🔍] |   |
|  | ------------------- |  +----------------------------------+   |
|  | Date Range          |                                         |
|  | [01/01/2025] to     |  Sort by: [Date (Newest) ▼]             |
|  | [12/31/2025]        |                                         |
|  |                     |  +----------------------------------+   |
|  | Organization        |  | Jan 15, 2025                     |   |
|  | [All Charities  ▼]  |  | Red Cross                  $500.00|   |
|  |                     |  | Healthcare • Check #1234         |   |
|  | Payment Method      |  | ✓ Tax Deductible  ⚠ Need Ack    |   |
|  | [All Methods    ▼]  |  | [View] [Download Receipt]        |   |
|  |                     |  +----------------------------------+   |
|  | Category            |                                         |
|  | [All Categories ▼]  |  +----------------------------------+   |
|  |                     |  | Jan 10, 2025                     |   |
|  | Status              |  | Local Food Bank            $100.00|   |
|  | [✓] Tax Deductible  |  | Food Security • Credit Card      |   |
|  | [✓] With Receipt    |  | ✓ Tax Deductible  ✓ Receipt      |   |
|  | [✓] Acknowledged    |  | [View] [Download Receipt]        |   |
|  | [ ] Refunded        |  +----------------------------------+   |
|  |                     |                                         |
|  | [Apply Filters]     |  +----------------------------------+   |
|  | [Clear All]         |  | Jan 5, 2025                      |   |
|  +---------------------+  | Habitat for Humanity       $250.00|   |
|                           | Housing • Stock                  |   |
|                           | ✓ Tax Deductible  ✗ No Ack       |   |
|                           | [View] [Upload Acknowledgment]   |   |
|                           +----------------------------------+   |
|                                                                   |
|  Showing 1-10 of 45 donations    < 1 2 3 4 5 >    Total: $3,200 |
|                                                                   |
+------------------------------------------------------------------+
```

## 6. Donation Details Page

```
+------------------------------------------------------------------+
|  CharitableGivingTracker                 [Search...] [@User ▼]  |
+------------------------------------------------------------------+
|  Dashboard | Donations | Organizations | Tax | Impact | Reports |
+------------------------------------------------------------------+
|  < Back to Donations                                              |
|                                                                   |
|  Donation to Red Cross                    [Edit] [Request Refund]|
|                                                                   |
|  +-------------------------------------------------------------+  |
|  | $500.00                                       Jan 15, 2025  |  |
|  | ✓ Tax Deductible  ⚠ Acknowledgment Required              |  |
|  +-------------------------------------------------------------+  |
|                                                                   |
|  +---------------------------+ +-----------------------------+   |
|  | Organization Details      | | Donation Details            |   |
|  | ------------------------- | | --------------------------- |   |
|  | Name: Red Cross          | | Type: Cash Donation         |   |
|  | EIN: 53-0196605          | | Amount: $500.00             |   |
|  | Status: ✓ Verified       | | Date: January 15, 2025      |   |
|  | 501(c)(3)                | | Payment: Check #1234        |   |
|  |                          | | Category: Healthcare        |   |
|  | [View Organization]       | |                            |   |
|  +---------------------------+ +-----------------------------+   |
|                                                                   |
|  +-------------------------------------------------------------+  |
|  | Tax Information                                             |  |
|  | ----------------------------------------------------------- |  |
|  | Tax Year: 2025                                             |  |
|  | Tax Deductible: Yes                                        |  |
|  | Deductible Amount: $500.00                                 |  |
|  | Acknowledgment Status: Required (≥ $250)                   |  |
|  | IRS Form: Include in Schedule A                            |  |
|  +-------------------------------------------------------------+  |
|                                                                   |
|  +-------------------------------------------------------------+  |
|  | Documents                                                   |  |
|  | ----------------------------------------------------------- |  |
|  | Receipt:                                                   |  |
|  | 📄 receipt_20250115.pdf (uploaded Jan 15)  [View] [Download]|  |
|  |                                                            |  |
|  | Acknowledgment Letter:                                     |  |
|  | ⚠ Not yet received                                         |  |
|  | [Upload Acknowledgment Letter]                             |  |
|  |                                                            |  |
|  | ℹ️ IRS requires written acknowledgment for donations ≥ $250|  |
|  +-------------------------------------------------------------+  |
|                                                                   |
|  Notes                                                            |
|  Annual donation to support disaster relief efforts               |
|                                                                   |
|  +-------------------------------------------------------------+  |
|  | Activity Timeline                                           |  |
|  | ----------------------------------------------------------- |  |
|  | • Donation recorded - Jan 15, 2025 10:30 AM                |  |
|  | • Receipt uploaded - Jan 15, 2025 10:31 AM                 |  |
|  | • Reminder sent - Jan 22, 2025 (acknowledgment needed)     |  |
|  +-------------------------------------------------------------+  |
|                                                                   |
+------------------------------------------------------------------+
```

## 7. Recurring Donations Management

```
+------------------------------------------------------------------+
|  CharitableGivingTracker                 [Search...] [@User ▼]  |
+------------------------------------------------------------------+
|  Dashboard | Donations | Organizations | Tax | Impact | Reports |
+------------------------------------------------------------------+
|                                                                   |
|  Recurring Donations                      [+ Setup Recurring]    |
|                                                                   |
|  +-------------------------------------------------------------+  |
|  | Active Recurring Donations (3)                              |  |
|  | ----------------------------------------------------------- |  |
|  | UNICEF                                                      |  |
|  | $50.00 / Monthly                          Next: Jan 20, 2025|  |
|  | International Relief                                        |  |
|  | Started: Jan 20, 2024 • 12 donations • Total: $600         |  |
|  | [Modify] [Pause] [View History] [Cancel]                   |  |
|  | ----------------------------------------------------------- |  |
|  | Sierra Club                                                 |  |
|  | $25.00 / Monthly                          Next: Feb 1, 2025 |  |
|  | Environment                                                 |  |
|  | Started: Feb 1, 2024 • 12 donations • Total: $300          |  |
|  | [Modify] [Pause] [View History] [Cancel]                   |  |
|  | ----------------------------------------------------------- |  |
|  | Doctors Without Borders                                     |  |
|  | $100.00 / Quarterly                      Next: Feb 15, 2025 |  |
|  | Healthcare                                                  |  |
|  | Started: Feb 15, 2024 • 4 donations • Total: $400          |  |
|  | [Modify] [Pause] [View History] [Cancel]                   |  |
|  +-------------------------------------------------------------+  |
|                                                                   |
|  +-------------------------------------------------------------+  |
|  | Paused Recurring Donations (1)                    [Show ▼] |  |
|  +-------------------------------------------------------------+  |
|                                                                   |
|  +-------------------------------------------------------------+  |
|  | Summary                                                     |  |
|  | Total Active: 3 recurring donations                        |  |
|  | Projected Annual Total: $1,300                             |  |
|  | Next Donation: Jan 20, 2025 ($50 to UNICEF)                |  |
|  +-------------------------------------------------------------+  |
|                                                                   |
+------------------------------------------------------------------+
```

## Design Notes

### Color Scheme
- Tax Deductible: Green (#10B981)
- Missing Acknowledgment: Amber (#F59E0B)
- Refunded: Red (#EF4444)
- Verified Charity: Green (#10B981)
- Recurring: Blue (#3B82F6)

### Icons
- ✓ Completed/Verified
- ⚠ Warning/Required
- ✗ Missing/Not received
- 📄 Document/Receipt
- 📅 Calendar/Date
- 🟢 Active/Verified

### Interactive Elements
- Hover effects on donation cards
- Expandable detail sections
- Drag-and-drop file uploads
- Auto-save for forms
- Real-time validation feedback
