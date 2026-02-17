import { Category } from './category';
import { Depth } from './depth';

export interface Prompt {
  promptId: string;
  userId?: string;
  text: string;
  category: Category;
  depth: Depth;
  tags?: string;
  isSystemPrompt: boolean;
  usageCount: number;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreatePromptRequest {
  userId: string;
  text: string;
  category: Category;
  depth: Depth;
  tags?: string;
}

export interface UpdatePromptRequest {
  promptId: string;
  text: string;
  category: Category;
  depth: Depth;
  tags?: string;
}
