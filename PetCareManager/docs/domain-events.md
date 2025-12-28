# Domain Events - Pet Care Manager

## Overview
This document defines the domain events tracked in the Pet Care Manager application. These events capture significant business occurrences related to pet health records, veterinary appointments, medication tracking, grooming schedules, and overall pet wellness management.

## Events

### PetProfileEvents

#### PetRegistered
- **Description**: New pet has been added to household care system
- **Triggered When**: User creates profile for pet with basic information
- **Key Data**: Pet ID, name, species, breed, birth date/age, gender, weight, microchip number, adoption/purchase date, photo
- **Consumers**: Pet directory, health record initializer, care scheduler, emergency contact card, microchip registry

#### PetProfileUpdated
- **Description**: Pet information has been modified
- **Triggered When**: User updates pet details or health status
- **Key Data**: Pet ID, updated fields, previous values, new values, update date, update reason
- **Consumers**: Pet profile, health record, care plan adjuster, emergency info updater

#### PetWeightRecorded
- **Description**: Pet weight measurement has been logged
- **Triggered When**: User records weight from vet visit or home weighing
- **Key Data**: Pet ID, weight, measurement date, measurement location, weight trend, ideal weight range
- **Consumers**: Health tracker, weight trend analyzer, medication dosage calculator, diet plan adjuster

#### PetPassedAway
- **Description**: Pet has died or been euthanized
- **Triggered When**: User records pet's passing
- **Key Data**: Pet ID, date of death, cause, age at death, euthanasia decision, memorial notes, final vet visit
- **Consumers**: Profile archiver, record closer, memorial service, grief resources, household update

### VeterinaryEvents

#### VetAppointmentScheduled
- **Description**: Veterinary visit has been booked
- **Triggered When**: User schedules appointment with veterinarian
- **Key Data**: Appointment ID, pet ID, vet clinic, appointment date/time, visit type, reason for visit, veterinarian name
- **Consumers**: Calendar integration, reminder scheduler, preparation checklist, medical history packager

#### VetAppointmentCompleted
- **Description**: Veterinary visit has been attended and completed
- **Triggered When**: User logs appointment completion with visit details
- **Key Data**: Appointment ID, visit date, veterinarian, diagnosis, treatment provided, prescriptions, next visit date, visit cost, visit notes
- **Consumers**: Medical record, medication scheduler, follow-up reminder, expense tracker, health timeline

#### DiagnosisReceived
- **Description**: Pet has been diagnosed with health condition
- **Triggered When**: Veterinarian identifies illness or condition
- **Key Data**: Diagnosis ID, pet ID, condition name, diagnosis date, severity, treatment plan, prognosis, veterinarian, diagnostic tests
- **Consumers**: Medical history, treatment planner, medication manager, monitoring scheduler, care instruction library

#### VeterinaryEmergency
- **Description**: Pet requires urgent medical attention
- **Triggered When**: User marks situation as emergency
- **Key Data**: Emergency ID, pet ID, emergency type, symptoms, timestamp, emergency clinic contacted, transport needed, insurance contacted
- **Consumers**: Emergency contact list, 24hr vet finder, medical history quick access, insurance claim initiator

### MedicationEvents

#### MedicationPrescribed
- **Description**: Medication has been prescribed to pet
- **Triggered When**: Veterinarian prescribes medication or supplement
- **Key Data**: Prescription ID, pet ID, medication name, dosage, frequency, start date, end date, prescribing vet, purpose, special instructions
- **Consumers**: Medication tracker, dosing scheduler, reminder service, pharmacy integration, refill monitor

#### MedicationAdministered
- **Description**: Medicine dose has been given to pet
- **Triggered When**: User logs medication administration
- **Key Data**: Administration ID, prescription ID, pet ID, administered date/time, dosage given, administered by, pet reaction
- **Consumers**: Medication log, compliance tracker, next dose scheduler, side effect monitor

#### MedicationMissed
- **Description**: Scheduled medication dose was not given
- **Triggered When**: Dose time passes without administration logged
- **Key Data**: Prescription ID, missed dose time, pet ID, reason for miss, makeup dose plan
- **Consumers**: Alert service, compliance tracker, makeup dose scheduler, veterinary consultation trigger

#### MedicationRefillNeeded
- **Description**: Medication supply is running low
- **Triggered When**: Remaining doses fall below threshold
- **Key Data**: Prescription ID, medication name, doses remaining, estimated run-out date, refill source, cost
- **Consumers**: Refill reminder, pharmacy order trigger, budget allocator, prescription renewal checker

#### MedicationDiscontinued
- **Description**: Pet medication has been stopped
- **Triggered When**: Treatment completes or vet discontinues medication
- **Key Data**: Prescription ID, discontinuation date, reason, treatment outcome, side effects experienced, total treatment duration
- **Consumers**: Medication tracker updater, medical history, outcome recorder, prescription archiver

### VaccinationEvents

#### VaccinationAdministered
- **Description**: Vaccine has been given to pet
- **Triggered When**: Pet receives immunization at vet clinic
- **Key Data**: Vaccination ID, pet ID, vaccine type, administration date, veterinarian, clinic, lot number, next due date, certificate issued
- **Consumers**: Vaccination record, reminder scheduler, certificate vault, boarding requirement checker, legal compliance

#### VaccinationDue
- **Description**: Pet vaccination booster is approaching due date
- **Triggered When**: System detects upcoming vaccination requirement
- **Key Data**: Pet ID, vaccine type, due date, days until due, last vaccination date, vet clinic, importance level
- **Consumers**: Reminder service, appointment scheduler, vaccination planner, boarding blocker if overdue

#### VaccinationOverdue
- **Description**: Required vaccination has not been given by due date
- **Triggered When**: Vaccination due date passes without administration
- **Key Data**: Pet ID, vaccine type, due date, days overdue, risks, legal implications, boarding restrictions
- **Consumers**: Alert service, urgent appointment scheduler, compliance monitor, activity restriction notifier

### GroomingEvents

#### GroomingAppointmentScheduled
- **Description**: Grooming session has been booked
- **Triggered When**: User schedules professional grooming or bath
- **Key Data**: Appointment ID, pet ID, groomer, appointment date/time, services requested, special instructions, estimated cost
- **Consumers**: Calendar integration, reminder service, groomer communication, budget tracker

#### GroomingCompleted
- **Description**: Grooming session has been finished
- **Triggered When**: Pet grooming appointment completed
- **Key Data**: Appointment ID, completion date, services performed, groomer, cost, next grooming recommended, pet behavior notes, style photos
- **Consumers**: Grooming history, expense tracker, next appointment scheduler, groomer rating, style reference

#### HomeGroomingLogged
- **Description**: At-home grooming activity has been recorded
- **Triggered When**: User logs self-grooming tasks (nail trim, brush, bath)
- **Key Data**: Grooming ID, pet ID, grooming date, tasks performed, duration, supplies used, next grooming due
- **Consumers**: Grooming schedule, supply inventory, next grooming reminder, DIY grooming tracker

### NutritionEvents

#### FoodBrandChanged
- **Description**: Pet's primary food has been switched
- **Triggered When**: User updates pet food information
- **Key Data**: Pet ID, previous food, new food, change date, reason for change, transition plan, vet recommended
- **Consumers**: Nutrition tracker, feeding scheduler, digestive health monitor, purchase reminder, cost tracker

#### FeedingScheduleSet
- **Description**: Feeding times and portions have been established
- **Triggered When**: User defines feeding routine
- **Key Data**: Pet ID, meals per day, portion sizes, feeding times, food type, special diet requirements
- **Consumers**: Feeding reminder, portion calculator, nutrition tracker, multiple caregiver coordinator

#### TreatGiven
- **Description**: Pet treat or snack has been provided
- **Triggered When**: User logs treat administration
- **Key Data**: Pet ID, treat type, quantity, timestamp, given by, treat calories, reason (training/reward/other)
- **Consumers**: Calorie tracker, treat limit monitor, training log, weight management

#### DietaryIssueReported
- **Description**: Pet has experienced food-related problem
- **Triggered When**: User logs digestive issue or food reaction
- **Key Data**: Pet ID, issue type, symptoms, suspected food, occurrence date, severity, veterinary contact needed
- **Consumers**: Health alert, food elimination tracker, veterinary consultation trigger, diet change recommender

### ActivityEvents

#### ExerciseSessionLogged
- **Description**: Pet physical activity has been recorded
- **Triggered When**: User logs walk, play session, or exercise
- **Key Data**: Activity ID, pet ID, activity type, duration, distance, intensity, timestamp, location, companions
- **Consumers**: Activity tracker, exercise goal monitor, health dashboard, energy level tracker, weight management

#### ExerciseGoalSet
- **Description**: Daily or weekly activity target has been established
- **Triggered When**: User defines exercise goals for pet
- **Key Data**: Pet ID, goal type, target (duration/distance/frequency), start date, goal reason, vet recommended
- **Consumers**: Goal tracker, exercise reminder, achievement monitor, health plan

#### BehaviorIncidentLogged
- **Description**: Notable behavior or incident has been recorded
- **Triggered When**: User documents behavioral issue or milestone
- **Key Data**: Incident ID, pet ID, incident date, behavior type, triggers, response actions, severity, training needed
- **Consumers**: Behavior pattern analyzer, training need identifier, veterinary consultation data, incident history

### ExpenseEvents

#### PetExpenseRecorded
- **Description**: Pet-related cost has been logged
- **Triggered When**: User records purchase or service payment
- **Key Data**: Expense ID, pet ID, amount, category, date, vendor, description, receipt, tax deductible flag
- **Consumers**: Budget tracker, expense analyzer, tax documentation, cost per pet calculator, category spending

#### InsuranceClaimFiled
- **Description**: Pet insurance claim has been submitted
- **Triggered When**: User initiates claim for covered expense
- **Key Data**: Claim ID, pet ID, claim amount, claim date, incident date, diagnosis, treatment, documentation submitted, claim number
- **Consumers**: Claim tracker, insurance communication, reimbursement monitor, medical record linker

#### InsuranceClaimSettled
- **Description**: Pet insurance claim has been resolved
- **Triggered When**: Insurance company processes claim
- **Key Data**: Claim ID, settlement date, amount reimbursed, amount denied, settlement reason, processing time
- **Consumers**: Expense updater, insurance value tracker, claim history, out-of-pocket calculator
