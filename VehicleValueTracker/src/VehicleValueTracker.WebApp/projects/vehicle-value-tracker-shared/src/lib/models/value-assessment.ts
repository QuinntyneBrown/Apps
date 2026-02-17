import { ConditionGrade } from './condition-grade';

export interface ValueAssessment {
  valueAssessmentId: string;
  vehicleId: string;
  assessmentDate: string;
  estimatedValue: number;
  mileageAtAssessment: number;
  conditionGrade: ConditionGrade;
  valuationSource?: string;
  exteriorCondition?: string;
  interiorCondition?: string;
  mechanicalCondition?: string;
  modifications: string[];
  knownIssues: string[];
  depreciationAmount?: number;
  depreciationPercentage?: number;
  notes?: string;
}
