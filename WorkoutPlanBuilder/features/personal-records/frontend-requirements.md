# Personal Records - Frontend Requirements

## Overview
Celebrate achievements with an engaging interface showing current PRs, history, and comparisons.

## Pages

### 1. Personal Records Dashboard (`/records`)
- Current PRs by exercise (grid/list view)
- Recent PRs timeline
- PR categories (strength, volume, reps)
- Leaderboard (social feature)
- Share achievements

### 2. Exercise PR Detail (`/records/exercise/{id}`)
- All PR types for exercise
- PR history timeline
- Progress chart
- Comparison to previous PRs
- Session details link
- Share specific PR

### 3. PR Achievement Celebration (Modal)
- Animated celebration when PR detected
- Before/after comparison
- Social sharing options
- Save achievement
- View details

## Components

### PRCard
```typescript
interface PRCardProps {
  record: PersonalRecord;
  exerciseName: string;
  onShare: (id: string) => void;
  onViewDetails: (id: string) => void;
  showCelebration?: boolean;
}
```

### PRTimeline
```typescript
interface PRTimelineProps {
  records: PersonalRecord[];
  exerciseId?: string;
  type?: RecordType;
}
```

### PRCelebrationModal
```typescript
interface PRCelebrationModalProps {
  record: PersonalRecord;
  previousRecord?: PersonalRecord;
  onClose: () => void;
  onShare: () => void;
}
```

## Features
- Real-time PR detection during workouts
- Animated celebrations (confetti, fireworks)
- Social sharing with custom graphics
- PR comparison tools
- Historical trend analysis
- Milestone tracking
- Achievement badges
- PR notifications

## Animations
- Confetti animation on new PR
- Number count-up animation
- Progress bar animations
- Celebration modal entrance
- Share card animations
