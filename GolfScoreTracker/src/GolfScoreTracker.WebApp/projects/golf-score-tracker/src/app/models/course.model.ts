export interface Course {
  courseId: string;
  name: string;
  location?: string;
  numberOfHoles: number;
  totalPar: number;
  courseRating?: number;
  slopeRating?: number;
  notes?: string;
  createdAt: Date;
}

export interface CreateCourseCommand {
  name: string;
  location?: string;
  numberOfHoles: number;
  totalPar: number;
  courseRating?: number;
  slopeRating?: number;
  notes?: string;
}

export interface UpdateCourseCommand {
  courseId: string;
  name: string;
  location?: string;
  numberOfHoles: number;
  totalPar: number;
  courseRating?: number;
  slopeRating?: number;
  notes?: string;
}
