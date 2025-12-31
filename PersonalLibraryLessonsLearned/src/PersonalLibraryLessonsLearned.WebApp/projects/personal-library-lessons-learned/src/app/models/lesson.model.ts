import { LessonCategory } from './lesson-category.enum';

export interface Lesson {
  lessonId: string;
  userId: string;
  sourceId?: string;
  title: string;
  content: string;
  category: LessonCategory;
  tags?: string;
  dateLearned: string;
  application?: string;
  isApplied: boolean;
  createdAt: string;
}

export interface CreateLesson {
  userId: string;
  sourceId?: string;
  title: string;
  content: string;
  category: LessonCategory;
  tags?: string;
  dateLearned: string;
  application?: string;
}

export interface UpdateLesson {
  lessonId: string;
  sourceId?: string;
  title: string;
  content: string;
  category: LessonCategory;
  tags?: string;
  dateLearned: string;
  application?: string;
  isApplied: boolean;
}
