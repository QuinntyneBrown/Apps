# Domain Events - Document Vault & Organizer

## Overview
This document defines the domain events tracked in the Document Vault & Organizer application. These events capture significant business occurrences related to secure document storage, organization, encryption, access control, version management, and document lifecycle.

## Events

### DocumentEvents

#### DocumentUploaded
- **Description**: New document has been added to secure vault
- **Triggered When**: User uploads file to document vault
- **Key Data**: Document ID, file name, file type, file size, upload date, uploaded by, category, encryption status, original source
- **Consumers**: Document library, encryption service, search indexer, storage manager, backup service, virus scanner

#### DocumentUpdated
- **Description**: Existing document has been replaced with new version
- **Triggered When**: User uploads newer version of existing document
- **Key Data**: Document ID, version number, update date, updated by, change summary, file size change, previous version archived
- **Consumers**: Version manager, document library, notification service, change log, backup service

#### DocumentDeleted
- **Description**: Document has been removed from vault
- **Triggered When**: User permanently deletes document or moves to trash
- **Key Data**: Document ID, deletion date, deleted by, deletion reason, retention period, permanent deletion date, recoverable flag
- **Consumers**: Trash manager, storage reclaimer, audit log, backup service, retention policy enforcer

#### DocumentRestored
- **Description**: Deleted document has been recovered from trash
- **Triggered When**: User restores document before permanent deletion
- **Key Data**: Document ID, restoration date, restored by, original deletion date, restored to location
- **Consumers**: Document library, category re-assignment, search re-indexer, notification service

### CategoryEvents

#### CategoryCreated
- **Description**: New organizational category has been established
- **Triggered When**: User creates document category or folder
- **Key Data**: Category ID, category name, parent category, description, security level, retention policy, color code
- **Consumers**: Category hierarchy, document organizer, access control, retention policy applicator

#### DocumentCategorized
- **Description**: Document has been assigned to category
- **Triggered When**: User places document into category or folder
- **Key Data**: Document ID, category ID, categorization date, categorized by, previous category, auto-categorized flag
- **Consumers**: Category organizer, search indexer, access controller, document finder

#### CategoryReorganized
- **Description**: Category structure has been modified
- **Triggered When**: User moves or restructures categories
- **Key Data**: Category ID, previous parent, new parent, affected documents count, reorganization date, hierarchy change
- **Consumers**: Hierarchy updater, document path updater, search re-indexer, breadcrumb refresher

### AccessControlEvents

#### DocumentShared
- **Description**: Document access has been granted to user
- **Triggered When**: Owner shares document with specific person
- **Key Data**: Share ID, document ID, shared by, shared with, permission level, share date, expiration date, notification sent
- **Consumers**: Access control, notification service, collaboration tracker, permission manager

#### ShareLinkGenerated
- **Description**: Shareable link has been created for document
- **Triggered When**: User generates public or private sharing link
- **Key Data**: Link ID, document ID, link URL, created by, creation date, expiration date, password protected, download limit, view limit
- **Consumers**: Link manager, access controller, link tracker, expiration monitor

#### ShareLinkAccessed
- **Description**: Document has been accessed via share link
- **Triggered When**: Someone opens document through shared link
- **Key Data**: Link ID, access timestamp, accessor IP, accessor location, document viewed, download occurred, device type
- **Consumers**: Access log, analytics, security monitor, usage tracker, owner notification

#### AccessRevoked
- **Description**: Document access has been removed
- **Triggered When**: Owner removes sharing permissions
- **Key Data**: Document ID, revoked from user, revocation date, revoked by, previous permission level, revocation reason
- **Consumers**: Access controller, notification service, audit log, permission updater

#### AccessAttemptDenied
- **Description**: Unauthorized access to document was blocked
- **Triggered When**: User attempts to access document without permission
- **Key Data**: Document ID, attempted by, attempt timestamp, access method, denial reason, security alert triggered
- **Consumers**: Security monitor, alert service, access log, suspicious activity detector

### EncryptionEvents

#### DocumentEncrypted
- **Description**: Document has been secured with encryption
- **Triggered When**: Document uploaded or encryption applied to existing document
- **Key Data**: Document ID, encryption date, encryption algorithm, key ID, encrypted by, encryption strength
- **Consumers**: Security manager, encryption key vault, compliance tracker, security audit

#### EncryptionKeyRotated
- **Description**: Document encryption key has been changed
- **Triggered When**: Security policy triggers key rotation or manual rotation initiated
- **Key Data**: Document ID, previous key ID, new key ID, rotation date, rotation reason, re-encryption status
- **Consumers**: Encryption service, key manager, security log, compliance tracker

#### DecryptionPerformed
- **Description**: Encrypted document has been decrypted for access
- **Triggered When**: Authorized user opens encrypted document
- **Key Data**: Document ID, decryption timestamp, decrypted for user, access duration, re-encrypted flag
- **Consumers**: Access log, security monitor, usage tracker, compliance audit

### VersionControlEvents

#### VersionCreated
- **Description**: New version of document has been saved
- **Triggered When**: Document updated and previous version preserved
- **Key Data**: Document ID, version number, version date, created by, change description, file differences, major/minor flag
- **Consumers**: Version history, rollback capability, change tracker, comparison service

#### VersionCompared
- **Description**: Two document versions have been compared
- **Triggered When**: User requests diff or comparison between versions
- **Key Data**: Document ID, version 1, version 2, comparison date, compared by, differences found, comparison type
- **Consumers**: Comparison viewer, change highlighter, version selector, audit support

#### VersionRestored
- **Description**: Previous document version has been made current
- **Triggered When**: User rolls back to earlier version
- **Key Data**: Document ID, restored version number, restoration date, restored by, current version archived, restoration reason
- **Consumers**: Version manager, change log, notification service, audit trail

#### VersionPruned
- **Description**: Old document versions have been removed
- **Triggered When**: Retention policy or manual cleanup removes old versions
- **Key Data**: Document ID, versions removed, prune date, retention policy, space reclaimed, oldest version kept
- **Consumers**: Storage optimizer, version manager, retention compliance, backup updater

### SearchEvents

#### DocumentSearchPerformed
- **Description**: User has searched for documents
- **Triggered When**: User executes search query in vault
- **Key Data**: Search query, search filters, results count, search timestamp, user ID, search method (text/tags/metadata)
- **Consumers**: Search analytics, popular searches tracker, search optimization, relevance tuner

#### OCRCompleted
- **Description**: Text extraction from scanned document has finished
- **Triggered When**: OCR processing completes on image or PDF
- **Key Data**: Document ID, OCR completion date, text extracted, confidence level, searchable now flag, processing duration
- **Consumers**: Search indexer, full-text search enabler, metadata enricher, document accessibility

#### MetadataExtracted
- **Description**: Document metadata has been parsed and stored
- **Triggered When**: System extracts metadata from uploaded file
- **Key Data**: Document ID, metadata fields, extraction date, auto-tags applied, title, author, creation date, keywords
- **Consumers**: Metadata manager, search indexer, auto-categorizer, document properties

### ExpirationEvents

#### ExpirationDateSet
- **Description**: Document expiration date has been configured
- **Triggered When**: User sets when document should expire or become invalid
- **Key Data**: Document ID, expiration date, set by, set date, expiration action (archive/delete/alert), reminder schedule
- **Consumers**: Expiration monitor, reminder scheduler, retention manager, compliance tracker

#### DocumentExpiring
- **Description**: Document is approaching expiration date
- **Triggered When**: System detects document expiration within warning window
- **Key Data**: Document ID, expiration date, days until expiration, expiration action, renewal option, owner notification
- **Consumers**: Notification service, renewal prompt, action planner, compliance monitor

#### DocumentExpired
- **Description**: Document has reached expiration date
- **Triggered When**: Expiration date arrives
- **Key Data**: Document ID, expiration date, expiration action taken, archived/deleted flag, renewal occurred
- **Consumers**: Expiration handler, archiver or deleter, compliance recorder, owner notification

### RetentionEvents

#### RetentionPolicyApplied
- **Description**: Document retention rule has been assigned
- **Triggered When**: Retention policy automatically or manually applied to document
- **Key Data**: Document ID, policy ID, retention period, policy start date, minimum retention date, legal hold flag
- **Consumers**: Retention manager, deletion preventer, compliance tracker, legal hold enforcer

#### RetentionPeriodCompleted
- **Description**: Minimum retention requirement has been satisfied
- **Triggered When**: Required retention period expires
- **Key Data**: Document ID, retention end date, policy satisfied, eligible for deletion, disposition action
- **Consumers**: Deletion eligibility marker, disposition workflow, compliance recorder, archive migrator

#### LegalHoldPlaced
- **Description**: Document preservation order has been applied
- **Triggered When**: Legal requirement prevents document deletion
- **Key Data**: Document ID, hold ID, hold placement date, placed by, hold reason, expected release date, matter reference
- **Consumers**: Deletion blocker, legal compliance, hold manager, audit trail

#### LegalHoldReleased
- **Description**: Document preservation requirement has been lifted
- **Triggered When**: Legal matter resolved and hold removed
- **Key Data**: Document ID, hold ID, release date, released by, hold duration, disposition now allowed
- **Consumers**: Hold remover, deletion eligibility, retention policy re-application, compliance log

### AuditEvents

#### AuditLogGenerated
- **Description**: Document activity audit report has been created
- **Triggered When**: User requests audit trail or scheduled audit runs
- **Key Data**: Report ID, document ID, audit period, activities logged, generated by, generation date, compliance purpose
- **Consumers**: Audit viewer, compliance reporting, security review, access investigation

#### SuspiciousActivityDetected
- **Description**: Unusual document access pattern has been identified
- **Triggered When**: Security monitoring detects anomalous behavior
- **Key Data**: Activity ID, document ID, suspicious user, activity type, detection date, risk level, investigation status
- **Consumers**: Security alert, access reviewer, incident response, admin notification

### BackupEvents

#### BackupCompleted
- **Description**: Document vault backup has finished successfully
- **Triggered When**: Scheduled backup process completes
- **Key Data**: Backup ID, backup date, documents backed up, backup size, backup location, backup integrity verified
- **Consumers**: Backup status monitor, disaster recovery, retention compliance, restore readiness

#### DocumentRestoreRequested
- **Description**: Request to recover document from backup has been initiated
- **Triggered When**: User needs to restore accidentally deleted or corrupted document
- **Key Data**: Restore request ID, document ID, backup date to restore from, requested by, request date, urgency
- **Consumers**: Restore workflow, backup retrieval, integrity checker, restoration service
