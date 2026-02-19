// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

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
  householdId: string | null;
  name: string;
  email: string | null;
  color: string;
  role: number;
  isImmediate: boolean;
  relationType: RelationType;
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
}
