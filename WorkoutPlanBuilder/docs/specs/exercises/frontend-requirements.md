# Exercises - Frontend Requirements

## Overview
The Exercises frontend provides an intuitive interface for browsing the exercise library, searching and filtering exercises, adding custom exercises, viewing performance history, and substituting exercises in workout plans.

## User Interface Components

### 1. Exercise Library Page
**Route:** `/exercises`

**Features:**
- Search bar with autocomplete
- Filter panel (category, muscle group, equipment, difficulty)
- Grid/list view toggle
- Exercise cards with thumbnail, name, and quick info
- Pagination or infinite scroll
- Sort options (popularity, alphabetical, recent)

**Components:**
- `ExerciseLibrary` (container)
- `ExerciseSearchBar`
- `ExerciseFilters`
- `ExerciseGrid` / `ExerciseList`
- `ExerciseCard`

### 2. Exercise Detail Page
**Route:** `/exercises/{id}`

**Features:**
- Exercise name and description
- Category and muscle group badges
- Equipment requirements
- Difficulty level indicator
- Detailed instructions
- Video demonstration
- Form tips and common mistakes
- Performance history chart
- "Add to Workout" button
- "Log Performance" button
- "Find Substitutes" link

**Components:**
- `ExerciseDetail` (container)
- `ExerciseHeader`
- `ExerciseInstructions`
- `ExerciseVideo`
- `PerformanceChart`
- `ExerciseActions`

### 3. Add Custom Exercise Form
**Route:** `/exercises/new`

**Features:**
- Exercise name input
- Description textarea
- Category dropdown
- Primary muscle group select
- Secondary muscle groups multi-select
- Equipment type select
- Difficulty level selector
- Instructions rich text editor
- Video URL input
- Image upload
- Save button

**Components:**
- `AddExerciseForm`
- `RichTextEditor`
- `ImageUpload`
- `MuscleGroupSelector`

### 4. Exercise Performance History
**Component:** `ExercisePerformanceHistory`

**Features:**
- Line chart showing weight progression
- Volume chart (sets × reps × weight)
- Personal records highlight
- Date range selector
- Export performance data
- Session details on hover/click

**Chart Types:**
- Weight over time
- Volume over time
- Reps at various weights
- Estimated 1RM progression

### 5. Exercise Substitution Modal
**Component:** `ExerciseSubstitutionModal`

**Features:**
- Reason selection (equipment, injury, preference)
- Suggested alternatives list
- Filter by available equipment
- Compare exercises side-by-side
- Notes field
- Confirm substitution button

### 6. Exercise Selector (for Workout Plans)
**Component:** `ExerciseSelector`

**Features:**
- Modal or sidebar view
- Quick search
- Category tabs
- Recently used section
- Custom exercises section
- Exercise preview on hover
- Multi-select option

## State Management

### Exercise Store/Context

```typescript
interface ExerciseState {
  exercises: Exercise[];
  selectedExercise: Exercise | null;
  filters: ExerciseFilters;
  searchTerm: string;
  loading: boolean;
  error: string | null;
  performanceHistory: ExercisePerformance[];
}

interface ExerciseFilters {
  category?: ExerciseCategory;
  muscleGroup?: MuscleGroup;
  equipment?: EquipmentType;
  difficulty?: DifficultyLevel;
  isCustom?: boolean;
}

interface Exercise {
  id: string;
  name: string;
  description: string;
  category: ExerciseCategory;
  primaryMuscleGroup: MuscleGroup;
  secondaryMuscleGroups: MuscleGroup[];
  equipment: EquipmentType;
  difficulty: DifficultyLevel;
  instructions: string;
  videoUrl?: string;
  imageUrl?: string;
  isCustom: boolean;
  popularityScore: number;
}

interface ExercisePerformance {
  id: string;
  exerciseId: string;
  exerciseName: string;
  performedDate: string;
  totalSets: number;
  totalReps: number;
  maxWeight: number;
  totalVolume: number;
  sets: SetPerformance[];
}

interface SetPerformance {
  setNumber: number;
  reps: number;
  weight: number;
  isWarmup: boolean;
  restSeconds: number;
}
```

### Actions

```typescript
// Load exercises
loadExercises(filters?: ExerciseFilters): Promise<Exercise[]>

// Search exercises
searchExercises(term: string): Promise<Exercise[]>

// Get exercise details
getExercise(id: string): Promise<Exercise>

// Add custom exercise
addExercise(exercise: CreateExerciseDto): Promise<Exercise>

// Update exercise
updateExercise(id: string, exercise: UpdateExerciseDto): Promise<void>

// Record performance
recordPerformance(exerciseId: string, performance: RecordPerformanceDto): Promise<void>

// Get performance history
getPerformanceHistory(exerciseId: string, startDate?: Date, endDate?: Date): Promise<ExercisePerformance[]>

// Get substitutions
getSubstitutions(exerciseId: string, equipment?: EquipmentType): Promise<Exercise[]>

// Substitute exercise
substituteExercise(substitution: SubstituteExerciseDto): Promise<void>
```

## API Integration

### Service Layer

```typescript
class ExerciseService {
  async searchExercises(params: SearchParams): Promise<PagedResult<Exercise>> {
    const response = await api.get('/api/exercises', { params });
    return response.data;
  }

  async getExerciseById(id: string): Promise<Exercise> {
    const response = await api.get(`/api/exercises/${id}`);
    return response.data;
  }

  async addExercise(exercise: CreateExerciseDto): Promise<Exercise> {
    const response = await api.post('/api/exercises', exercise);
    return response.data;
  }

  async updateExercise(id: string, exercise: UpdateExerciseDto): Promise<void> {
    await api.put(`/api/exercises/${id}`, exercise);
  }

  async recordPerformance(exerciseId: string, performance: RecordPerformanceDto): Promise<void> {
    await api.post(`/api/exercises/${exerciseId}/performance`, performance);
  }

  async getPerformanceHistory(exerciseId: string, params: HistoryParams): Promise<ExercisePerformance[]> {
    const response = await api.get(`/api/exercises/${exerciseId}/performance/history`, { params });
    return response.data;
  }

  async getSubstitutions(exerciseId: string, params: SubstitutionParams): Promise<Exercise[]> {
    const response = await api.get(`/api/exercises/${exerciseId}/substitutions`, { params });
    return response.data;
  }

  async substituteExercise(substitution: SubstituteExerciseDto): Promise<void> {
    await api.post('/api/exercises/substitute', substitution);
  }
}
```

## User Interactions

### Browse Exercises
1. User navigates to exercise library
2. System loads and displays exercises
3. User applies filters (category, muscle group, equipment)
4. System updates exercise list
5. User clicks exercise card
6. System navigates to exercise detail page

### Add Custom Exercise
1. User clicks "Add Custom Exercise"
2. System displays exercise form
3. User fills in exercise details
4. User uploads image/video (optional)
5. User clicks "Save"
6. System validates input
7. System creates exercise
8. System displays success message
9. System navigates to exercise detail page

### Record Exercise Performance
1. User views exercise detail page
2. User clicks "Log Performance"
3. System displays performance logging modal
4. User selects workout session
5. User enters sets, reps, and weight
6. User clicks "Save"
7. System records performance
8. System updates performance history chart
9. System checks for new personal records
10. System displays success/PR notification

### Substitute Exercise
1. User editing workout plan
2. User clicks "Substitute" on exercise
3. System displays substitution modal
4. User selects substitution reason
5. System shows suggested alternatives
6. User selects new exercise
7. User adds optional notes
8. User confirms substitution
9. System updates workout plan
10. System displays success message

## Validation Rules

### Add/Edit Exercise Form
- Name: Required, max 200 characters
- Description: Max 1000 characters
- Category: Required
- Primary Muscle Group: Required
- Equipment: Required
- Instructions: Required, max 5000 characters
- Video URL: Valid URL format if provided
- Image: Max 5MB, formats: JPG, PNG, WebP

### Performance Recording
- Workout Session: Required
- At least one set required
- Reps: Integer, 1-100
- Weight: Decimal, 0-1000
- Rest Seconds: Integer, 0-600

## UI/UX Considerations

### Visual Design
- Exercise cards with clear imagery
- Color-coded muscle group indicators
- Difficulty badges (Beginner: Green, Intermediate: Yellow, Advanced: Red)
- Equipment icons for quick recognition
- Clear typography for instructions

### Performance
- Lazy load exercise images
- Virtualize long exercise lists
- Cache frequently accessed exercises
- Debounce search input (300ms)
- Optimize video loading

### Accessibility
- Keyboard navigation support
- ARIA labels for all interactive elements
- Alt text for all exercise images
- Focus indicators
- Screen reader support for charts

### Mobile Responsiveness
- Touch-friendly exercise cards
- Swipeable exercise gallery
- Bottom sheet for filters on mobile
- Simplified performance logging interface
- Responsive charts

## Error Handling

### Error Scenarios
- Exercise not found (404)
- Network errors
- Invalid form input
- Performance recording failure
- Image upload failure

### Error Messages
- "Exercise not found. It may have been deleted."
- "Unable to load exercises. Please try again."
- "Please fill in all required fields."
- "Performance recording failed. Please try again."
- "Image upload failed. Max size is 5MB."

## Notifications

### Success Messages
- "Custom exercise added successfully!"
- "Performance recorded! New personal record!"
- "Exercise substituted in your workout plan"
- "Exercise updated successfully"

### Info Messages
- "Showing 50 exercises matching your filters"
- "No exercises found. Try adjusting your filters"
- "Loading exercise library..."

## Testing Requirements

### Unit Tests
- Component rendering
- Form validation
- State management
- Utility functions

### Integration Tests
- Exercise search and filter
- Add custom exercise flow
- Performance recording
- Exercise substitution

### E2E Tests
- Complete exercise browsing flow
- Add and view custom exercise
- Record performance and view history
- Substitute exercise in workout plan
