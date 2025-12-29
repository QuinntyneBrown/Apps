# Frontend Requirements - Recipe Management

## Overview
User interface components and workflows for recipe creation, browsing, editing, rating, and favoriting with rich search and filtering capabilities.

## Pages/Views

### 1. Recipe Library Page (`/recipes`)

**Purpose**: Browse, search, and filter recipe collection

**Components**:
- RecipeLibraryHeader: Search bar, filter toggles, view mode switcher (grid/list)
- RecipeFilterSidebar: Cuisine, difficulty, time, dietary tags, ingredient filters
- RecipeGrid/RecipeList: Recipe cards with photo, name, rating, time, difficulty
- RecipePagination: Page navigation for large collections
- QuickAddButton: Floating action button to add new recipe

**Features**:
- Real-time search with debouncing (300ms)
- Multi-select filters with chip display
- Toggle favorites-only view
- Sort options: Relevance, Rating, Date Added, Cook Time
- Grid view (2-4 columns responsive) or list view
- Infinite scroll or pagination
- Empty state for no results with suggestions

**State Management**:
- Search query
- Active filters (cuisine, difficulty, time ranges, dietary tags)
- Sort order
- View mode (grid/list)
- Current page
- Total recipe count
- Selected recipes (for bulk actions)

**API Calls**:
- GET /api/recipes (with query parameters)
- GET /api/recipes/popular
- GET /api/recipes/favorites

### 2. Recipe Detail Page (`/recipes/:id`)

**Purpose**: Display full recipe information with photos, ingredients, instructions, nutrition, and ratings

**Components**:
- RecipeHeader: Name, cuisine, difficulty badges, rating stars, favorite button
- RecipePhotoGallery: Main photo with thumbnail carousel
- RecipeMetadata: Prep time, cook time, total time, servings adjuster, source
- IngredientsList: Ingredients with quantities, checkboxes for shopping list
- InstructionSteps: Numbered steps with optional photos, timer triggers
- NutritionPanel: Calories, macros, expandable detailed nutrition
- RatingsSummary: Average rating, rating distribution chart, review count
- RatingsAndReviews: Paginated user reviews with filtering
- RelatedRecipes: Similar recipes based on cuisine/ingredients
- ActionButtons: Edit, Delete, Print, Share, Add to Meal Plan

**Features**:
- Servings adjuster (auto-scales ingredient quantities)
- Add all/selected ingredients to shopping list
- Print-friendly recipe card view
- Share recipe via link, email, social media
- Photo zoom/lightbox for recipe images
- Ingredient substitution suggestions
- Nutrition per serving calculation
- Mark recipe as cooked (triggers rating prompt)
- Version history viewer (for recipe owners)

**State Management**:
- Recipe data (loaded from API)
- Servings multiplier
- Selected ingredients for shopping list
- Current photo index
- Ratings page number
- User's rating for this recipe
- Favorite status

**API Calls**:
- GET /api/recipes/{id}
- POST /api/recipes/{id}/favorite
- DELETE /api/recipes/{id}/favorite
- GET /api/recipes/{id}/ratings
- POST /api/recipes/{id}/ratings
- GET /api/recipes/{id}/versions

### 3. Add/Edit Recipe Page (`/recipes/new`, `/recipes/:id/edit`)

**Purpose**: Create new recipes or edit existing ones

**Components**:
- RecipeForm: Multi-step or single-page form
- RecipeBasicInfo: Name, cuisine, difficulty, source, servings
- RecipeTimingInfo: Prep time, cook time (with hour/minute inputs)
- RecipePhotoUploader: Drag-and-drop photo upload with preview
- IngredientsBuilder: Dynamic list with add/remove, quantity/unit selectors
- InstructionsBuilder: Dynamic ordered list with rich text editor
- DietaryTagsSelector: Multi-select checkboxes for dietary labels
- NutritionCalculator: Auto-calculate or manual entry
- RecipeFormActions: Save Draft, Publish, Cancel

**Features**:
- Form validation with inline error messages
- Auto-save to local storage every 30 seconds
- Photo upload with preview, crop, and reorder
- Ingredient autocomplete from known ingredients
- Drag-to-reorder ingredients and instructions
- Import recipe from URL (parse and pre-fill form)
- Rich text editor for instruction steps
- Template selection for common recipe types
- Dietary tags auto-detection from ingredients
- Save as draft vs. publish

**State Management**:
- Form data (recipe object)
- Validation errors
- Upload progress for photos
- Dirty state (unsaved changes)
- Ingredient autocomplete suggestions
- Preview mode toggle

**API Calls**:
- POST /api/recipes (create)
- PUT /api/recipes/{id} (update)
- POST /api/recipes/import (import from URL)
- GET /api/ingredients/autocomplete

**Validation Rules**:
- Name: Required, 3-200 characters
- Cuisine: Required selection
- Ingredients: At least 1 required
- Instructions: At least 1 required
- Times: Positive numbers
- Servings: At least 1
- Photos: Max 10, each <5MB, valid image format

### 4. Recipe Rating Modal/Page

**Purpose**: Rate and review a recipe after cooking

**Components**:
- RatingForm: Star rating selector, review textarea
- CookingDatePicker: When did you make this?
- DifficultyFeedback: Was it easier/harder than expected?
- WouldMakeAgainToggle: Binary choice
- PhotoUpload: Optional photos of user's cooked dish
- SubmitButton: Post rating

**Features**:
- Visual star rating (1-5 with half stars)
- Character count for review (max 2000)
- Photo upload for user's version
- Optional fields for quick submission
- Edit existing rating if already submitted
- Preview before posting

**State Management**:
- Rating score
- Review text
- Cooking date
- Difficulty experienced
- Would make again flag
- Uploaded photos
- Submission status

**API Calls**:
- POST /api/recipes/{id}/ratings
- PUT /api/recipes/{id}/ratings/{ratingId}

### 5. Favorites Collection Page (`/recipes/favorites`)

**Purpose**: View and organize favorited recipes

**Components**:
- FavoritesHeader: Category filter, search
- FavoriteCategoryTabs: All, Quick Weeknight, Special Occasion, etc.
- FavoriteRecipeGrid: Recipe cards with unfavorite button
- FavoriteEmptyState: Prompt to add favorites

**Features**:
- Filter favorites by custom categories
- Remove from favorites with confirmation
- Edit favorite category
- Sort by date favorited, rating, name
- Quick add to meal plan from favorites

**State Management**:
- Favorite recipes
- Active category filter
- Sort order

**API Calls**:
- GET /api/recipes/favorites

## Shared Components

### RecipeCard
**Purpose**: Reusable recipe preview card

**Props**:
- recipe: RecipeDto
- viewMode: 'grid' | 'list'
- showActions: boolean
- onFavorite: function
- onAddToMealPlan: function

**Elements**:
- Recipe photo with fallback placeholder
- Recipe name (truncated if long)
- Star rating display
- Quick metadata (time, difficulty)
- Favorite heart icon (filled if favorited)
- Hover actions overlay (View, Add to Plan, Favorite)

### IngredientListItem
**Purpose**: Display single ingredient with quantity

**Props**:
- ingredient: RecipeIngredient
- servingsMultiplier: number
- selectable: boolean
- onSelect: function

**Elements**:
- Checkbox (if selectable)
- Quantity (adjusted by servings)
- Unit
- Ingredient name
- Optional notes

### RatingStars
**Purpose**: Display or input star rating

**Props**:
- rating: number (0-5)
- editable: boolean
- size: 'small' | 'medium' | 'large'
- onChange: function

**Elements**:
- 5 star icons (filled, half-filled, or empty)
- Click/hover interaction if editable
- Aria label for accessibility

## User Workflows

### Create Recipe from Scratch
1. Click "Add Recipe" button
2. Fill out basic info (name, cuisine, difficulty)
3. Add ingredients one by one with quantities
4. Add instruction steps
5. Upload photos (optional)
6. Select dietary tags
7. Review and save
8. View newly created recipe

### Import Recipe from URL
1. Click "Import Recipe"
2. Paste recipe URL
3. System parses and extracts data
4. Review pre-filled form
5. Edit/correct any extracted data
6. Save recipe
7. View imported recipe

### Find Recipe to Cook
1. Browse recipe library or search
2. Apply filters (time, difficulty, dietary)
3. View recipe details
4. Check ingredients against pantry
5. Add to meal plan or add ingredients to shopping list
6. Cook recipe
7. Rate and review

### Rate Recipe After Cooking
1. Mark meal as completed in meal plan OR click "I made this" on recipe
2. Rating modal appears
3. Select star rating
4. Write optional review
5. Answer quick questions (difficulty, would make again)
6. Submit rating
7. Rating appears on recipe page

### Organize Favorites
1. Browse recipes
2. Click heart icon to favorite
3. Select category for favorite (optional)
4. View all favorites in favorites page
5. Filter by category
6. Remove from favorites if needed

## Form Validation & Error Handling

### Client-Side Validation
- Real-time validation on blur for each field
- Inline error messages below inputs
- Disabled submit button until form valid
- Required field indicators (*)
- Character count for text fields
- File size/type validation for photos

### Server-Side Error Handling
- Display API error messages in toast notifications
- Form-level errors displayed at top of form
- Field-level errors mapped to specific inputs
- Retry logic for network failures
- Unsaved changes warning on navigation

## Performance Optimization

### Image Optimization
- Lazy load recipe photos below the fold
- Use responsive images (srcset) for different sizes
- Thumbnail generation for list/grid views
- Blur-up technique while loading
- WebP format with JPEG fallback

### Data Loading
- Cache recipe list data for 5 minutes
- Prefetch recipe details on card hover
- Infinite scroll with intersection observer
- Debounce search input (300ms)
- Virtual scrolling for large recipe lists

### Code Splitting
- Lazy load recipe editor only when needed
- Separate bundle for photo upload components
- Dynamic import for recipe import feature

## Responsive Design

### Mobile (<768px)
- Single column recipe grid
- Simplified filter drawer (slide-in)
- Collapsible ingredient/instruction sections
- Sticky header with back button
- Bottom sheet for actions

### Tablet (768-1024px)
- 2-column recipe grid
- Side-by-side filter sidebar
- Recipe detail in modal or full page

### Desktop (>1024px)
- 3-4 column recipe grid
- Persistent filter sidebar
- Full recipe detail layout
- Keyboard shortcuts for power users

## Accessibility

### WCAG 2.1 AA Compliance
- Semantic HTML (main, nav, article)
- ARIA labels for interactive elements
- Keyboard navigation support
- Focus visible indicators
- Alt text for all recipe images
- Color contrast ratio >4.5:1
- Screen reader announcements for state changes

### Keyboard Shortcuts
- `/` - Focus search
- `n` - New recipe
- `Esc` - Close modals
- Arrow keys - Navigate recipe cards
- `Enter` - Open selected recipe

## State Management Architecture

### Redux Slices (if using Redux)
- `recipesSlice`: Recipe list, filters, pagination
- `recipeDetailSlice`: Selected recipe data
- `favoritesSlice`: User favorites
- `ratingsSlice`: Recipe ratings and user reviews
- `formSlice`: Recipe form state and validation

### React Query Keys
- `['recipes', filters]` - Recipe list with filters
- `['recipe', id]` - Single recipe detail
- `['recipe-ratings', id]` - Recipe ratings
- `['favorites']` - User favorites
- `['popular-recipes']` - Popular recipes

## Error States

### Empty States
- No recipes found: Suggest adjusting filters or adding recipes
- No favorites: Prompt to browse recipes and add favorites
- No ratings: "Be the first to rate this recipe"
- No search results: Suggest similar searches or popular recipes

### Error States
- Failed to load recipes: Retry button
- Failed to save recipe: Error message with retry
- Photo upload failed: Remove and try again
- Network offline: Offline indicator with cached data

## Loading States
- Skeleton loaders for recipe cards
- Spinner for form submission
- Progress bar for photo uploads
- Shimmer effect for recipe detail loading
- Optimistic updates for favorites toggle
