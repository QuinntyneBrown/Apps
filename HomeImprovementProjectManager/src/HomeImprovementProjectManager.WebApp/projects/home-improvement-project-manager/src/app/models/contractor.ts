export interface Contractor {
  contractorId: string;
  projectId?: string;
  name: string;
  trade?: string;
  phoneNumber?: string;
  email?: string;
  rating?: number;
  createdAt: string;
}
