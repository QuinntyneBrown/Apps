export enum NoteType {
  Text = 0,
  Concept = 1,
  Reference = 2,
  Meeting = 3,
  Project = 4,
  Literature = 5,
  Daily = 6,
  Permanent = 7,
  Fleeting = 8,
  Other = 9
}

export const NoteTypeLabels: Record<NoteType, string> = {
  [NoteType.Text]: 'Text',
  [NoteType.Concept]: 'Concept',
  [NoteType.Reference]: 'Reference',
  [NoteType.Meeting]: 'Meeting',
  [NoteType.Project]: 'Project',
  [NoteType.Literature]: 'Literature',
  [NoteType.Daily]: 'Daily',
  [NoteType.Permanent]: 'Permanent',
  [NoteType.Fleeting]: 'Fleeting',
  [NoteType.Other]: 'Other'
};
