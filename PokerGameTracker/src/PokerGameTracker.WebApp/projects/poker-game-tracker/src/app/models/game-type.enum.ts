export enum GameType {
  TexasHoldem = 0,
  OmahaHoldem = 1,
  SevenCardStud = 2,
  FiveCardDraw = 3,
  Tournament = 4,
  CashGame = 5,
  Other = 6
}

export const gameTypeLabels: Record<GameType, string> = {
  [GameType.TexasHoldem]: 'Texas Hold\'em',
  [GameType.OmahaHoldem]: 'Omaha Hold\'em',
  [GameType.SevenCardStud]: 'Seven Card Stud',
  [GameType.FiveCardDraw]: 'Five Card Draw',
  [GameType.Tournament]: 'Tournament',
  [GameType.CashGame]: 'Cash Game',
  [GameType.Other]: 'Other'
};
