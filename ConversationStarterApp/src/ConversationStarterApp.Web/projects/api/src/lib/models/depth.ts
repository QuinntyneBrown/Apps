export enum Depth {
  Surface = 0,
  Moderate = 1,
  Deep = 2,
  Intimate = 3
}

export const DepthLabels: Record<Depth, string> = {
  [Depth.Surface]: 'Surface',
  [Depth.Moderate]: 'Moderate',
  [Depth.Deep]: 'Deep',
  [Depth.Intimate]: 'Intimate'
};
