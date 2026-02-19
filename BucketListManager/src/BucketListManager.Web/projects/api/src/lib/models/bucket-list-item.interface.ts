import { Category } from './category.enum';
import { Priority } from './priority.enum';
import { ItemStatus } from './item-status.enum';

export interface BucketListItem {
  bucketListItemId: string;
  userId: string;
  title: string;
  description: string;
  category: Category;
  priority: Priority;
  status: ItemStatus;
  targetDate?: string;
  completedDate?: string;
  notes?: string;
  createdAt: string;
}

export interface CreateBucketListItem {
  userId: string;
  title: string;
  description: string;
  category: Category;
  priority: Priority;
  targetDate?: string;
  notes?: string;
}

export interface UpdateBucketListItem {
  bucketListItemId: string;
  title: string;
  description: string;
  category: Category;
  priority: Priority;
  status: ItemStatus;
  targetDate?: string;
  completedDate?: string;
  notes?: string;
}
