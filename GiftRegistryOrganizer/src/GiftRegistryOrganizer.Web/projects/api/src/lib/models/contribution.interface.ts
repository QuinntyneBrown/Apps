export interface Contribution {
  contributionId: string;
  registryItemId: string;
  contributorName: string;
  contributorEmail?: string;
  quantity: number;
  contributedAt: Date;
}
