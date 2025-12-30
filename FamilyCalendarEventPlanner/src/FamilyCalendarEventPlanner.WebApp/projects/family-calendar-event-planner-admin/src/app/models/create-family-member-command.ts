export interface CreateFamilyMemberCommand {
  familyId: string;
  householdId?: string | null;
  name: string;
  email?: string | null;
  color: string;
  role: number;
  isImmediate: boolean;
  relationType: number;
}
