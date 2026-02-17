import { OpportunityType } from './opportunity-type.enum';
import { OpportunityStatus } from './opportunity-status.enum';

export interface Opportunity {
  opportunityId: string;
  contactId: string;
  type: OpportunityType;
  description: string;
  status: OpportunityStatus;
  value?: number;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateOpportunityRequest {
  contactId: string;
  opportunityType: OpportunityType;
  description: string;
  potentialValue?: string;
  status: OpportunityStatus;
  notes?: string;
}

export interface UpdateOpportunityRequest {
  opportunityId: string;
  description: string;
  status: OpportunityStatus;
  potentialValue?: string;
  notes?: string;
}
