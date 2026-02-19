export enum Category {
  Icebreaker = 0,
  Deep = 1,
  Fun = 2,
  Relationship = 3,
  Reflective = 4,
  Hypothetical = 5,
  ValuesAndBeliefs = 6,
  DreamsAndAspirations = 7,
  PastExperiences = 8,
  Other = 9
}

export const CategoryLabels: Record<Category, string> = {
  [Category.Icebreaker]: 'Icebreaker',
  [Category.Deep]: 'Deep',
  [Category.Fun]: 'Fun',
  [Category.Relationship]: 'Relationship',
  [Category.Reflective]: 'Reflective',
  [Category.Hypothetical]: 'Hypothetical',
  [Category.ValuesAndBeliefs]: 'Values & Beliefs',
  [Category.DreamsAndAspirations]: 'Dreams & Aspirations',
  [Category.PastExperiences]: 'Past Experiences',
  [Category.Other]: 'Other'
};
