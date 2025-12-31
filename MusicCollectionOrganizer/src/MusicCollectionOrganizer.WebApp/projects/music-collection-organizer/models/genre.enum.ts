export enum Genre {
  Rock = 0,
  Pop = 1,
  Jazz = 2,
  Classical = 3,
  HipHop = 4,
  Electronic = 5,
  Country = 6,
  Blues = 7,
  Metal = 8,
  Alternative = 9,
  Other = 10
}

export const GenreLabels: Record<Genre, string> = {
  [Genre.Rock]: 'Rock',
  [Genre.Pop]: 'Pop',
  [Genre.Jazz]: 'Jazz',
  [Genre.Classical]: 'Classical',
  [Genre.HipHop]: 'Hip Hop',
  [Genre.Electronic]: 'Electronic',
  [Genre.Country]: 'Country',
  [Genre.Blues]: 'Blues',
  [Genre.Metal]: 'Metal',
  [Genre.Alternative]: 'Alternative',
  [Genre.Other]: 'Other'
};
