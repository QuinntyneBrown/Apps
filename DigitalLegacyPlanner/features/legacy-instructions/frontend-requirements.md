# Legacy Instructions - Frontend Requirements

## Overview
User interface for creating detailed legacy instructions for each digital account.

## Pages & Components

### Instructions Dashboard
**Route**: `/instructions`

**Components**:
- Instructions list grouped by account
- Filter by action type (close, memorialize, preserve, transfer)
- Completion progress indicator
- Add instruction button
- Accounts without instructions warning

### Create Instruction Wizard
**Route**: `/instructions/new`

**Steps**:
1. Select Account
2. Choose Preferred Action
3. Detailed Instructions
4. Review & Save

**Features**:
- Platform-specific templates
- Step-by-step guidance
- Action-specific forms
- Preview final instructions

### Memorialization Preferences
**Component**: `<MemorializationForm />`

**Form Fields**:
- Memorialize vs. Delete (toggle)
- Memorial Settings (conditional):
  - Allow comments checkbox
  - Display profile checkbox
  - Custom memorial message (textarea)
- Platform-specific options

**Visual Aids**:
- Examples of memorialized accounts
- Platform policy summaries
- Preview of memorial settings

### Data Download Instructions
**Component**: `<DataDownloadForm />`

**Form Fields**:
- Data types to preserve (checkboxes: Photos, Videos, Posts, Messages, etc.)
- Download method (dropdown: Platform tool, Manual, API)
- Storage location (text input)
- Detailed download instructions (textarea with step numbers)

**Features**:
- Platform-specific download guides
- Estimated data size calculator
- Storage requirement warnings
- Link to platform data export tools

### Content Distribution Planner
**Component**: `<DistributionPlanForm />`

**Features**:
- Select content items (multi-select)
- Assign beneficiaries (searchable dropdown)
- Set access levels per beneficiary
- Choose distribution method
- Add distribution notes

**Visual Design**:
- Drag-and-drop content assignment
- Beneficiary cards with assigned content
- Distribution timeline
- Access level indicators

### Instruction Templates Library
**Component**: `<TemplateLibrary />`

**Features**:
- Pre-built templates for common platforms
- Categorized by platform and action
- Preview before using
- Customize after selection
- Save custom templates

**Templates Include**:
- Facebook memorialization
- Gmail account closure
- Instagram data download
- LinkedIn profile deletion
- Dropbox content transfer

## UI/UX Requirements

### Instruction Builder Interface
- Visual step-by-step builder
- Numbered instruction steps
- Add/remove/reorder steps
- Rich text formatting for instructions
- Checklist items within instructions

### Platform Integration
- Display platform logos and branding
- Link to official platform policies
- Show platform-specific requirements
- Warn about platform limitations

### Progress Tracking
- Instructions completion percentage
- Accounts without instructions highlighted
- Priority-based todo list
- Completion celebration

### Action-Specific Forms

#### Close Account Action
- Cancellation steps
- Data download reminder
- Final account deletion checklist
- Important content preservation warning

#### Memorialize Account Action
- Memorialization preference form
- Platform policy display
- Memorial settings configuration
- Family notification options

#### Preserve Account Action
- Archive creation steps
- Storage location specification
- Access maintenance plan
- Periodic review reminder

#### Transfer Account Action
- Recipient designation
- Transfer method selection
- Ownership change procedure
- Platform-specific transfer guide

## Component Structure

```
/components/instructions/
  ├── InstructionsList.tsx
  ├── InstructionCard.tsx
  ├── CreateInstructionWizard/
  │   ├── SelectAccount.tsx
  │   ├── ChooseAction.tsx
  │   ├── DetailedSteps.tsx
  │   └── ReviewInstructions.tsx
  ├── MemorializationForm.tsx
  ├── DataDownloadForm.tsx
  ├── DistributionPlanForm.tsx
  ├── TemplateLibrary.tsx
  ├── InstructionBuilder.tsx
  └── PlatformGuidance.tsx
```

## State Management

```typescript
interface InstructionState {
  instructions: LegacyInstruction[];
  templates: InstructionTemplate[];
  loading: boolean;
  error: string | null;
  activeInstruction: LegacyInstruction | null;
  wizardStep: number;
}
```

## User Flows

### Create Instruction Flow
1. Click "Add Instruction"
2. Select account from list
3. Choose preferred action
4. Fill action-specific details
5. Add detailed steps
6. Review instruction
7. Save and confirm

### Use Template Flow
1. Browse template library
2. Preview template
3. Select template
4. Auto-fill account details
5. Customize as needed
6. Save instruction

## Validation & Feedback

- Required action selection
- Minimum instruction detail validation
- Beneficiary validation for distribution
- Success confirmation with next steps
- Warning for incomplete instructions

## Accessibility
- Clear action labels
- Keyboard navigation through wizard
- Screen reader support for templates
- High contrast for action types
- Focus management in forms
