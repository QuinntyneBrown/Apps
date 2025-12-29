# Wine Collection Wireframes

## 1. Collection Dashboard

```
+------------------------------------------------------------------+
|  Wine Cellar Inventory                      [+ Add Wine]         |
+------------------------------------------------------------------+
|                                                                   |
|  +---------------+ +---------------+ +---------------+           |
|  | Total Bottles | | Total Value   | | Ready to Drink|           |
|  |     237       | |   $45,680     | |      12       |           |
|  +---------------+ +---------------+ +---------------+           |
|                                                                   |
|  Filter: [Region ▼] [Vintage ▼] [Status ▼]  View: [Grid] [List] |
|                                                                   |
|  +-------------+  +-------------+  +-------------+  +----------+ |
|  | [Label Img] |  | [Label Img] |  | [Label Img] |  |[Label]   | |
|  |             |  |             |  |             |  |          | |
|  | Château     |  | Opus One    |  | Screaming   |  | Ridge    | |
|  | Margaux     |  | 2018        |  | Eagle 2019  |  | Monte    | |
|  | 2015        |  | Napa Valley |  | Napa Valley |  | Bello    | |
|  | Bordeaux    |  | $350        |  | $125        |  | $95      | |
|  | $280        |  | 🟢 Ready    |  | 🟡 2026-30  |  | 🔴 Drink | |
|  | 🟡 2025-35  |  | 6 bottles   |  | 3 bottles   |  | Now!     | |
|  +-------------+  +-------------+  +-------------+  +----------+ |
|                                                                   |
|  +-------------+  +-------------+  +-------------+  +----------+ |
|  | [Label Img] |  | [Label Img] |  | [Label Img] |  |[Label]   | |
|  | Domaine de  |  | Sassicaia   |  | Silver Oak  |  | Caymus   | |
|  +-------------+  +-------------+  +-------------+  +----------+ |
|                                                                   |
+------------------------------------------------------------------+
```

## 2. Add Wine Form

```
+------------------------------------------------------------------+
|  Add Wine to Collection                                    [✕]   |
+------------------------------------------------------------------+
|                                                                   |
|  Wine Information                                                 |
|  Wine Name *         [Château Margaux_______________________]    |
|  Producer *          [Château Margaux_______________________]    |
|  Vintage *           [2015__] (1800-2025)                        |
|                                                                   |
|  Origin                                                           |
|  Region *            [Margaux, Bordeaux, France_____________]    |
|  Varietal            [Cabernet Sauvignon Blend______________]    |
|                                                                   |
|  Acquisition                                                      |
|  Purchase Date *     [03/15/2024  ▼]                            |
|  Cost per Bottle *   [$280.00_]                                  |
|  Retailer            [Wine.com______________________________]    |
|  Quantity *          [3] bottles                                 |
|                                                                   |
|  Cellar Location *                                                |
|  [Zone A > Rack 3 > Shelf 2                               ▼]    |
|                                                                   |
|  Drinking Window (Optional)                                       |
|  From: [2025  ▼]  To: [2035  ▼]                                 |
|                                                                   |
|  Label Photo                                                      |
|  +----------------------------------------------------------+    |
|  | [Drag & drop wine label photo here or click to upload]  |    |
|  +----------------------------------------------------------+    |
|                                                                   |
|  Notes                                                            |
|  [En primeur purchase. Highly rated vintage.________________]    |
|                                                                   |
|  [Cancel]                                          [Add to Cellar]|
|                                                                   |
+------------------------------------------------------------------+
```

## 3. Wine Details

```
+------------------------------------------------------------------+
|  Wine Details                    [Edit] [Open Bottle] [Delete]   |
+------------------------------------------------------------------+
|                                                                   |
|  +-------------------+  +------------------------------------+    |
|  |                   |  | Château Margaux 2015               |    |
|  |   [Wine Label]    |  | Margaux, Bordeaux, France          |    |
|  |                   |  | Cabernet Sauvignon Blend           |    |
|  |     Photo         |  |                                    |    |
|  |                   |  | Bottles: 3                         |    |
|  |                   |  | Location: Zone A, Rack 3, Shelf 2  |    |
|  +-------------------+  |                                    |    |
|                         | Current Value: $420 (+50%)         |    |
|                         | Drinking Window: 2025-2035         |    |
|                         | Status: 🟡 Not quite ready         |    |
|                         +------------------------------------+    |
|                                                                   |
|  Acquisition Details                                              |
|  +------------------------------------------------------------+  |
|  | Purchased: March 15, 2024                                  |  |
|  | Cost: $280 per bottle ($840 total)                        |  |
|  | Source: Wine.com                                           |  |
|  | Age: 9 years (2015 vintage)                               |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Drinking Window Analysis                                         |
|  +------------------------------------------------------------+  |
|  | Optimal drinking: 2025-2035 (10-20 years)                 |  |
|  | Current status: Developing nicely                          |  |
|  | Recommendation: Hold for 1+ years for best results        |  |
|  | Days until optimal: 365 days                              |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Tasting History (0 tastings)                                     |
|  [No bottles opened yet]                                          |
|                                                                   |
|  Notes                                                            |
|  En primeur purchase. Highly rated vintage by Robert Parker      |
|  (98 points). Classic Margaux structure and elegance expected.   |
|                                                                   |
+------------------------------------------------------------------+
```

## 4. Cellar Map

```
+------------------------------------------------------------------+
|  Cellar Map                                    [Edit Layout]      |
+------------------------------------------------------------------+
|                                                                   |
|  Zone A (Climate Controlled - 55°F)                               |
|  +------------------------------------------------------------+  |
|  | Rack 1    Rack 2    Rack 3    Rack 4    Rack 5            |  |
|  | [🍷🍷🍷]  [🍷🍷🍷]  [🍷🟢🟢]  [🍷🍷🍷]  [🍷🍷🍷]            |  |
|  | [🍷🍷🍷]  [🍷🍷🍷]  [🟡🟡🟢]  [🍷🍷🍷]  [🍷🍷🍷]            |  |
|  | [🍷🍷🍷]  [🔴🍷🍷]  [🍷🍷🍷]  [🍷🍷🍷]  [🍷🍷🍷]            |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Zone B (Ambient Storage)                                         |
|  +------------------------------------------------------------+  |
|  | Rack 6    Rack 7    Rack 8                                 |  |
|  | [🍷🍷🍷]  [🍷🍷🍷]  [🍷🍷🍷]                                |  |
|  | [🍷🍷🍷]  [🍷🍷🍷]  [🍷🍷🍷]                                |  |
|  +------------------------------------------------------------+  |
|                                                                   |
|  Legend:  🍷 In Cellar  🟢 Ready Now  🟡 Not Ready  🔴 Drink ASAP|
|                                                                   |
+------------------------------------------------------------------+
```
