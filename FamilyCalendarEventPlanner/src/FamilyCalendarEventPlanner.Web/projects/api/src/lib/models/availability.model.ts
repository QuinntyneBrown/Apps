import { BlockType } from './enums';

export interface AvailabilityBlock {
  blockId: string;
  memberId: string;
  startTime: string;
  endTime: string;
  blockType: BlockType;
  reason: string;
}

export interface CreateAvailabilityBlockRequest {
  memberId: string;
  startTime: string;
  endTime: string;
  blockType: BlockType;
  reason?: string;
}
