# Wine Collection - Frontend Requirements

## Overview
User interface for managing wine collection, viewing cellar inventory, and tracking wine details.

## User Stories
1. Add wines to collection with detailed information
2. View collection in grid or list format
3. Search and filter wines by region, vintage, status
4. View cellar map showing bottle locations
5. Track wines in drinking window
6. Record bottle openings and consumption

## Pages/Views

### 1. Collection Dashboard (`/collection`)
- Grid view of all wines with label photos
- Total collection value and bottle count
- Wines ready to drink alert
- Recent acquisitions
- Quick add wine button

### 2. Add Wine Form
- Wine name, producer, vintage
- Region and varietal selectors
- Acquisition details (date, cost, source)
- Quantity and location
- Drinking window (optional)
- Label photo upload

### 3. Wine Details View (`/wines/:id`)
- Wine information card
- Label photo gallery
- Tasting notes history
- Drinking window status
- Value appreciation chart
- Open bottle button

### 4. Cellar Map (`/cellar`)
- Interactive cellar layout
- Visual representation of racks/shelves
- Color-coded by drinking window status
- Click to view bottle details
- Drag-and-drop to relocate

### 5. Drinking Window View
- List of wines at peak
- Wines approaching window
- Wines past peak (urgent)
- Priority drinking queue

## UI Components

### WineCard
- Label photo thumbnail
- Wine name and vintage
- Producer and region
- Current value
- Drinking window indicator
- Quick actions

### CellarMapVisualization
- SVG-based cellar layout
- Interactive zones and racks
- Bottle position markers
- Status color coding

### DrinkingWindowIndicator
- Traffic light visual (green/yellow/red)
- Days until window start/end
- Optimal drinking status

## State Management

```typescript
interface WineCollectionState {
  wines: Wine[];
  locations: CellarLocation[];
  totalValue: number;
  totalBottles: number;
  winesInWindow: Wine[];
  loading: boolean;
}

interface Wine {
  bottleId: string;
  wineName: string;
  producer: string;
  vintage: number;
  region: string;
  acquisitionCost: number;
  currentValue: number;
  quantity: number;
  drinkingWindowStart?: Date;
  drinkingWindowEnd?: Date;
  status: string;
}
```

## API Integration

```typescript
class WineCollectionService {
  async getWines(): Promise<Wine[]>;
  async createWine(data: CreateWineRequest): Promise<Wine>;
  async openBottle(id: string, notes: OpenBottleRequest): Promise<void>;
  async getWinesInDrinkingWindow(): Promise<Wine[]>;
  async getCollectionValue(): Promise<number>;
}
```
