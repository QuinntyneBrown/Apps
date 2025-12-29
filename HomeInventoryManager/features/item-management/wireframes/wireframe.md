# Item Management Wireframes

## 1. Items Dashboard

```
+------------------------------------------------------------------+
|  Home Inventory Manager                    [+ Add Item] [Export] |
+------------------------------------------------------------------+
|                                                                   |
|  +---------------+ +---------------+ +---------------+           |
|  | Total Items   | | Total Value   | | Categories    |           |
|  |     247       | |   $45,320     | |      18       |           |
|  +---------------+ +---------------+ +---------------+           |
|                                                                   |
|  Search: [________________]  [🔍]                                |
|                                                                   |
|  Filters: [Category ▼] [Location ▼] [Status ▼] [Value Range]   |
|  Sort by: [Name ▼]                        [Grid View] [List View]|
|                                                                   |
|  +-------------+  +-------------+  +-------------+  +----------+ |
|  | [Photo]     |  | [Photo]     |  | [Photo]     |  | [Photo]  | |
|  |             |  |             |  |             |  |          | |
|  | TV 65"      |  | Laptop      |  | Camera      |  | Sofa     | |
|  | Electronics |  | Electronics |  | Electronics |  | Furniture| |
|  | $1,200      |  | $1,800      |  | $950        |  | $2,400   | |
|  | Living Room |  | Office      |  | Office      |  | Living   | |
|  | [Edit] [...] |  | [Edit] [...] |  | [Edit] [...] |  |[Edit][.]| |
|  +-------------+  +-------------+  +-------------+  +----------+ |
|                                                                   |
|  +-------------+  +-------------+  +-------------+  +----------+ |
|  | [Photo]     |  | [Photo]     |  | [Photo]     |  | [Photo]  | |
|  | Refrigerator|  | Washer      |  | Bike        |  | Drill    | |
|  | Appliance   |  | Appliance   |  | Sports      |  | Tools    | |
|  | $2,100      |  | $850        |  | $420        |  | $180     | |
|  +-------------+  +-------------+  +-------------+  +----------+ |
|                                                                   |
|  [Load More Items]                                 Page 1 of 12  |
+------------------------------------------------------------------+
```

## 2. Add Item Form

```
+------------------------------------------------------------------+
|  Add New Item                                              [✕]   |
+------------------------------------------------------------------+
|                                                                   |
|  Item Information                                                 |
|  +------------------------------------------------------------+  |
|  | Item Name *                                                |  |
|  | [________________________________________________]         |  |
|  |                                                            |  |
|  | Description                                                |  |
|  | [________________________________________________]         |  |
|  | [________________________________________________]         |  |
|  | [________________________________________________]         |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Category & Location                                              |
|  Category *         [Electronics > TV & Audio            ▼]      |
|  Location *         [Living Room > TV Stand              ▼]      |
|                                                                   |
|  Purchase Information                                             |
|  Purchase Date      [01/15/2024 ▼]   Purchase Price  [$1,200.00]|
|  Retailer           [Best Buy_________________________]          |
|                                                                   |
|  Current Information                                              |
|  Current Value *    [$1,200.00]      (Auto-filled from purchase) |
|  Condition *        (•) Excellent  ( ) Good  ( ) Fair  ( ) Poor  |
|  Serial Number      [SN123456789__________________________]      |
|                                                                   |
|  Documentation                                                    |
|  +------------------------------------------------------------+  |
|  | Photos (0/10)                                              |  |
|  |                                                            |  |
|  | +--------------------------------------------------+       |  |
|  | |  Drag & drop photos here or click to upload     |       |  |
|  | |  Max 10 photos, 5MB each                         |       |  |
|  | +--------------------------------------------------+       |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Receipt            [Upload Receipt]  [No receipt uploaded]      |
|                                                                   |
|  Notes                                                            |
|  [_____________________________________________________________]  |
|  [_____________________________________________________________]  |
|                                                                   |
|  [Cancel]                                       [Save Item]       |
|                                                                   |
+------------------------------------------------------------------+
```

## 3. Item Details View

```
+------------------------------------------------------------------+
|  Item Details                           [Edit] [Move] [Delete]   |
+------------------------------------------------------------------+
|                                                                   |
|  +----------------------+  +----------------------------------+   |
|  |                      |  | Sony 65" 4K Smart TV             |   |
|  |   [Primary Photo]    |  | Category: Electronics > TV       |   |
|  |                      |  | Location: Living Room > TV Stand |   |
|  |       Large          |  |                                  |   |
|  |       Image          |  | Current Value: $1,200.00         |   |
|  |                      |  | Condition: Excellent             |   |
|  |                      |  | Status: Active                   |   |
|  +----------------------+  |                                  |   |
|  [Thumb] [Thumb] [Thumb]  +----------------------------------+   |
|   Photo   Photo   Photo                                          |
|                                                                   |
|  Purchase Information                                             |
|  +------------------------------------------------------------+  |
|  | Purchased: Jan 15, 2024                                    |  |
|  | Purchase Price: $1,200.00                                  |  |
|  | Retailer: Best Buy                                         |  |
|  | Age: 11 months                                             |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Documentation                                                    |
|  Serial Number: SN123456789ABCD                                  |
|  Receipt: [View Receipt PDF] ✓ Uploaded                         |
|                                                                   |
|  Notes                                                            |
|  Extended warranty purchased until Jan 2027.                     |
|  Includes wall mount and HDMI cables.                            |
|                                                                   |
|  Activity History                                                 |
|  +------------------------------------------------------------+  |
|  | • Item added - Jan 15, 2024                                |  |
|  | • Photo uploaded - Jan 15, 2024                            |  |
|  | • Receipt attached - Jan 15, 2024                          |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  [Print Item Details]                     [Export to PDF]         |
|                                                                   |
+------------------------------------------------------------------+
```

## 4. Bulk Import

```
+------------------------------------------------------------------+
|  Bulk Import Items                                               |
+------------------------------------------------------------------+
|                                                                   |
|  Step 1: Download Template                                        |
|  +------------------------------------------------------------+  |
|  | Download our CSV or Excel template to ensure proper        |  |
|  | formatting of your import data.                            |  |
|  |                                                            |  |
|  | [Download CSV Template]  [Download Excel Template]        |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Step 2: Upload File                                              |
|  +------------------------------------------------------------+  |
|  | +------------------------------------------------------+   |  |
|  | |  Drag & drop your file here or click to upload      |   |  |
|  | |  Supported formats: CSV, XLSX                        |   |  |
|  | +------------------------------------------------------+   |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Step 3: Review & Import                                          |
|  +------------------------------------------------------------+  |
|  | Preview (Showing first 5 rows)                             |  |
|  +------------------------------------------------------------+  |
|  | Name          | Category    | Location    | Value  | Status |  |
|  |------------------------------------------------------------| |
|  | Coffee Maker  | Kitchen     | Counter     | $120   | ✓     |  |
|  | Toaster       | Kitchen     | Counter     | $45    | ✓     |  |
|  | Blender       | Kitchen     | Cabinet     | $89    | ✓     |  |
|  | Plates Set    | Kitchen     | Cabinet     | $67    | ⚠     |  |
|  | Wine Glasses  | Kitchen     | Shelf       | $55    | ✓     |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Import Summary:                                                  |
|  Total Rows: 47  |  Valid: 45  |  Errors: 2                      |
|                                                                   |
|  Errors:                                                          |
|  • Row 4: Location "Cabinet 1" not found                         |
|  • Row 23: Category required but missing                         |
|                                                                   |
|  [Cancel]  [Fix Errors]                      [Import 45 Items]   |
|                                                                   |
+------------------------------------------------------------------+
```
