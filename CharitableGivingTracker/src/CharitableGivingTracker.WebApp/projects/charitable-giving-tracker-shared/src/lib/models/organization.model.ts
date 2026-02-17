export interface Organization {
  organizationId: string;
  name: string;
  ein?: string;
  address?: string;
  website?: string;
  is501c3: boolean;
  notes?: string;
  totalDonations: number;
}

export interface CreateOrganizationCommand {
  name: string;
  ein?: string;
  address?: string;
  website?: string;
  is501c3: boolean;
  notes?: string;
}

export interface UpdateOrganizationCommand {
  organizationId: string;
  name: string;
  ein?: string;
  address?: string;
  website?: string;
  is501c3: boolean;
  notes?: string;
}
