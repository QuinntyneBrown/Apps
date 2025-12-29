# Frontend Requirements - Family Preferences

## Pages/Views

### 1. Family Members Page (`/family`)

**Components**:
- MemberList: All household members with avatars
- AddMemberButton: Add new family member
- MemberCard: Name, age, dietary summary
- PreferencesEditor: Edit member details

**Features**:
- Add/edit/remove family members
- Set dietary preferences and restrictions
- Mark severity (preference vs allergy)
- Track favorite recipes per member
- Filter recipes by member
- View member compatibility for recipes

**API Calls**:
- GET /api/household/members
- POST /api/household/members
- PUT /api/household/members/{id}/preferences

### 2. Member Detail View

**Components**:
- MemberProfile: Name, age, photo
- PreferencesList: Likes and dislikes
- RestrictionsList: Allergies and restrictions with severity
- FavoriteRecipes: Member's top-rated recipes
- CompatibleRecipes: Recipes they can eat

**Features**:
- Comprehensive preference management
- Allergen warnings with severity
- Recipe recommendations for member
- Track rating history

## State Management
- `householdSlice`: Family members and preferences
