import { RelationshipType } from './relationship-type.enum';

export interface Relationship {
  relationshipId: string;
  personId: string;
  relatedPersonId: string;
  relationshipType: RelationshipType;
  createdAt: string;
}

export interface CreateRelationshipRequest {
  personId: string;
  relatedPersonId: string;
  relationshipType: RelationshipType;
}
