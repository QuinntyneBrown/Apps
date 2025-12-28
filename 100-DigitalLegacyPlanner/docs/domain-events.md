# Domain Events - Digital Legacy Planner

## Overview
This application helps users plan for their digital afterlife by documenting accounts, creating access instructions, and preparing emergency protocols. Domain events capture legacy planning, account inventory, instruction creation, and the preparation for digital asset transition.

## Events

### AccountInventoryEvents

#### DigitalAccountRegistered
- **Description**: A digital account has been added to the legacy inventory
- **Triggered When**: User documents an online account for legacy planning
- **Key Data**: Account ID, user ID, service name, account type, importance level, username, timestamp
- **Consumers**: Account inventory, legacy value assessment, access planning

#### AccountCategorized
- **Description**: A digital account has been classified by type or importance
- **Triggered When**: User assigns category and priority to account
- **Key Data**: Account ID, category (social/financial/email/storage/etc.), legacy priority, closure preference, timestamp
- **Consumers**: Category organization, priority planning, estate value assessment

#### AccountAccessDetailsAdded
- **Description**: Login and access information has been documented
- **Triggered When**: User records credentials and access methods
- **Key Data**: Account ID, access method, password location reference, 2FA details, recovery info, timestamp
- **Consumers**: Encrypted vault, access instruction compiler, emergency access system

#### AccountValueAssessed
- **Description**: Financial or sentimental value of account has been evaluated
- **Triggered When**: User rates account importance to estate
- **Key Data**: Account ID, monetary value, sentimental value, content worth preserving, timestamp
- **Consumers**: Estate planning, priority setting, preservation planning

### InstructionEvents

#### LegacyInstructionsCreated
- **Description**: Specific instructions for handling account have been written
- **Triggered When**: User documents what to do with account after death
- **Key Data**: Instruction ID, account ID, preferred action (close/memorialize/preserve/transfer), detailed steps, timestamp
- **Consumers**: Instruction repository, executor guidance, legacy fulfillment

#### MemorializationPreferenceSet
- **Description**: User has indicated preference for account memorialization
- **Triggered When**: User chooses how account should be remembered
- **Key Data**: Preference ID, account ID, memorialize vs. delete, memorial settings, timestamp
- **Consumers**: Social media planning, memory preservation, platform-specific protocols

#### DataDownloadInstructed
- **Description**: Instructions to download account data have been specified
- **Triggered When**: User requests data preservation before closure
- **Key Data**: Instruction ID, account ID, data types to preserve, download method, storage location, timestamp
- **Consumers**: Data preservation planning, digital asset extraction, archive creation

#### ContentDistributionPlanned
- **Description**: Plan for distributing digital content has been created
- **Triggered When**: User designates recipients for digital assets
- **Key Data**: Distribution plan ID, account/content IDs, beneficiaries, distribution method, timestamp
- **Consumers**: Digital will, beneficiary notification, asset transfer planning

### TrustedContactEvents

#### DigitalExecutorDesignated
- **Description**: A person has been assigned as digital executor
- **Triggered When**: User names trusted person to manage digital legacy
- **Key Data**: Executor ID, executor name, contact info, scope of authority, access level, timestamp
- **Consumers**: Executor registry, access control, notification planning

#### EmergencyContactAdded
- **Description**: Emergency contact for digital legacy has been registered
- **Triggered When**: User adds backup person for legacy management
- **Key Data**: Contact ID, contact name, relationship, contact info, authority level, timestamp
- **Consumers**: Emergency protocol, backup planning, access hierarchy

#### AccessPermissionGranted
- **Description**: Specific access permissions have been given to trusted person
- **Triggered When**: User authorizes someone to access certain accounts
- **Key Data**: Permission ID, grantee ID, account IDs, access scope, activation conditions, timestamp
- **Consumers**: Access control, emergency activation, permission management

#### ExecutorNotified
- **Description**: Digital executor has been informed of their role
- **Triggered When**: User sends notification to executor
- **Key Data**: Notification ID, executor ID, notification method, acceptance status, timestamp
- **Consumers**: Executor onboarding, role confirmation, communication tracking

### EmergencyProtocolEvents

#### EmergencyAccessConfigured
- **Description**: Emergency access mechanism has been set up
- **Triggered When**: User configures how access is granted in emergency
- **Key Data**: Protocol ID, activation trigger, waiting period, notification recipients, access package, timestamp
- **Consumers**: Emergency system, dead man's switch, automated activation

#### DeadManSwitchActivated
- **Description**: Inactivity-based access release has been triggered
- **Triggered When**: User inactivity exceeds threshold
- **Key Data**: Activation ID, inactivity duration, trigger date, notification sent, timestamp
- **Consumers**: Emergency notification, access release, executor alert

#### EmergencyAccessRequested
- **Description**: Trusted contact has requested emergency access
- **Triggered When**: Executor initiates access request
- **Key Data**: Request ID, requester ID, request reason, verification required, timestamp
- **Consumers**: Verification workflow, access approval, security check

#### EmergencyAccessGranted
- **Description**: Emergency access to legacy information has been approved
- **Triggered When**: Verification complete and access released
- **Key Data**: Grant ID, request ID, grantee ID, access scope, grant timestamp
- **Consumers**: Vault unlocking, instruction delivery, access logging

#### ActivityConfirmed
- **Description**: User has confirmed they are alive and active
- **Triggered When**: User responds to inactivity check
- **Key Data**: Confirmation ID, confirmation date, next check date, timestamp
- **Consumers**: Dead man's switch reset, emergency protocol cancellation, activity monitoring

### DocumentEvents

#### DigitalWillCreated
- **Description**: Formal digital will document has been generated
- **Triggered When**: User compiles comprehensive digital legacy plan
- **Key Data**: Will ID, creation date, accounts covered, beneficiaries, legal status, timestamp
- **Consumers**: Legal document storage, estate planning integration, comprehensive planning

#### WillUpdated
- **Description**: Digital will has been revised
- **Triggered When**: User updates legacy instructions
- **Key Data**: Will ID, version number, changes made, update reason, timestamp
- **Consumers**: Version control, beneficiary notification, legal currency

#### EthicalWillRecorded
- **Description**: Non-legal instructions and wishes have been documented
- **Triggered When**: User records personal wishes for digital legacy
- **Key Data**: Ethical will ID, content, wishes, values to preserve, timestamp
- **Consumers**: Legacy guidance, executor context, personal meaning preservation

#### FinalMessageCreated
- **Description**: Message to be delivered after death has been written
- **Triggered When**: User writes farewell or final communication
- **Key Data**: Message ID, recipients, message content, delivery conditions, timestamp
- **Consumers**: Message vault, conditional delivery, emotional legacy

### AssetEvents

#### DigitalAssetDocumented
- **Description**: Valuable digital asset has been registered
- **Triggered When**: User catalogs digital property of value
- **Key Data**: Asset ID, asset type, location, value, access method, beneficiary, timestamp
- **Consumers**: Asset inventory, valuation, transfer planning

#### IntellectualPropertyLogged
- **Description**: Digital IP or creative work has been documented
- **Triggered When**: User records ownership of digital creations
- **Key Data**: IP ID, work description, copyright info, licensing, intended heir, timestamp
- **Consumers**: IP estate planning, creative legacy, rights management

#### CryptocurrencyWalletRecorded
- **Description**: Cryptocurrency holdings have been documented
- **Triggered When**: User adds crypto wallet to legacy plan
- **Key Data**: Wallet ID, cryptocurrency type, wallet address, access method, recovery phrase location, timestamp
- **Consumers**: Crypto asset planning, secure access instructions, high-value asset tracking

#### DigitalPhotoLibraryDocumented
- **Description**: Photo and media library has been cataloged
- **Triggered When**: User plans for photo collection preservation
- **Key Data**: Library ID, storage location, size, preservation priority, access instructions, timestamp
- **Consumers**: Memory preservation, family archive, digital memory legacy

### SubscriptionEvents

#### PaidSubscriptionInventoried
- **Description**: Paid digital subscription has been listed for cancellation planning
- **Triggered When**: User documents subscription for post-death management
- **Key Data**: Subscription ID, service name, cost, payment method, cancellation instructions, timestamp
- **Consumers**: Financial legacy planning, subscription cancellation, estate expense reduction

#### SubscriptionCancellationPlanned
- **Description**: Instructions for subscription cancellation have been created
- **Triggered When**: User specifies which subscriptions to cancel
- **Key Data**: Plan ID, subscription ID, cancellation priority, cancellation method, timestamp
- **Consumers**: Executor instructions, expense management, account closure

### PrivacyEvents

#### PrivacyPreferenceSet
- **Description**: Privacy wishes for digital afterlife have been specified
- **Triggered When**: User indicates privacy desires
- **Key Data**: Preference ID, privacy level, data deletion wishes, exposure limits, timestamp
- **Consumers**: Privacy enforcement, data handling, sensitive content protection

#### SensitiveContentFlagged
- **Description**: Content requiring special handling has been marked
- **Triggered When**: User identifies private or sensitive digital content
- **Key Data**: Flag ID, content location, sensitivity level, handling instructions, timestamp
- **Consumers**: Privacy protection, content filtering, executor guidance

#### DataDeletionScheduled
- **Description**: Automatic data deletion has been programmed
- **Triggered When**: User sets data to be destroyed upon death
- **Key Data**: Deletion schedule ID, data locations, deletion method, verification required, timestamp
- **Consumers**: Data destruction service, privacy preservation, irreversible deletion

### ReviewEvents

#### LegacyPlanReviewed
- **Description**: User has reviewed and updated their digital legacy plan
- **Triggered When**: Periodic or triggered review of plan
- **Key Data**: Review ID, review date, changes made, new accounts added, timestamp
- **Consumers**: Plan currency, completeness tracking, maintenance scheduling

#### PlanCompletenessAssessed
- **Description**: Coverage of digital legacy plan has been evaluated
- **Triggered When**: Analysis of plan comprehensiveness
- **Key Data**: Assessment ID, completion percentage, missing elements, priority gaps, timestamp
- **Consumers**: Gap identification, completion prompts, quality improvement

#### AnnualUpdateReminder
- **Description**: Reminder to review legacy plan has been sent
- **Triggered When**: Annual review date arrives
- **Key Data**: Reminder ID, last review date, changes since last review, timestamp
- **Consumers**: Maintenance prompts, plan currency, proactive updating

### LegalEvents

#### LegalDocumentAttached
- **Description**: Official legal document has been linked to digital plan
- **Triggered When**: User uploads will, power of attorney, or other legal doc
- **Key Data**: Document ID, document type, validity date, legal jurisdiction, timestamp
- **Consumers**: Legal integration, estate planning coordination, official documentation

#### AttorneyNotified
- **Description**: Legal representative has been informed of digital legacy plan
- **Triggered When**: User shares plan with attorney
- **Key Data**: Notification ID, attorney info, plan version shared, timestamp
- **Consumers**: Legal coordination, professional guidance, estate integration

### MemoryEvents

#### MemoryPreservationPlanCreated
- **Description**: Plan for preserving digital memories has been established
- **Triggered When**: User designates content for memory preservation
- **Key Data**: Plan ID, content to preserve, storage method, access for family, timestamp
- **Consumers**: Memory archiving, family legacy, digital immortality

#### StoriesRecorded
- **Description**: Personal stories or life narrative has been documented
- **Triggered When**: User records stories for posterity
- **Key Data**: Story ID, narrative content, audience, delivery timing, timestamp
- **Consumers**: Story vault, family heritage, wisdom preservation
