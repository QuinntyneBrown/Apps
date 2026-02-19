export interface Neighbor {
  neighborId: string;
  userId: string;
  name: string;
  address?: string;
  contactInfo?: string;
  bio?: string;
  interests?: string;
  isVerified: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateNeighbor {
  userId: string;
  name: string;
  address?: string;
  contactInfo?: string;
  bio?: string;
  interests?: string;
  isVerified: boolean;
}

export interface UpdateNeighbor {
  neighborId: string;
  name: string;
  address?: string;
  contactInfo?: string;
  bio?: string;
  interests?: string;
  isVerified: boolean;
}
