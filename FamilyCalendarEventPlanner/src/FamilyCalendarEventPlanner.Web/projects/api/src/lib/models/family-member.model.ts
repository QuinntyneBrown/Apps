import { MemberRole, RelationType } from './enums';

export interface FamilyMember {
  memberId: string;
  familyId: string;
  householdId: string | null;
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
