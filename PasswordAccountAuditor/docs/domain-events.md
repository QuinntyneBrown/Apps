# Domain Events - Password & Account Auditor

## Overview
This application helps users maintain security across their digital accounts by auditing passwords, tracking security status, and monitoring for breaches. Domain events capture account management, security assessments, breach notifications, and the ongoing protection of digital identity.

## Events

### AccountEvents

#### AccountAdded
- **Description**: A new online account has been registered in the auditor
- **Triggered When**: User adds an account to track
- **Key Data**: Account ID, user ID, service name, username/email, account URL, category, creation timestamp
- **Consumers**: Account inventory, security baseline, audit scheduler

#### AccountDetailsUpdated
- **Description**: Information about an account has been modified
- **Triggered When**: User updates account details
- **Key Data**: Account ID, updated fields, previous values, new values, timestamp
- **Consumers**: Account management, change log, data accuracy

#### AccountDeactivated
- **Description**: An account has been marked as closed or inactive
- **Triggered When**: User closes an online account
- **Key Data**: Account ID, deactivation date, deactivation method, data deletion status, timestamp
- **Consumers**: Account cleanup, security surface reduction, deletion tracking

#### AccountReactivated
- **Description**: A previously inactive account has been reopened
- **Triggered When**: User resumes use of dormant account
- **Key Data**: Account ID, reactivation date, inactivity duration, timestamp
- **Consumers**: Active account tracker, security re-audit trigger

### PasswordEvents

#### PasswordStrengthAssessed
- **Description**: Password security strength has been evaluated
- **Triggered When**: User adds or updates password
- **Key Data**: Assessment ID, account ID, strength score, weakness identified, improvement suggestions, timestamp
- **Consumers**: Security scoring, recommendation engine, weak password alerts

#### WeakPasswordDetected
- **Description**: An account with inadequate password has been identified
- **Triggered When**: Password fails strength criteria
- **Key Data**: Account ID, weakness type, risk level, improvement urgency, timestamp
- **Consumers**: Security alert system, remediation queue, risk management

#### PasswordReused
- **Description**: Same password used across multiple accounts has been detected
- **Triggered When**: Duplicate password identified
- **Key Data**: Password hash, account IDs using it, risk multiplier, timestamp
- **Consumers**: Reuse alert, unique password recommendation, security risk assessment

#### PasswordChanged
- **Description**: An account password has been updated
- **Triggered When**: User changes password for an account
- **Key Data**: Account ID, change date, strength improvement, change reason, timestamp
- **Consumers**: Security improvement tracker, password age reset, audit log

#### PasswordAgeExceeded
- **Description**: A password has exceeded recommended age
- **Triggered When**: Password older than security policy threshold
- **Key Data**: Account ID, password age, last changed date, urgency level, timestamp
- **Consumers**: Password rotation reminder, security policy enforcement

### SecurityFeatureEvents

#### TwoFactorAuthEnabled
- **Description**: Two-factor authentication has been activated for an account
- **Triggered When**: User enables 2FA on a service
- **Key Data**: Account ID, 2FA type (SMS/app/hardware), setup date, timestamp
- **Consumers**: Security posture improvement, protection level tracker

#### TwoFactorAuthDisabled
- **Description**: Two-factor authentication has been deactivated
- **Triggered When**: User turns off 2FA
- **Key Data**: Account ID, deactivation date, deactivation reason, risk increase, timestamp
- **Consumers**: Security degradation alert, risk assessment update

#### BiometricAuthAdded
- **Description**: Biometric authentication has been configured
- **Triggered When**: User sets up fingerprint/face recognition
- **Key Data**: Account ID, biometric type, setup date, timestamp
- **Consumers**: Security enhancement tracker, authentication method inventory

#### BackupCodesGenerated
- **Description**: Account recovery backup codes have been created
- **Triggered When**: User generates backup authentication codes
- **Key Data**: Account ID, number of codes, generation date, storage location, timestamp
- **Consumers**: Recovery preparedness, backup code management

### AuditEvents

#### SecurityAuditPerformed
- **Description**: Comprehensive security audit has been conducted
- **Triggered When**: User or system runs full security assessment
- **Key Data**: Audit ID, accounts audited, issues found, security score, timestamp
- **Consumers**: Security dashboard, issue prioritization, improvement tracking

#### ComplianceCheckCompleted
- **Description**: Account compliance with security best practices verified
- **Triggered When**: Account checked against security standards
- **Key Data**: Check ID, account ID, compliance items, pass/fail status, timestamp
- **Consumers**: Compliance reporting, gap identification, remediation planning

#### VulnerabilityIdentified
- **Description**: A security weakness has been discovered
- **Triggered When**: Audit reveals security gap
- **Key Data**: Vulnerability ID, account ID, vulnerability type, severity, remediation steps, timestamp
- **Consumers**: Vulnerability management, security team notification, fix tracking

#### SecurityScoreCalculated
- **Description**: Overall security posture score has been computed
- **Triggered When**: Periodic or on-demand security score calculation
- **Key Data**: Score ID, overall score, category scores, trend, timestamp
- **Consumers**: Security dashboard, progress tracking, benchmark comparison

### BreachEvents

#### BreachDetected
- **Description**: An account has been involved in a data breach
- **Triggered When**: Breach database indicates account compromise
- **Key Data**: Breach ID, account ID, breach source, data exposed, breach date, discovery date, timestamp
- **Consumers**: Urgent alert system, immediate action queue, breach response

#### BreachNotificationSent
- **Description**: User has been alerted to account breach
- **Triggered When**: Breach detection triggers notification
- **Key Data**: Notification ID, breach ID, account ID, notification channel, urgency, timestamp
- **Consumers**: User communication, action tracking, notification log

#### BreachResponseCompleted
- **Description**: User has taken action in response to breach
- **Triggered When**: User completes breach remediation steps
- **Key Data**: Response ID, breach ID, actions taken, password changed, account secured, timestamp
- **Consumers**: Breach resolution tracker, security restoration, incident closure

#### BreachMonitoringEnabled
- **Description**: Ongoing breach monitoring has been activated for account
- **Triggered When**: User subscribes to breach alerts
- **Key Data**: Monitor ID, account ID, monitoring service, alert preferences, timestamp
- **Consumers**: Continuous monitoring service, alert delivery, proactive protection

### RemediationEvents

#### SecurityIssueReported
- **Description**: A security problem requiring attention has been flagged
- **Triggered When**: System or user identifies security concern
- **Key Data**: Issue ID, account ID, issue type, severity, recommended action, timestamp
- **Consumers**: Remediation queue, priority sorting, action assignment

#### RemediationActionCompleted
- **Description**: A security issue has been addressed
- **Triggered When**: User completes recommended security improvement
- **Key Data**: Action ID, issue ID, account ID, action taken, verification, timestamp
- **Consumers**: Issue resolution tracker, security improvement metrics

#### RemediationPostponed
- **Description**: Security action has been deferred by user
- **Triggered When**: User chooses to delay security improvement
- **Key Data**: Action ID, issue ID, postpone reason, snooze duration, risk acceptance, timestamp
- **Consumers**: Follow-up scheduler, risk tracking, decision log

### RecoveryEvents

#### RecoveryInfoUpdated
- **Description**: Account recovery information has been configured
- **Triggered When**: User sets or updates recovery email/phone
- **Key Data**: Account ID, recovery method, contact details, verification status, timestamp
- **Consumers**: Recovery preparedness, account accessibility, security questions

#### RecoveryMethodTested
- **Description**: Account recovery process has been verified
- **Triggered When**: User tests if they can recover account
- **Key Data**: Test ID, account ID, recovery method, success status, timestamp
- **Consumers**: Recovery assurance, access verification, preparedness confirmation

#### AccountRecoveryInitiated
- **Description**: User has begun process to regain access to account
- **Triggered When**: Recovery procedure is started
- **Key Data**: Recovery ID, account ID, recovery method used, initiation timestamp
- **Consumers**: Recovery tracking, support escalation, access restoration

### CategoryEvents

#### AccountCategorized
- **Description**: An account has been assigned to a category
- **Triggered When**: User organizes accounts by type
- **Key Data**: Account ID, category (financial/social/work/shopping), priority tier, timestamp
- **Consumers**: Organization system, priority-based auditing, focused management

#### CriticalAccountDesignated
- **Description**: An account has been marked as high-priority
- **Triggered When**: User identifies especially important account
- **Key Data**: Account ID, criticality level, importance reason, enhanced monitoring, timestamp
- **Consumers**: Priority monitoring, enhanced security requirements, critical alerts

### MonitoringEvents

#### LoginActivityDetected
- **Description**: Suspicious or unusual login activity has been noticed
- **Triggered When**: Abnormal access pattern identified
- **Key Data**: Activity ID, account ID, login location, device, anomaly type, timestamp
- **Consumers**: Security alert, unauthorized access detection, user verification

#### PermissionsAudited
- **Description**: Third-party app permissions have been reviewed
- **Triggered When**: User audits what apps have access to account
- **Key Data**: Audit ID, account ID, connected apps, permissions granted, risk assessment, timestamp
- **Consumers**: Privacy management, permission cleanup, access control

#### UnusedAccountIdentified
- **Description**: An account with no recent activity has been detected
- **Triggered When**: Account exceeds inactivity threshold
- **Key Data**: Account ID, last login date, inactivity duration, closure recommendation, timestamp
- **Consumers**: Account cleanup suggestion, security surface reduction, resource optimization

### ReportingEvents

#### SecurityReportGenerated
- **Description**: Comprehensive security status report has been created
- **Triggered When**: Periodic or on-demand report generation
- **Key Data**: Report ID, report period, accounts covered, key findings, recommendations, timestamp
- **Consumers**: Executive summary, trend analysis, strategic planning

#### ImprovementTrendDetected
- **Description**: Positive security posture trend has been identified
- **Triggered When**: Analysis shows consistent security improvements
- **Key Data**: Trend ID, improvement areas, percentage increase, timeframe, timestamp
- **Consumers**: Success celebration, motivation system, progress validation
