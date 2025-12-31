export enum ContactType {
  Colleague = 0,
  Mentor = 1,
  Mentee = 2,
  Client = 3,
  Recruiter = 4,
  IndustryPeer = 5,
  Referral = 6,
  EventConnection = 7,
  Other = 8,
}

export const ContactTypeLabels: Record<ContactType, string> = {
  [ContactType.Colleague]: 'Colleague',
  [ContactType.Mentor]: 'Mentor',
  [ContactType.Mentee]: 'Mentee',
  [ContactType.Client]: 'Client',
  [ContactType.Recruiter]: 'Recruiter',
  [ContactType.IndustryPeer]: 'Industry Peer',
  [ContactType.Referral]: 'Referral',
  [ContactType.EventConnection]: 'Event Connection',
  [ContactType.Other]: 'Other',
};
