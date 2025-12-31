export enum WeatherCondition {
  Sunny = 0,
  PartlyCloudy = 1,
  Cloudy = 2,
  Rainy = 3,
  Stormy = 4,
  Foggy = 5,
  Snowy = 6,
  Windy = 7,
}

export const WeatherConditionLabels: Record<WeatherCondition, string> = {
  [WeatherCondition.Sunny]: 'Sunny',
  [WeatherCondition.PartlyCloudy]: 'Partly Cloudy',
  [WeatherCondition.Cloudy]: 'Cloudy',
  [WeatherCondition.Rainy]: 'Rainy',
  [WeatherCondition.Stormy]: 'Stormy',
  [WeatherCondition.Foggy]: 'Foggy',
  [WeatherCondition.Snowy]: 'Snowy',
  [WeatherCondition.Windy]: 'Windy',
};
