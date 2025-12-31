export enum AuditType {
  Manual = 0,
  Automated = 1,
  PasswordStrength = 2,
  TwoFactorCheck = 3,
  BreachDetection = 4,
  Compliance = 5
}

export const AuditTypeLabels: Record<AuditType, string> = {
  [AuditType.Manual]: 'Manual',
  [AuditType.Automated]: 'Automated',
  [AuditType.PasswordStrength]: 'Password Strength',
  [AuditType.TwoFactorCheck]: 'Two-Factor Check',
  [AuditType.BreachDetection]: 'Breach Detection',
  [AuditType.Compliance]: 'Compliance'
};
