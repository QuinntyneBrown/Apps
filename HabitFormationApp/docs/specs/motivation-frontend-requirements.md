# Motivation - Frontend Requirements

## UI Components

### 1. Achievements Gallery
- Grid of achievement badges
- Locked vs unlocked states
- Progress bars for locked achievements
- Celebration animations on unlock
- Filter by category
- Total points display

### 2. Motivation Dashboard
- Motivation score gauge (0-100)
- Trend graph (improving/declining)
- Recent achievements
- Personal bests
- Suggested actions
- Motivational quotes

### 3. Achievement Unlock Celebration
- Full-screen modal
- Confetti animation
- Badge display with shine effect
- Points earned
- Share buttons
- "Awesome!" sound effect

### 4. Personal Bests Display
- List of records
- Comparison with previous best
- Visual indicators (🏆 for new)
- Category grouping
- Timeline view

### 5. Motivation Dip Intervention
- Gentle encouragement message
- Past achievement reminder
- Suggested easier habits
- Quick win challenges
- Partner notification option

## State Management
```typescript
interface MotivationState {
  achievements: Achievement[];
  motivationScore: number;
  trend: 'improving' | 'stable' | 'declining';
  personalBests: PersonalBest[];
  totalPoints: number;
  recentUnlocks: Achievement[];
}
```

## Animations
- Badge unlock shine effect
- Confetti on achievements
- Score gauge fill animation
- Trophy bounce on personal best
- Sparkles on milestones

## Gamification UI
- Progress bars everywhere
- Point animations (+10, +50)
- Level-up celebrations
- Streak multipliers
- Daily challenges widget
