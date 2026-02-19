export interface Resource {
  resourceId: string;
  groupId: string;
  sharedByUserId: string;
  title: string;
  description?: string;
  url?: string;
  resourceType?: string;
  createdAt: string;
}

export interface CreateResource {
  groupId: string;
  sharedByUserId: string;
  title: string;
  description?: string;
  url?: string;
  resourceType?: string;
}

export interface UpdateResource {
  resourceId: string;
  title: string;
  description?: string;
  url?: string;
  resourceType?: string;
}
