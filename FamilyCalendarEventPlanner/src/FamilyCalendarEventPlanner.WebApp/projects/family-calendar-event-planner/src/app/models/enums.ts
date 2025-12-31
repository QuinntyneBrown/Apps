export enum EventType {
  Appointment = 'Appointment',
  FamilyDinner = 'FamilyDinner',
  Sports = 'Sports',
  School = 'School',
  Vacation = 'Vacation',
  Birthday = 'Birthday',
  Other = 'Other'
}

export enum EventStatus {
  Scheduled = 'Scheduled',
  Completed = 'Completed',
  Cancelled = 'Cancelled'
}

export enum RecurrenceFrequency {
  None = 'None',
  Daily = 'Daily',
  Weekly = 'Weekly',
  Monthly = 'Monthly',
  Yearly = 'Yearly'
}

export enum MemberRole {
  Admin = 'Admin',
  Member = 'Member',
  ViewOnly = 'ViewOnly'
}

export enum RelationType {
  Self = 'Self',
  Spouse = 'Spouse',
  Child = 'Child',
  Parent = 'Parent',
  Sibling = 'Sibling',
  Grandparent = 'Grandparent',
  Grandchild = 'Grandchild',
  AuntUncle = 'AuntUncle',
  NieceNephew = 'NieceNephew',
  Cousin = 'Cousin',
  InLaw = 'InLaw',
  Other = 'Other'
}

export enum RSVPStatus {
  Pending = 'Pending',
  Accepted = 'Accepted',
  Declined = 'Declined',
  Tentative = 'Tentative'
}

export enum BlockType {
  Busy = 'Busy',
  OutOfOffice = 'OutOfOffice',
  Personal = 'Personal'
}

export enum ConflictSeverity {
  Low = 'Low',
  Medium = 'Medium',
  High = 'High',
  Critical = 'Critical'
}

export enum NotificationChannel {
  Email = 'Email',
  Push = 'Push',
  SMS = 'SMS'
}

export enum CanadianProvince {
  Alberta = 'Alberta',
  BritishColumbia = 'BritishColumbia',
  Manitoba = 'Manitoba',
  NewBrunswick = 'NewBrunswick',
  NewfoundlandAndLabrador = 'NewfoundlandAndLabrador',
  NorthwestTerritories = 'NorthwestTerritories',
  NovaScotia = 'NovaScotia',
  Nunavut = 'Nunavut',
  Ontario = 'Ontario',
  PrinceEdwardIsland = 'PrinceEdwardIsland',
  Quebec = 'Quebec',
  Saskatchewan = 'Saskatchewan',
  Yukon = 'Yukon'
}
