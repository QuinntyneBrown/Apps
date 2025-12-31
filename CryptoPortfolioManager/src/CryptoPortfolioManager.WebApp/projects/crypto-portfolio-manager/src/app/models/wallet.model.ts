export interface Wallet {
  walletId: string;
  name: string;
  address?: string;
  walletType: string;
  isActive: boolean;
  notes?: string;
  totalValue: number;
}

export interface CreateWalletRequest {
  name: string;
  address?: string;
  walletType: string;
  notes?: string;
}

export interface UpdateWalletRequest {
  walletId: string;
  name: string;
  address?: string;
  walletType: string;
  notes?: string;
}
