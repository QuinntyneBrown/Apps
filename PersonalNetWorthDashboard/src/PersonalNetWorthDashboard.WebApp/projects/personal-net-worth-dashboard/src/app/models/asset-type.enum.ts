export enum AssetType {
  Cash = 0,
  Investment = 1,
  Retirement = 2,
  RealEstate = 3,
  Vehicle = 4,
  PersonalProperty = 5,
  Business = 6,
  Other = 7
}

export const AssetTypeLabels: Record<AssetType, string> = {
  [AssetType.Cash]: 'Cash',
  [AssetType.Investment]: 'Investment',
  [AssetType.Retirement]: 'Retirement',
  [AssetType.RealEstate]: 'Real Estate',
  [AssetType.Vehicle]: 'Vehicle',
  [AssetType.PersonalProperty]: 'Personal Property',
  [AssetType.Business]: 'Business',
  [AssetType.Other]: 'Other'
};
