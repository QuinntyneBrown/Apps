import { AuditStatus } from './audit-status.enum';
import { AuditType } from './audit-type.enum';

export interface SecurityAudit {
  securityAuditId: string;
  accountId: string;
  auditType: AuditType;
  status: AuditStatus;
  auditDate: string;
  findings?: string;
  recommendations?: string;
  securityScore: number;
  notes?: string;
}

export interface CreateSecurityAudit {
  accountId: string;
  auditType: AuditType;
  findings?: string;
  recommendations?: string;
  securityScore: number;
  notes?: string;
}

export interface UpdateSecurityAudit {
  securityAuditId: string;
  auditType: AuditType;
  status: AuditStatus;
  findings?: string;
  recommendations?: string;
  securityScore: number;
  notes?: string;
}
