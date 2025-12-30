export type RelationType =
  | 'Self'
  | 'Spouse'
  | 'Child'
  | 'Parent'
  | 'Sibling'
  | 'Grandparent'
  | 'Grandchild'
  | 'AuntUncle'
  | 'NieceNephew'
  | 'Cousin'
  | 'InLaw'
  | 'Other';

export interface FamilyMemberDto {
  memberId: string;
  familyId: string;
  name: string;
  email: string | null;
  color: string;
<<<<<<< Updated upstream
  role: string;
  isImmediate: boolean;
  relationType: RelationType;
=======
  role: number;
}

export enum MemberRole {
  Admin = 0,
  Member = 1,
  ViewOnly = 2
}

export function getRoleLabel(role: number): string {
  switch (role) {
    case MemberRole.Admin:
      return 'Admin';
    case MemberRole.Member:
      return 'Member';
    case MemberRole.ViewOnly:
      return 'View Only';
    default:
      return 'Unknown';
  }
>>>>>>> Stashed changes
}
