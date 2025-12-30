import { RelationType } from './family-member-dto';

export interface CreateFamilyMemberCommand {
  familyId: string;
  name: string;
  email?: string | null;
  color: string;
  role: string;
  isImmediate: boolean;
  relationType: RelationType;
}
