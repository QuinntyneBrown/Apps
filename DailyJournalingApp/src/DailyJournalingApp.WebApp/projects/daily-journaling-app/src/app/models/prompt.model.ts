export interface Prompt {
  promptId: string;
  text: string;
  category?: string;
  isSystemPrompt: boolean;
  createdByUserId?: string;
  createdAt: string;
}

export interface CreatePrompt {
  text: string;
  category?: string;
  createdByUserId?: string;
}
