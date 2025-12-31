export interface Story {
  storyId: string;
  personId: string;
  title: string;
  content?: string;
  createdAt: string;
}

export interface CreateStoryRequest {
  personId: string;
  title: string;
  content?: string;
}

export interface UpdateStoryRequest {
  storyId?: string;
  title: string;
  content?: string;
}
