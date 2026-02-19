import { TopicCategory } from './topic-category.enum';

export interface Topic {
  topicId: string;
  meetingId?: string;
  userId: string;
  title: string;
  description?: string;
  category: TopicCategory;
  discussionNotes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateTopic {
  meetingId?: string;
  userId: string;
  title: string;
  description?: string;
  category: TopicCategory;
  discussionNotes?: string;
}

export interface UpdateTopic {
  topicId: string;
  title: string;
  description?: string;
  category: TopicCategory;
  discussionNotes?: string;
}
