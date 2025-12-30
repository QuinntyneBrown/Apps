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
  role: string;
  isImmediate: boolean;
  relationType: RelationType;
}
