import { IntroductionStatus } from './introduction-status.enum';

export interface Introduction {
  introductionId: string;
  fromContactId: string;
  toContactId: string;
  purpose: string;
  status: IntroductionStatus;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface RequestIntroductionRequest {
  fromContactId: string;
  toContactId: string;
  purpose: string;
  notes?: string;
}

export interface MakeIntroductionRequest {
  introductionId: string;
  notes?: string;
}
