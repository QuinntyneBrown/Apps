export interface Message {
  messageId: string;
  senderNeighborId: string;
  recipientNeighborId: string;
  subject: string;
  content: string;
  isRead: boolean;
  readAt?: string;
  createdAt: string;
}

export interface CreateMessage {
  senderNeighborId: string;
  recipientNeighborId: string;
  subject: string;
  content: string;
}

export interface UpdateMessage {
  messageId: string;
  subject: string;
  content: string;
  isRead: boolean;
}
