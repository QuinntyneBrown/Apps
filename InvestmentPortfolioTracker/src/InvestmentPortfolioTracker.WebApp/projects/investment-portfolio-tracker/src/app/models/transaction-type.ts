export enum TransactionType {
  Buy = 0,
  Sell = 1,
  Dividend = 2,
  Interest = 3,
  Deposit = 4,
  Withdrawal = 5,
  Transfer = 6,
  Fee = 7
}

export const TRANSACTION_TYPE_LABELS: Record<TransactionType, string> = {
  [TransactionType.Buy]: 'Buy',
  [TransactionType.Sell]: 'Sell',
  [TransactionType.Dividend]: 'Dividend',
  [TransactionType.Interest]: 'Interest',
  [TransactionType.Deposit]: 'Deposit',
  [TransactionType.Withdrawal]: 'Withdrawal',
  [TransactionType.Transfer]: 'Transfer',
  [TransactionType.Fee]: 'Fee'
};
