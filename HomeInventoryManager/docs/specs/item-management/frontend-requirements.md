# Item Management - Frontend Requirements

## Overview
User interface for cataloging and managing household inventory items.

## User Stories
1. Add new items with photos and details quickly
2. View all items in organized grid or list view
3. Search and filter items by category, location, value
4. Edit item details and update values
5. Move items between locations
6. Remove items with disposal tracking
7. Export inventory to CSV/Excel
8. Bulk import items from spreadsheet

## Pages/Views

### 1. Items Dashboard (`/items`)
- Grid/List toggle view of all items
- Summary statistics (Total items, Total value, Categories)
- Quick filters (Category, Location, Status, Value range)
- Search bar for text search
- Sort options (Name, Value, Purchase date, Category)
- Bulk action toolbar (Export, Delete, Move)

### 2. Add Item Form (`/items/new`)
- Item name and description
- Category selector (hierarchical dropdown)
- Location selector (room/storage area)
- Purchase information (date, price, retailer)
- Current value (auto-filled from purchase price)
- Condition selector (Excellent/Good/Fair/Poor)
- Serial number input
- Photo upload with drag-and-drop (up to 10 photos)
- Receipt upload button
- Notes text area
- Save button with validation

### 3. Item Details View (`/items/:id`)
- Photo gallery with primary image prominently displayed
- All item information in organized sections
- Edit button to modify details
- Location change quick action
- Value update quick action
- Activity timeline (created, updated, relocated)
- Delete/Remove item button
- Print/Export item details

### 4. Edit Item Form (`/items/:id/edit`)
- Similar to Add Item form but pre-populated
- Show change history
- Field-level validation
- Cancel and Save changes

### 5. Bulk Import View (`/items/import`)
- File upload area (CSV/Excel)
- Template download link
- Import preview table
- Field mapping interface
- Validation errors display
- Import progress indicator
- Import summary results

### 6. Category Management (`/categories`)
- Category tree view
- Add/Edit/Delete categories
- Set depreciation rates
- Assign insurance classifications
- Category usage statistics

## UI Components

### ItemCard
- Primary photo thumbnail
- Item name and category
- Current value (prominent)
- Location badge
- Quick action menu (View, Edit, Move, Delete)
- Condition indicator

### ItemGrid
- Responsive grid layout (3-4 columns on desktop)
- Virtual scrolling for performance
- Checkbox for bulk selection
- Hover effects with quick actions

### ItemList
- Table view with sortable columns
- Columns: Photo, Name, Category, Location, Purchase Date, Value, Status
- Row actions menu
- Pagination or infinite scroll

### PhotoGallery
- Primary image display
- Thumbnail strip
- Lightbox for full-size viewing
- Photo management (set primary, delete, add caption)
- Zoom and pan controls

### CategorySelector
- Hierarchical dropdown
- Search/filter categories
- Show item count per category
- Quick add new category option

### LocationSelector
- Organized by room/area
- Visual icons for locations
- Show item count per location
- Quick add new location option

### ItemSearchBar
- Instant search as user types
- Search across name, description, serial number
- Recent searches dropdown
- Advanced filter toggle

### ValueEditor
- Currency input with formatting
- Depreciation calculator helper
- Market value lookup suggestion
- Value history chart

## State Management

```typescript
interface ItemManagementState {
  items: Item[];
  selectedItems: string[];
  categories: Category[];
  currentItem: Item | null;
  filters: ItemFilters;
  searchQuery: string;
  viewMode: 'grid' | 'list';
  sortBy: SortOption;
  loading: boolean;
  error: string | null;
}

interface Item {
  itemId: string;
  name: string;
  description: string;
  categoryId: string;
  categoryName: string;
  purchaseDate?: Date;
  purchasePrice?: number;
  currentValue: number;
  locationId: string;
  locationName: string;
  serialNumber?: string;
  condition: 'Excellent' | 'Good' | 'Fair' | 'Poor';
  status: 'Active' | 'Disposed' | 'Sold' | 'Donated' | 'Lost' | 'Stolen';
  photos: Photo[];
  receiptId?: string;
  notes?: string;
  createdAt: Date;
  updatedAt: Date;
}

interface Category {
  categoryId: string;
  name: string;
  parentCategoryId?: string;
  depreciationRate: number;
  insuranceClassification?: string;
  iconName?: string;
  itemCount: number;
}

interface Photo {
  photoId: string;
  url: string;
  thumbnailUrl?: string;
  uploadedAt: Date;
  isPrimary: boolean;
  caption?: string;
}

interface ItemFilters {
  categoryIds: string[];
  locationIds: string[];
  status: string[];
  minValue?: number;
  maxValue?: number;
  condition?: string[];
}
```

## API Integration

```typescript
class ItemService {
  async getItems(params: GetItemsParams): Promise<PaginatedResult<Item>>;
  async getItem(itemId: string): Promise<Item>;
  async createItem(data: CreateItemRequest): Promise<Item>;
  async updateItem(itemId: string, updates: UpdateItemRequest): Promise<Item>;
  async deleteItem(itemId: string, reason: string, disposalMethod: string): Promise<void>;
  async relocateItem(itemId: string, locationId: string, reason: string): Promise<Item>;
  async bulkImport(file: File): Promise<ImportResult>;
  async exportItems(params: ExportParams): Promise<Blob>;
  async getCategories(): Promise<Category[]>;
  async uploadPhoto(itemId: string, photo: File): Promise<Photo>;
  async deletePhoto(photoId: string): Promise<void>;
}
```

## User Interactions

### Adding an Item
1. Click "Add Item" button
2. Fill required fields (name, category, location)
3. Optionally add photos, receipt, purchase info
4. Click "Save"
5. See success notification
6. Redirect to item details or dashboard

### Editing an Item
1. Click item to view details
2. Click "Edit" button
3. Modify fields
4. Click "Save Changes"
5. See update confirmation
6. Return to item details with updated info

### Relocating an Item
1. From item details, click "Change Location"
2. Select new location from modal
3. Optionally add reason for move
4. Confirm relocation
5. See location updated in UI

### Bulk Import
1. Navigate to Import page
2. Download template if needed
3. Upload filled CSV/Excel file
4. Review preview and fix errors
5. Confirm import
6. See progress bar
7. View import summary (success/failed)

### Search and Filter
1. Type in search bar for instant results
2. Click filter icon to open filter panel
3. Select categories, locations, value range
4. Results update in real-time
5. Clear filters to reset

## Responsive Design

- **Mobile**: Single column, card view, bottom nav
- **Tablet**: 2-column grid, side panel filters
- **Desktop**: 3-4 column grid, persistent filter sidebar

## Accessibility

- Keyboard navigation support
- ARIA labels for all interactive elements
- Screen reader friendly
- High contrast mode support
- Focus indicators

## Performance Optimizations

- Virtual scrolling for large item lists
- Lazy loading of images
- Debounced search
- Optimistic UI updates
- Image compression on upload
- Cached category and location lists

## Notifications

### Success
- "Item added successfully"
- "Item updated"
- "Item relocated to [Location]"
- "Item removed from inventory"
- "X items imported successfully"

### Errors
- "Failed to save item. Please try again."
- "Photo upload failed. Max size is 5MB."
- "Serial number already exists in your inventory"
- "Import failed: Invalid file format"
- "You can only upload up to 10 photos per item"

## Validation

- Name: Required, max 200 characters
- Category: Required selection
- Location: Required selection
- Purchase price: Non-negative number
- Current value: Non-negative number
- Serial number: Max 100 characters, alphanumeric
- Photos: Max 10 per item, max 5MB each
- Description: Max 2000 characters
- Notes: Max 5000 characters
