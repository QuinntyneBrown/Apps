import { RelationType } from './family-member-dto';

export interface CreateFamilyMemberCommand {
  familyId: string;
  name: string;
  email?: string | null;
  color: string;
<<<<<<< Updated upstream
  role: string;
  isImmediate: boolean;
  relationType: RelationType;
=======
  role: number;
>>>>>>> Stashed changes
}
