# Chore Management - Frontend Requirements

## Overview
User interface for creating, editing, and managing household chores with gamification elements.

## Key Pages

### 1. Chores Dashboard
**Route**: `/chores`

**Features**:
- List of all household chores
- Filter by category (Cleaning, Cooking, Outdoor, etc.)
- Filter by difficulty (Easy, Medium, Hard)
- Quick stats (Total chores, Active, Completed today)
- Add Chore button
- Chore cards with icons and point values
- Assignment status indicators

### 2. Create Chore Form
**Route**: `/chores/create`

**Form Fields**:
- Chore Name (required)
- Description (required)
- Category (dropdown: Cleaning, Cooking, Outdoor, Maintenance, Pet Care, Other)
- Estimated Duration (minutes, slider or input)
- Difficulty Level (Easy, Medium, Hard - affects points)
- Point Value (auto-calculated, can override)
- Frequency (One-time, Daily, Weekly, Bi-weekly, Monthly)
- Instructions (textarea)
- Upload Photo (optional, for reference)

**Features**:
- Point value calculator shows points as difficulty/duration change
- Visual preview of chore card
- Save as template option
- Real-time validation

### 3. Chore Details View
**Route**: `/chores/{choreId}`

**Sections**:
- **Chore Info**: Name, description, category, difficulty, points
- **Assignment**: Current assignee and due date
- **Instructions**: Step-by-step if provided
- **Photo**: Reference image if uploaded
- **Completion History**: List of past completions with ratings
- **Assignment History**: Who has done this chore and how often
- **Actions**: Assign, Edit, Delete

### 4. Chore Library
**Component**: Browseable library of chores

**Features**:
- Pre-defined chore templates
- Search chores by name
- Filter by category
- Quick add from template
- Customize before adding

## UI Components

### ChoreCard
- Chore icon (category-based)
- Chore name
- Point value badge
- Difficulty indicator (1-3 stars)
- Duration estimate
- Assigned to (if assigned)
- Quick assign button

### PointBadge
- Circular badge with point value
- Color-coded by difficulty:
  - Green: Easy (1-5 points)
  - Yellow: Medium (6-10 points)
  - Red: Hard (11-20 points)

### DifficultyStars
- Visual difficulty rating (★☆☆, ★★☆, ★★★)
- Helps kids understand effort required

### ChoreIcon
- Category-specific icons
- Customizable colors
- Consistent design language

## User Flows

### Create Chore Flow
1. User clicks "Add Chore"
2. Form displays with required fields
3. User enters chore name and description
4. User selects category (icon shows)
5. User sets duration (slider shows minutes)
6. User selects difficulty
7. Point value auto-calculates and displays
8. User can override points if desired
9. User adds optional instructions
10. User uploads optional photo
11. User clicks "Create Chore"
12. Chore added to library
13. Prompt to assign chore immediately

### Edit Chore Flow
1. User views chore details
2. User clicks "Edit Chore"
3. Form pre-populated with current values
4. User modifies fields
5. Warning shows if active assignments affected
6. User saves changes
7. Active assignments updated
8. Success notification shown

### Assign Chore Flow
1. User selects chore
2. Clicks "Assign Chore"
3. Dialog shows household members
4. User selects assignee
5. System suggests due date based on frequency
6. User confirms assignment
7. Assignee receives notification
8. Chore appears on assignee's todo list

## State Management

```typescript
interface ChoreState {
  chores: Chore[];
  selectedChore: Chore | null;
  loading: boolean;
  error: string | null;
  filters: ChoreFilters;
}

interface Chore {
  choreId: string;
  householdId: string;
  choreName: string;
  description: string;
  category: ChoreCategory;
  estimatedDuration: number; // minutes
  difficultyLevel: 'Easy' | 'Medium' | 'Hard';
  pointValue: number;
  frequency: 'OneTime' | 'Daily' | 'Weekly' | 'BiWeekly' | 'Monthly';
  isActive: boolean;
  instructions?: string;
  imageUrl?: string;
  currentAssignment?: Assignment;
}

interface Assignment {
  assignmentId: string;
  assignedTo: string;
  assignedToName: string;
  dueDate: Date;
  status: 'Pending' | 'InProgress' | 'Completed';
}

type ChoreCategory = 'Cleaning' | 'Cooking' | 'Outdoor' | 'Maintenance' | 'PetCare' | 'Other';
```

## Responsive Design

### Mobile (Kid-Friendly)
- Large touch targets
- Big, colorful icons
- Simple, clear labels
- Bottom navigation
- Swipe gestures for quick actions
- Large point badges

### Tablet
- Two-column chore grid
- Expanded chore cards
- Side panel for details

### Desktop (Parent View)
- Multi-column layout
- Advanced filters
- Bulk actions
- Detailed analytics

## Gamification Elements

- **Point Badges**: Visual point values
- **Difficulty Stars**: Clear effort indicators
- **Color Coding**: Category-based colors
- **Icons**: Fun, engaging visuals
- **Animations**: Success animations on completion
- **Progress Bars**: Visual completion tracking

## Testing
- E2E: Complete chore creation flow
- E2E: Assign chore to family member
- E2E: Edit chore with active assignment
- Unit: Point value calculation
- Unit: Due date calculation based on frequency
- Accessibility: Kid-friendly interface testing
