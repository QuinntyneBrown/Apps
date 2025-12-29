# Activity Management - Frontend Requirements

## Components
- Enrollment form with child selector
- Activity list by child
- Status badges (Active, Waitlist, Completed)
- Season summary cards

## State Management
```typescript
interface ActivityState {
  enrollments: Enrollment[];
  children: Child[];
  selectedChild: Child | null;
}
```
