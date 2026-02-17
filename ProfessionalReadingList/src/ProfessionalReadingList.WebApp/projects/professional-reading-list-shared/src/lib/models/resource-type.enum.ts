export enum ResourceType {
  Book = 0,
  Article = 1,
  ResearchPaper = 2,
  BlogPost = 3,
  Video = 4,
  Podcast = 5,
  Whitepaper = 6,
  Other = 7
}

export const ResourceTypeLabels: Record<ResourceType, string> = {
  [ResourceType.Book]: 'Book',
  [ResourceType.Article]: 'Article',
  [ResourceType.ResearchPaper]: 'Research Paper',
  [ResourceType.BlogPost]: 'Blog Post',
  [ResourceType.Video]: 'Video',
  [ResourceType.Podcast]: 'Podcast',
  [ResourceType.Whitepaper]: 'Whitepaper',
  [ResourceType.Other]: 'Other'
};
