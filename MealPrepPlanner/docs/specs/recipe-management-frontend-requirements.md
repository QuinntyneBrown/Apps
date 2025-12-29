# Recipe Management - Frontend Requirements

## 1. Overview

The Recipe Management frontend provides an intuitive interface for browsing, creating, editing, and organizing recipes with rich media support and advanced search capabilities.

## 2. Pages and Routes

### 2.1 Recipe Library
**Route**: `/recipes`

**Components**:
- Search bar with filters
- Grid/list view toggle
- Recipe cards with images
- Category sidebar
- Favorites filter
- Sorting options

### 2.2 Recipe Details
**Route**: `/recipes/:id`

**Components**:
- Recipe header (name, image, rating)
- Ingredients list with scaling
- Step-by-step instructions
- Nutrition information panel
- Action buttons (favorite, edit, delete, prepare)
- Similar recipes suggestions

### 2.3 Create/Edit Recipe
**Route**: `/recipes/new` or `/recipes/:id/edit`

**Components**:
- Multi-step form
- Image upload
- Ingredients builder
- Instructions editor
- Nutrition calculator
- Tags input
- Preview mode

### 2.4 Favorites Collection
**Route**: `/recipes/favorites`

**Components**:
- Favorited recipes grid
- Quick access actions
- Export options

## 3. Components

### 3.1 RecipeCard
**Props**:
- `recipe`: Recipe object
- `onFavorite`: Function
- `onClick`: Function
- `showActions`: Boolean

**Display**:
- Recipe image
- Name and description
- Prep/cook time
- Servings
- Rating stars
- Favorite icon
- Quick actions

### 3.2 RecipeDetail
**Features**:
- Image carousel
- Servings adjuster (scales ingredients)
- Print recipe button
- Share functionality
- Add to meal plan
- Mark as prepared

### 3.3 IngredientsList
**Features**:
- Checkbox for shopping list
- Quantity scaling
- Substitution suggestions
- Nutritional breakdown per ingredient

### 3.4 InstructionsPanel
**Features**:
- Step-by-step view
- Cooking mode (hands-free, large text)
- Timer integration
- Step images
- Voice commands support

### 3.5 RecipeForm
**Sections**:
1. Basic Info (name, description, category)
2. Timing (prep, cook)
3. Ingredients (add/remove, quantities)
4. Instructions (ordered steps)
5. Nutrition (auto-calculate or manual)
6. Tags and metadata

### 3.6 RecipeSearch
**Features**:
- Autocomplete
- Advanced filters panel
- Recent searches
- Saved search filters

### 3.7 NutritionPanel
**Display**:
- Calories per serving
- Macronutrients (protein, carbs, fat)
- Micronutrients
- Visual charts
- Daily value percentages

### 3.8 CookingModeView
**Features**:
- Full-screen view
- Large, readable text
- Voice navigation
- Ingredient checklist
- Integrated timers
- Keep screen awake

## 4. State Management

### 4.1 Recipe State
```typescript
interface RecipeState {
  recipes: Recipe[];
  selectedRecipe: Recipe | null;
  favorites: Recipe[];
  recentlyViewed: Recipe[];
  categories: Category[];
  loading: boolean;
  error: string | null;
  filters: RecipeFilters;
  searchTerm: string;
  sortBy: SortOption;
  viewMode: 'grid' | 'list';
}

interface RecipeFilters {
  categoryId?: string;
  cuisineType?: string;
  difficulty?: Difficulty;
  maxPrepTime?: number;
  tags?: string[];
  favoritesOnly?: boolean;
}
```

### 4.2 Actions
```typescript
// Async actions
fetchRecipes(filters?: RecipeFilters)
fetchRecipeById(id: string)
createRecipe(data: CreateRecipeRequest)
updateRecipe(id: string, data: UpdateRecipeRequest)
deleteRecipe(id: string)
toggleFavorite(id: string)
markPrepared(id: string, data: PreparedData)
calculateNutrition(id: string)
searchRecipes(term: string)

// Sync actions
setFilters(filters: RecipeFilters)
setSearchTerm(term: string)
setSortBy(sortBy: SortOption)
setViewMode(mode: 'grid' | 'list')
clearFilters()
```

## 5. API Integration

```typescript
class RecipeService {
  async getRecipes(filters?: RecipeFilters): Promise<PagedResult<Recipe>>
  async getRecipeById(id: string): Promise<Recipe>
  async createRecipe(data: CreateRecipeRequest): Promise<Recipe>
  async updateRecipe(id: string, data: UpdateRecipeRequest): Promise<Recipe>
  async deleteRecipe(id: string): Promise<void>
  async toggleFavorite(id: string): Promise<void>
  async markPrepared(id: string, data: PreparedData): Promise<void>
  async calculateNutrition(id: string): Promise<NutritionInfo>
  async uploadImage(file: File): Promise<string>
}
```

## 6. User Interactions

### 6.1 Browse Recipes
1. View recipe grid/list
2. Apply filters (category, cuisine, time)
3. Search by name or ingredient
4. Click recipe to view details

### 6.2 Create Recipe
1. Click "Add Recipe"
2. Fill basic information
3. Add ingredients with quantities
4. Write step-by-step instructions
5. Upload images
6. Add tags
7. Calculate or enter nutrition
8. Save recipe

### 6.3 Use Recipe in Cooking
1. Open recipe details
2. Click "Cooking Mode"
3. Adjust servings (ingredients scale)
4. Check off ingredients
5. Follow instructions step-by-step
6. Use timers as needed
7. Mark as prepared when done
8. Rate recipe

### 6.4 Manage Recipe
1. Edit recipe details
2. Add to favorites
3. Add to meal plan
4. Share with others
5. Export or print
6. Delete recipe

## 7. UI/UX Requirements

### 7.1 Recipe Cards
- High-quality images
- Hover effects
- Quick actions on hover
- Responsive grid layout
- Loading skeletons

### 7.2 Detail View
- Hero image section
- Clean, readable typography
- Print-friendly layout
- Sticky action buttons
- Breadcrumb navigation

### 7.3 Form Design
- Multi-step wizard
- Progress indicator
- Auto-save drafts
- Validation feedback
- Rich text editor for instructions

### 7.4 Filters and Search
- Collapsible filter panel
- Tag chips for active filters
- Clear all filters option
- Search suggestions
- Filter count badges

### 7.5 Responsive Breakpoints
- Desktop: Full grid, sidebar filters
- Tablet: 2-column grid, drawer filters
- Mobile: Single column, bottom sheet filters

## 8. Visual Design

### 8.1 Color Scheme
- Primary: #4CAF50 (green)
- Secondary: #FF9800 (orange)
- Background: #F5F5F5
- Cards: White
- Text: #333 (dark), #666 (medium), #999 (light)

### 8.2 Typography
- Headings: Bold, 1.5-2.5rem
- Body: Regular, 1rem
- Small text: 0.875rem
- Font: System fonts stack

### 8.3 Icons
- ⭐ Favorite/Rating
- ⏱️ Prep/Cook time
- 👥 Servings
- 🏷️ Tags
- 📊 Nutrition
- 🖼️ Image
- ✏️ Edit
- 🗑️ Delete
- 📋 Copy
- 🖨️ Print

## 9. Advanced Features

### 9.1 Servings Scaler
- Adjust servings count
- Auto-scale all ingredients
- Recalculate nutrition
- Handle fractional quantities

### 9.2 Ingredient Substitutions
- Suggest alternatives
- Common swaps
- Dietary restriction options
- Availability-based suggestions

### 9.3 Recipe Import
- Paste URL to import
- Parse from text
- Import from file
- Supported sites integration

### 9.4 Smart Search
- Search by ingredients ("chicken, rice, broccoli")
- Natural language queries
- Fuzzy matching
- Search history

### 9.5 Recipe Collections
- Create custom collections
- Weekly meal prep collection
- Holiday recipes
- Quick dinners
- Share collections

## 10. Accessibility

### 10.1 Keyboard Navigation
- Tab through recipe cards
- Arrow keys in cooking mode
- Shortcuts for common actions
- Skip links

### 10.2 Screen Reader Support
- Alt text for images
- ARIA labels
- Descriptive button text
- Form field labels

### 10.3 Visual Accessibility
- High contrast mode
- Adjustable text size
- Color-blind friendly
- Focus indicators

## 11. Performance

### 11.1 Optimization
- Lazy load recipe images
- Virtual scrolling for long lists
- Debounced search
- Cached recipe data
- Progressive image loading

### 11.2 Offline Support
- Cache favorite recipes
- Offline cooking mode
- Service worker for images
- Sync when online

## 12. Analytics Events

- `recipe_viewed`
- `recipe_created`
- `recipe_favorited`
- `recipe_prepared`
- `recipe_search`
- `cooking_mode_started`
- `servings_adjusted`
- `nutrition_calculated`

## 13. Testing Requirements

### 13.1 Unit Tests
- Component rendering
- Form validation
- Servings calculations
- Filter logic

### 13.2 Integration Tests
- Recipe CRUD operations
- Search functionality
- Image upload
- Nutrition calculation

### 13.3 E2E Tests
- Create recipe flow
- Search and filter recipes
- Cooking mode workflow
- Favorite management
