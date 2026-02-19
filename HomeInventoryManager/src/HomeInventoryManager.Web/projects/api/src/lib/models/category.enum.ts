export enum Category {
  Electronics = 0,
  Furniture = 1,
  Appliances = 2,
  Jewelry = 3,
  Collectibles = 4,
  Tools = 5,
  Clothing = 6,
  Books = 7,
  Sports = 8,
  Other = 9
}

export const CategoryLabels: Record<Category, string> = {
  [Category.Electronics]: 'Electronics',
  [Category.Furniture]: 'Furniture',
  [Category.Appliances]: 'Appliances',
  [Category.Jewelry]: 'Jewelry',
  [Category.Collectibles]: 'Collectibles',
  [Category.Tools]: 'Tools',
  [Category.Clothing]: 'Clothing',
  [Category.Books]: 'Books',
  [Category.Sports]: 'Sports',
  [Category.Other]: 'Other'
};
