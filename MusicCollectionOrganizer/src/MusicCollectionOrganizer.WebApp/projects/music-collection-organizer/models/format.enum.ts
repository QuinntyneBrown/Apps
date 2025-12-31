export enum Format {
  CD = 0,
  Vinyl = 1,
  Cassette = 2,
  Digital = 3,
  StreamingOnly = 4
}

export const FormatLabels: Record<Format, string> = {
  [Format.CD]: 'CD',
  [Format.Vinyl]: 'Vinyl',
  [Format.Cassette]: 'Cassette',
  [Format.Digital]: 'Digital',
  [Format.StreamingOnly]: 'Streaming Only'
};
