import { ContentType } from './content-type';

export interface ViewingRecord {
  viewingRecordId: string;
  title: string;
  contentType: ContentType;
  watchDate: Date;
  platform: string;
  rating?: number;
  isRewatch: boolean;
  runtime: number;
}
