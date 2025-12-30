
export interface CreateFamilyMemberCommand {
  familyId: string;
  name: string;
  email?: string | null;
  color: string;
  role: number;
  isImmediate: boolean;
  relationType: number;
}
