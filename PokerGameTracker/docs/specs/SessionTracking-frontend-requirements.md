# Session Tracking - Frontend

## Pages

### Active Session (`/sessions/active`)
- Session timer (elapsed time)
- Current buy-in amount
- Add rebuy button
- Table notes field
- End session button

### Session History (`/sessions`)
- List of all sessions
- Each showing: date, duration, location, profit/loss
- Color-coded P/L (green profit, red loss)
- Filter by date, location, game type
- Summary cards: total profit, session count, win rate

## UI Components
```typescript
interface SessionCard {
  date: Date;
  duration: string;
  location: string;
  stakes: string;
  profitLoss: number;
  gameType: string;
}
```
