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

export interface RecurrencePattern {
  frequency: RecurrenceFrequency;
  interval: number;
  endDate: string | null;
  daysOfWeek: string[];
}

export interface CalendarEvent {
  eventId: string;
  familyId: string;
  creatorId: string;
  title: string;
  description: string;
  startTime: string;
  endTime: string;
  location: string;
  eventType: EventType;
  recurrencePattern: RecurrencePattern;
  status: EventStatus;
}

export interface CreateEventRequest {
  familyId: string;
  creatorId: string;
  title: string;
  description?: string;
  startTime: string;
  endTime: string;
  location?: string;
  eventType: EventType;
  recurrencePattern?: RecurrencePattern;
}

export interface UpdateEventRequest {
  eventId: string;
  title?: string;
  description?: string;
  startTime?: string;
  endTime?: string;
  location?: string;
  eventType?: EventType;
  recurrencePattern?: RecurrencePattern;
}

export interface FamilyMember {
  memberId: string;
  familyId: string;
  name: string;
  email: string | null;
  color: string;
  role: MemberRole;
  isImmediate: boolean;
  relationType: RelationType;
}

export interface CreateFamilyMemberRequest {
  familyId: string;
  name: string;
  email?: string | null;
  color: string;
  role: MemberRole;
  isImmediate: boolean;
  relationType: RelationType;
}

export interface UpdateFamilyMemberRequest {
  memberId: string;
  name?: string;
  email?: string | null;
  color?: string;
  isImmediate?: boolean;
  relationType?: RelationType;
}

export interface ChangeMemberRoleRequest {
  memberId: string;
  role: MemberRole;
}

export interface EventAttendee {
  attendeeId: string;
  eventId: string;
  familyMemberId: string;
  rsvpStatus: RSVPStatus;
  responseTime: string | null;
  notes: string;
}

export interface AddAttendeeRequest {
  eventId: string;
  familyMemberId: string;
  notes?: string;
}

export interface RespondToEventRequest {
  attendeeId: string;
  rsvpStatus: RSVPStatus;
  notes?: string;
}

export interface AvailabilityBlock {
  blockId: string;
  memberId: string;
  startTime: string;
  endTime: string;
  blockType: BlockType;
  reason: string;
}

export interface CreateAvailabilityBlockRequest {
  memberId: string;
  startTime: string;
  endTime: string;
  blockType: BlockType;
  reason?: string;
}

export interface ScheduleConflict {
  conflictId: string;
  conflictingEventIds: string[];
  affectedMemberIds: string[];
  conflictSeverity: ConflictSeverity;
  isResolved: boolean;
  resolvedAt: string | null;
}

export interface CreateConflictRequest {
  conflictingEventIds: string[];
  affectedMemberIds: string[];
  conflictSeverity: ConflictSeverity;
}

export interface EventReminder {
  reminderId: string;
  eventId: string;
  recipientId: string;
  reminderTime: string;
  deliveryChannel: NotificationChannel;
  sent: boolean;
}

export interface CreateReminderRequest {
  eventId: string;
  recipientId: string;
  reminderTime: string;
  deliveryChannel: NotificationChannel;
}

export interface RescheduleReminderRequest {
  reminderId: string;
  reminderTime: string;
}
