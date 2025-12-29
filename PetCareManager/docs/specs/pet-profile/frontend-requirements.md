# Pet Profile - Frontend Requirements

## Overview
The Pet Profile frontend provides an intuitive interface for managing pet information, tracking health metrics, and viewing pet history. The interface should be responsive, accessible, and provide real-time updates using domain events.

## Technology Stack

### Recommended Technologies
- **Framework:** React 18+ with TypeScript
- **State Management:** Redux Toolkit or Zustand
- **UI Library:** Material-UI or Tailwind CSS
- **Forms:** React Hook Form with Zod validation
- **Data Fetching:** TanStack Query (React Query)
- **Real-time:** SignalR for event notifications
- **Charts:** Recharts or Chart.js for weight tracking
- **Image Upload:** React Dropzone
- **Date Picker:** React DatePicker

## Page Structure

### 1. Pet List Page
**Route:** `/households/{householdId}/pets`

**Components:**
- `PetListHeader` - Title, search, and add pet button
- `PetGrid` - Grid layout of pet cards
- `PetCard` - Individual pet summary card
- `EmptyState` - Displayed when no pets exist
- `FilterBar` - Filter by species, deceased status

**Features:**
- Grid view of all household pets
- Search pets by name
- Filter by species (All, Dog, Cat, etc.)
- Toggle to show/hide deceased pets
- Quick actions: View, Edit, Delete
- Add new pet button (prominent CTA)

**State Management:**
```typescript
interface PetListState {
  pets: Pet[];
  loading: boolean;
  error: string | null;
  filters: {
    search: string;
    species: Species | 'All';
    includeDeceased: boolean;
  };
  sortBy: 'name' | 'dateAdded' | 'species';
}
```

### 2. Pet Detail Page
**Route:** `/pets/{id}`

**Components:**
- `PetHeader` - Photo, name, species, age
- `PetInfoSection` - Basic information display
- `WeightTracker` - Weight history chart and table
- `MedicalNotesSection` - Medical information
- `ActivityTimeline` - Recent events and updates
- `QuickActions` - Edit, Record Weight, etc.

**Tabs:**
- **Profile** - Basic information
- **Weight** - Weight tracking and history
- **Medical** - Health records and notes
- **Activity** - Timeline of all events

**Features:**
- Full pet details with large photo
- Edit inline or via modal
- Weight tracking chart with trends
- Timeline of all pet-related events
- Quick access to common actions
- Share profile (future)

### 3. Add/Edit Pet Form
**Route:** `/pets/new` or `/pets/{id}/edit`

**Form Fields:**

**Basic Information:**
- Pet Name* (text, max 100 chars)
- Species* (dropdown: Dog, Cat, Bird, Fish, Reptile, Small Mammal, Other)
- Breed (text, max 100 chars, optional)
- Gender* (radio: Male, Female, Unknown)
- Date of Birth (date picker, optional)
- Color/Markings (text, max 50 chars, optional)

**Identification:**
- Microchip Number (text, max 15 chars, optional)
- Photo Upload (image, max 5MB)

**Health Information:**
- Current Weight (number, kg, optional)
- Medical Notes (textarea, max 2000 chars, optional)
- Special Needs (textarea, max 1000 chars, optional)

**Validation Rules:**
- Name is required
- Species and Gender are required
- Date of Birth cannot be future
- Microchip must be unique (validated server-side)
- Weight must be positive number
- Photo must be image type (jpg, png, gif)

**UX Patterns:**
- Multi-step wizard for new pets (3 steps)
- Inline validation with error messages
- Auto-save draft (localStorage)
- Confirm before discarding changes
- Success notification after save

### 4. Weight Recording Modal
**Trigger:** Button on pet detail page

**Form Fields:**
- Weight* (number input, kg)
- Date Recorded* (date/time picker, default: now)
- Notes (textarea, max 500 chars, optional)

**Features:**
- Previous weight displayed for reference
- Show weight change (+/- kg and %)
- Alert if change > 20%
- Quick preset date/times (Today, Yesterday)
- Chart preview updates in real-time

### 5. Deceased Pet Modal
**Trigger:** Mark as Deceased button

**Form Fields:**
- Date of Death* (date picker)
- Memorial Notes (textarea, optional)

**Features:**
- Confirmation dialog
- Explanation of what happens next
- Option to archive or keep visible
- Generate memorial card (future)

## UI Components Library

### Core Components

#### PetCard
```typescript
interface PetCardProps {
  pet: Pet;
  onView: (id: string) => void;
  onEdit: (id: string) => void;
  onDelete: (id: string) => void;
  showActions?: boolean;
}
```

**Design:**
- Photo on top (square, object-fit: cover)
- Name and species below
- Age badge
- Deceased indicator (grayscale + ribbon)
- Hover actions menu

#### PetAvatar
```typescript
interface PetAvatarProps {
  src?: string;
  species: Species;
  size: 'sm' | 'md' | 'lg' | 'xl';
  deceased?: boolean;
}
```

**Design:**
- Circular avatar
- Fallback icon based on species
- Size variants: 40px, 64px, 96px, 200px
- Grayscale filter if deceased

#### WeightChart
```typescript
interface WeightChartProps {
  data: WeightRecord[];
  height?: number;
  showTrend?: boolean;
  highlightAnomalies?: boolean;
}
```

**Design:**
- Line chart with data points
- X-axis: Date
- Y-axis: Weight (kg)
- Trend line (dashed)
- Highlight outliers in red
- Tooltips on hover

#### SpeciesIcon
```typescript
interface SpeciesIconProps {
  species: Species;
  size?: number;
  color?: string;
}
```

**Design:**
- SVG icons for each species
- Dog: 🐕 cat: 🐈 bird: 🐦 etc.
- Consistent size and style

### Form Components

#### PetFormWizard
**Steps:**
1. Basic Info (Name, Species, Breed, Gender)
2. Details (DOB, Color, Photo)
3. Health (Weight, Medical Notes, Special Needs)

**Features:**
- Progress indicator
- Next/Previous navigation
- Skip optional steps
- Review before submit

#### ImageUploader
**Features:**
- Drag and drop zone
- File browser fallback
- Image preview
- Crop/resize tool
- Remove uploaded image
- Progress indicator

### Display Components

#### PetInfoGrid
**Layout:**
```
┌─────────────────────────┬─────────────────────────┐
│ Species: Dog            │ Gender: Male            │
├─────────────────────────┼─────────────────────────┤
│ Breed: Golden Retriever │ Age: 3 years 2 months   │
├─────────────────────────┼─────────────────────────┤
│ Color: Golden           │ Microchip: 123456789    │
└─────────────────────────┴─────────────────────────┘
```

#### ActivityTimeline
**Events to Display:**
- PetRegistered: "Max was registered"
- PetProfileUpdated: "Profile updated by John"
- PetWeightRecorded: "Weight recorded: 25.5 kg"
- PetPassedAway: "Max passed away"

**Design:**
- Vertical timeline with icons
- Most recent at top
- Date/time stamps
- Event-specific styling
- Expandable details

## Responsive Design

### Breakpoints
- Mobile: < 640px
- Tablet: 640px - 1024px
- Desktop: > 1024px

### Mobile Adaptations
- **Pet List:** Single column card layout
- **Pet Detail:** Stacked sections, full-width tabs
- **Forms:** Single column, larger touch targets
- **Charts:** Responsive width, simplified on mobile
- **Navigation:** Hamburger menu, bottom nav bar

### Tablet Adaptations
- **Pet List:** 2-column grid
- **Pet Detail:** Side-by-side photo and info
- **Forms:** Two-column layout for related fields

### Desktop
- **Pet List:** 3-4 column grid
- **Pet Detail:** Sidebar layout with tabs
- **Forms:** Optimized multi-column layouts

## State Management

### Global State (Redux/Zustand)

```typescript
interface AppState {
  pets: {
    items: Record<string, Pet>;
    lists: {
      byHousehold: Record<string, string[]>;
    };
    loading: boolean;
    error: string | null;
  };
  ui: {
    selectedPetId: string | null;
    modals: {
      addPet: boolean;
      editPet: boolean;
      recordWeight: boolean;
      markDeceased: boolean;
    };
    filters: PetFilters;
  };
  user: {
    currentHouseholdId: string;
    preferences: UserPreferences;
  };
}
```

### Local Component State
- Form inputs (React Hook Form)
- UI toggles (show/hide)
- Pagination state
- Temporary calculations

### Server State (React Query)
```typescript
// Queries
const { data: pets } = useQuery(['pets', householdId], () =>
  api.getPets(householdId)
);

const { data: pet } = useQuery(['pet', petId], () =>
  api.getPet(petId)
);

const { data: weights } = useQuery(['weights', petId], () =>
  api.getWeightHistory(petId)
);

// Mutations
const registerPet = useMutation(api.registerPet, {
  onSuccess: (data) => {
    queryClient.invalidateQueries(['pets']);
    showNotification('Pet registered successfully!');
  }
});

const updatePet = useMutation(api.updatePet, {
  onSuccess: () => {
    queryClient.invalidateQueries(['pet', petId]);
    queryClient.invalidateQueries(['pets']);
  }
});
```

## Real-time Updates

### SignalR Integration

```typescript
// Connect to event hub
const connection = new HubConnectionBuilder()
  .withUrl('/hubs/pets')
  .withAutomaticReconnect()
  .build();

// Subscribe to events
connection.on('PetRegistered', (event) => {
  queryClient.invalidateQueries(['pets', event.householdId]);
  showNotification(`New pet ${event.name} registered!`);
});

connection.on('PetProfileUpdated', (event) => {
  queryClient.invalidateQueries(['pet', event.petId]);
  showNotification('Pet profile updated');
});

connection.on('PetWeightRecorded', (event) => {
  queryClient.invalidateQueries(['weights', event.petId]);
  updateWeightChart(event);
});

connection.on('PetPassedAway', (event) => {
  queryClient.invalidateQueries(['pet', event.petId]);
  showCondolenceMessage(event);
});
```

### Optimistic Updates
```typescript
const recordWeight = useMutation(api.recordWeight, {
  onMutate: async (newWeight) => {
    // Cancel outgoing refetches
    await queryClient.cancelQueries(['weights', petId]);

    // Snapshot previous value
    const previous = queryClient.getQueryData(['weights', petId]);

    // Optimistically update
    queryClient.setQueryData(['weights', petId], (old) => ({
      ...old,
      weights: [...old.weights, newWeight]
    }));

    return { previous };
  },
  onError: (err, variables, context) => {
    // Rollback on error
    queryClient.setQueryData(['weights', petId], context.previous);
  },
  onSettled: () => {
    // Refetch after error or success
    queryClient.invalidateQueries(['weights', petId]);
  }
});
```

## User Experience Patterns

### Loading States
- **Initial Load:** Skeleton screens for pet cards
- **List Loading:** Shimmer effect on grid
- **Form Submit:** Disable button, show spinner
- **Image Upload:** Progress bar

### Error Handling
- **Network Error:** Retry button, offline indicator
- **Validation Error:** Inline field errors in red
- **Server Error:** Toast notification with details
- **404 Not Found:** Friendly message with back button

### Success Feedback
- **Pet Registered:** Success toast + redirect to profile
- **Profile Updated:** Toast notification, stay on page
- **Weight Recorded:** Chart updates, success message
- **Pet Deleted:** Undo snackbar (5 second window)

### Confirmation Dialogs
Required for:
- Deleting a pet
- Marking pet as deceased
- Discarding form changes
- Removing photo

Pattern:
```
┌─────────────────────────────────┐
│  Are you sure?                  │
│                                 │
│  This will mark Max as deceased │
│  and archive all appointments.  │
│                                 │
│  [Cancel]  [Confirm]            │
└─────────────────────────────────┘
```

## Accessibility (a11y)

### WCAG 2.1 AA Compliance
- Keyboard navigation for all interactive elements
- Focus indicators visible (2px blue outline)
- Color contrast ratio > 4.5:1 for text
- Alt text for all pet images
- ARIA labels for icons and buttons
- Form labels properly associated
- Error messages announced to screen readers

### Semantic HTML
```html
<main role="main">
  <section aria-labelledby="pets-heading">
    <h1 id="pets-heading">My Pets</h1>
    <div role="list">
      <article role="listitem">
        <!-- Pet card -->
      </article>
    </div>
  </section>
</main>
```

### Keyboard Shortcuts
- `Alt + N` - Add new pet
- `Alt + S` - Focus search
- `Enter` - Open pet detail (when card focused)
- `Escape` - Close modal
- `Tab/Shift+Tab` - Navigate through form fields

## Performance Optimization

### Code Splitting
```typescript
// Lazy load routes
const PetList = lazy(() => import('./pages/PetList'));
const PetDetail = lazy(() => import('./pages/PetDetail'));
const PetForm = lazy(() => import('./pages/PetForm'));

// Route configuration
<Route path="/pets" element={
  <Suspense fallback={<LoadingSpinner />}>
    <PetList />
  </Suspense>
} />
```

### Image Optimization
- Compress uploaded images (client-side)
- Serve WebP format with fallback
- Lazy load images below fold
- Use thumbnails in list views (150x150)
- Full resolution only in detail view

### Memoization
```typescript
// Expensive computations
const petsBySpecies = useMemo(() =>
  groupBy(pets, 'species'),
  [pets]
);

// Component optimization
const PetCard = memo(({ pet, onView }) => {
  // ...
}, (prevProps, nextProps) =>
  prevProps.pet.id === nextProps.pet.id &&
  prevProps.pet.updatedAt === nextProps.pet.updatedAt
);
```

### Virtualization
For households with 50+ pets:
```typescript
import { FixedSizeGrid } from 'react-window';

<FixedSizeGrid
  columnCount={3}
  columnWidth={300}
  height={600}
  rowCount={Math.ceil(pets.length / 3)}
  rowHeight={350}
  width={1000}
>
  {({ columnIndex, rowIndex, style }) => (
    <div style={style}>
      <PetCard pet={pets[rowIndex * 3 + columnIndex]} />
    </div>
  )}
</FixedSizeGrid>
```

## Testing Requirements

### Unit Tests (Jest + React Testing Library)
```typescript
describe('PetCard', () => {
  it('renders pet name and species', () => {
    const pet = { name: 'Max', species: 'Dog' };
    render(<PetCard pet={pet} />);
    expect(screen.getByText('Max')).toBeInTheDocument();
    expect(screen.getByText('Dog')).toBeInTheDocument();
  });

  it('displays deceased indicator', () => {
    const pet = { name: 'Max', isDeceased: true };
    render(<PetCard pet={pet} />);
    expect(screen.getByLabelText('Deceased')).toBeInTheDocument();
  });

  it('calls onView when clicked', () => {
    const onView = jest.fn();
    render(<PetCard pet={pet} onView={onView} />);
    fireEvent.click(screen.getByRole('button', { name: /view/i }));
    expect(onView).toHaveBeenCalledWith(pet.id);
  });
});
```

### Integration Tests
- Form submission flows
- API integration with MSW (Mock Service Worker)
- Navigation between pages
- Real-time event handling

### E2E Tests (Playwright/Cypress)
```typescript
test('Register a new pet', async ({ page }) => {
  await page.goto('/pets');
  await page.click('text=Add Pet');

  await page.fill('[name="name"]', 'Buddy');
  await page.selectOption('[name="species"]', 'Dog');
  await page.selectOption('[name="gender"]', 'Male');

  await page.click('text=Next');
  await page.fill('[name="breed"]', 'Labrador');

  await page.click('text=Submit');

  await expect(page.locator('text=Pet registered successfully')).toBeVisible();
  await expect(page.locator('text=Buddy')).toBeVisible();
});
```

### Visual Regression Tests (Chromatic/Percy)
- Pet card variations
- Form states (empty, filled, error)
- Responsive layouts
- Theme variations

## Design System Integration

### Color Palette
```css
:root {
  --primary: #3b82f6;      /* Blue */
  --success: #10b981;      /* Green */
  --warning: #f59e0b;      /* Amber */
  --error: #ef4444;        /* Red */
  --deceased: #6b7280;     /* Gray */

  --bg-primary: #ffffff;
  --bg-secondary: #f9fafb;
  --text-primary: #111827;
  --text-secondary: #6b7280;
  --border: #e5e7eb;
}
```

### Typography
- **Headings:** Inter, semi-bold
  - H1: 32px
  - H2: 24px
  - H3: 20px
- **Body:** Inter, regular, 16px
- **Small:** Inter, regular, 14px

### Spacing Scale
- xs: 4px
- sm: 8px
- md: 16px
- lg: 24px
- xl: 32px
- 2xl: 48px

### Shadow Levels
```css
--shadow-sm: 0 1px 2px rgba(0, 0, 0, 0.05);
--shadow-md: 0 4px 6px rgba(0, 0, 0, 0.1);
--shadow-lg: 0 10px 15px rgba(0, 0, 0, 0.1);
```

## Localization (i18n)

### Translation Keys
```typescript
{
  "pets.title": "My Pets",
  "pets.addButton": "Add Pet",
  "pets.empty": "No pets yet. Add your first pet!",

  "species.dog": "Dog",
  "species.cat": "Cat",
  "species.bird": "Bird",

  "form.name.label": "Pet Name",
  "form.name.required": "Name is required",
  "form.species.label": "Species",

  "weight.recorded": "Weight recorded successfully",
  "weight.trend.up": "Weight increasing",
  "weight.trend.down": "Weight decreasing",

  "deceased.confirm": "Are you sure you want to mark {name} as deceased?",
  "deceased.condolence": "We're sorry for your loss of {name}."
}
```

### Date/Time Formatting
```typescript
import { format } from 'date-fns';
import { enUS, es, fr } from 'date-fns/locale';

const formatDate = (date, locale = 'en-US') => {
  const localeMap = { 'en-US': enUS, 'es': es, 'fr': fr };
  return format(date, 'PP', { locale: localeMap[locale] });
};
```

## Analytics & Tracking

### Events to Track
```typescript
// Registration flow
analytics.track('Pet Registration Started');
analytics.track('Pet Registration Completed', {
  species: 'Dog',
  timeToComplete: 120 // seconds
});

// Engagement
analytics.track('Pet Profile Viewed', { petId });
analytics.track('Weight Recorded', { petId });
analytics.track('Photo Uploaded', { petId });

// Features
analytics.track('Weight Chart Viewed');
analytics.track('Filter Applied', { filterType: 'species' });
```

## Browser Support

- Chrome/Edge: Last 2 versions
- Firefox: Last 2 versions
- Safari: Last 2 versions
- Mobile Safari: iOS 13+
- Chrome Android: Last 2 versions

## Future Enhancements

1. **Advanced Features**
   - Multi-photo gallery with carousel
   - Pet comparison (side by side)
   - Export profile as PDF
   - Print memorial cards

2. **Social Features**
   - Share pet profile (read-only link)
   - Pet profile QR code
   - Community features

3. **AI Features**
   - Breed identification from photo
   - Weight prediction based on trends
   - Anomaly detection alerts

4. **PWA Features**
   - Offline support
   - Add to home screen
   - Push notifications
   - Background sync
