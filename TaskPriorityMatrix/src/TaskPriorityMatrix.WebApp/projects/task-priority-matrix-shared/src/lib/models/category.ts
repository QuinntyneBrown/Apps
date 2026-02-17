export interface Category {
  categoryId: string;
  name: string;
  color: string;
  createdAt: Date;
}

export interface CreateCategoryRequest {
  name: string;
  color: string;
}

export interface UpdateCategoryRequest {
  categoryId: string;
  name: string;
  color: string;
}
