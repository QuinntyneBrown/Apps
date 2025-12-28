# Domain Events - Personal Loan Comparison Tool

## Overview
This document defines the domain events tracked in the Personal Loan Comparison Tool application. These events capture significant business occurrences related to loan comparison, debt consolidation analysis, repayment optimization, and lender evaluation.

## Events

### LoanEvents

#### LoanOfferAdded
- **Description**: A loan offer has been added for comparison
- **Triggered When**: User inputs loan offer details from a lender
- **Key Data**: Offer ID, lender name, loan amount, interest rate, term, monthly payment, fees, offer date, user ID, timestamp
- **Consumers**: Comparison engine, ranking calculator, offer aggregator, dashboard UI

#### LoanOfferAccepted
- **Description**: User has chosen and accepted a loan offer
- **Triggered When**: User marks an offer as accepted
- **Key Data**: Offer ID, acceptance date, final terms, lender, loan amount, user ID
- **Consumers**: Active loan tracker, offer archiver, comparison closer, recommendation analyzer

#### LoanOfferRejected
- **Description**: User has declined a loan offer
- **Triggered When**: User marks offer as not proceeding
- **Key Data**: Offer ID, rejection reason, rejection date, user ID
- **Consumers**: Offer archiver, preference learner, lender feedback tracker

#### LoanOfferExpired
- **Description**: Loan offer has passed expiration date
- **Triggered When**: Offer expiration date is reached
- **Key Data**: Offer ID, expiration date, offer terms, user ID
- **Consumers**: Offer cleanup service, expired offer archiver, reminder service

### ComparisonEvents

#### LoanComparisonGenerated
- **Description**: Side-by-side comparison of loan offers created
- **Triggered When**: User requests comparison of multiple loan options
- **Key Data**: Comparison ID, offer IDs, comparison metrics, ranking, best option, timestamp, user ID
- **Consumers**: Comparison visualizer, recommendation engine, decision support system

#### TotalCostCalculated
- **Description**: Total cost of loan over full term computed
- **Triggered When**: Loan offer added or comparison requested
- **Key Data**: Offer ID, total payments, total interest, fees, true cost, cost comparison to other offers, user ID
- **Consumers**: Cost comparator, ranking engine, affordability checker

#### APRCalculated
- **Description**: Annual Percentage Rate (true cost) calculated
- **Triggered When**: Loan offer with fees added or updated
- **Key Data**: Offer ID, stated rate, fees, calculated APR, APR comparison, calculation date
- **Consumers**: True cost comparator, transparency checker, ranking adjuster

#### BestOfferIdentified
- **Description**: System has determined the optimal loan offer
- **Triggered When**: Comparison analysis completes
- **Key Data**: Offer ID, ranking factors, total savings vs alternatives, recommendation confidence, timestamp
- **Consumers**: Recommendation service, notification system, decision dashboard

### DebtEvents

#### CurrentDebtAdded
- **Description**: Existing debt recorded for consolidation analysis
- **Triggered When**: User inputs current debt (credit card, loan, etc.)
- **Key Data**: Debt ID, creditor, balance, interest rate, minimum payment, debt type, user ID, timestamp
- **Consumers**: Debt aggregator, consolidation analyzer, payoff calculator

#### DebtConsolidationAnalyzed
- **Description**: Analysis of consolidating multiple debts into one loan completed
- **Triggered When**: User requests consolidation scenario
- **Key Data**: Analysis ID, total current debt, current total payments, consolidated loan terms, monthly savings, interest savings, timestamp
- **Consumers**: Consolidation recommender, savings calculator, comparison generator

#### PayoffTimelineCalculated
- **Description**: Debt payoff schedule has been projected
- **Triggered When**: Current debts or loan offers analyzed
- **Key Data**: Scenario ID, current payoff timeline, consolidated payoff timeline, time savings, total cost comparison, user ID
- **Consumers**: Timeline visualizer, savings calculator, decision support

### CreditEvents

#### CreditScoreEntered
- **Description**: User's credit score has been recorded
- **Triggered When**: User inputs credit score for rate estimation
- **Key Data**: Credit score, score source, entry date, user ID
- **Consumers**: Rate estimator, eligibility checker, lender matcher

#### RateEstimateAdjusted
- **Description**: Estimated interest rate adjusted based on credit profile
- **Triggered When**: Credit score or other factors updated
- **Key Data**: Previous rate estimate, new rate estimate, adjustment factors, user ID, timestamp
- **Consumers**: Offer updater, payment recalculator, comparison refresher

#### PrequalificationCompleted
- **Description**: Soft credit check or prequalification performed
- **Triggered When**: User completes prequalification with lender
- **Key Data**: Prequalification ID, lender, qualified amount, estimated rate, expiration date, user ID
- **Consumers**: Offer creator, rate confirmer, eligibility tracker

### AffordabilityEvents

#### AffordabilityAssessed
- **Description**: User's ability to afford loan payments evaluated
- **Triggered When**: Income and expenses analyzed against loan offers
- **Key Data**: Monthly income, monthly obligations, DTI ratio, max affordable payment, assessment date, user ID
- **Consumers**: Affordability filter, budget impact analyzer, risk assessor

#### DebtToIncomeCalculated
- **Description**: Debt-to-income ratio computed
- **Triggered When**: Monthly obligations and income updated
- **Key Data**: Total monthly debt, monthly income, DTI percentage, lender threshold comparison, timestamp
- **Consumers**: Qualification estimator, affordability checker, risk indicator

#### BudgetImpactAnalyzed
- **Description**: Effect of loan payment on monthly budget calculated
- **Triggered When**: User reviews budget impact of loan offers
- **Key Data**: Offer ID, monthly payment, current budget, available funds after payment, budget strain percentage, user ID
- **Consumers**: Affordability advisor, payment warning system, budget planner

### PayoffEvents

#### PayoffStrategyCreated
- **Description**: Loan repayment strategy has been configured
- **Triggered When**: User models standard, accelerated, or custom payoff plan
- **Key Data**: Strategy ID, loan ID, strategy type, extra payments, projected payoff date, interest savings, user ID, timestamp
- **Consumers**: Payoff calculator, savings projector, strategy comparator

#### ExtraPaymentImpactCalculated
- **Description**: Effect of additional principal payments computed
- **Triggered When**: User models making extra payments
- **Key Data**: Loan ID, extra payment amount, frequency, time saved, interest saved, new payoff date, user ID
- **Consumers**: Payoff optimizer, savings visualizer, motivation system

#### EarlyPayoffProjected
- **Description**: Accelerated payoff timeline calculated
- **Triggered When**: User explores paying off loan early
- **Key Data**: Loan ID, current payoff date, accelerated payoff date, time saved, interest saved, required payments, user ID
- **Consumers**: Payoff planner, savings calculator, strategy recommender

### LenderEvents

#### LenderAdded
- **Description**: New lender added to comparison database
- **Triggered When**: User adds lender information for tracking offers
- **Key Data**: Lender ID, lender name, contact info, specialties, user rating, user ID, timestamp
- **Consumers**: Lender directory, offer association, preference tracker

#### LenderRated
- **Description**: User has rated their experience with a lender
- **Triggered When**: User provides lender review or rating
- **Key Data**: Lender ID, rating score, review text, rating date, user ID
- **Consumers**: Lender reputation tracker, recommendation adjuster, review aggregator

### FeesEvents

#### OriginationFeeCalculated
- **Description**: Loan origination or processing fee computed
- **Triggered When**: Loan offer includes percentage-based origination fee
- **Key Data**: Offer ID, fee percentage, fee amount, total loan cost impact, user ID
- **Consumers**: Total cost calculator, APR calculator, fee comparator

#### PrepaymentPenaltyIdentified
- **Description**: Loan offer includes prepayment penalty clause
- **Triggered When**: User marks offer as having prepayment restrictions
- **Key Data**: Offer ID, penalty type, penalty amount/percentage, penalty period, user ID
- **Consumers**: Early payoff calculator, offer ranking adjuster, risk highlighter

### RecommendationEvents

#### PersonalizedRecommendationGenerated
- **Description**: Custom loan recommendation created based on user profile
- **Triggered When**: System analyzes user needs and available offers
- **Key Data**: Recommendation ID, recommended offer, reasoning, fit score, alternatives, timestamp, user ID
- **Consumers**: Recommendation UI, notification service, decision support

#### SavingsOpportunityIdentified
- **Description**: Potential to save money identified through better loan option
- **Triggered When**: Analysis finds significantly better offer than current debt
- **Key Data**: Opportunity type, potential monthly savings, total interest savings, recommended action, timestamp
- **Consumers**: Alert service, optimization advisor, notification system

### ApplicationEvents

#### LoanApplicationStarted
- **Description**: User has begun loan application process
- **Triggered When**: User clicks through to apply with lender
- **Key Data**: Application ID, offer ID, lender, application start date, user ID
- **Consumers**: Application tracker, conversion monitor, lender analytics

#### LoanApplicationApproved
- **Description**: Loan application has been approved by lender
- **Triggered When**: User updates application status to approved
- **Key Data**: Application ID, approval date, final terms, approved amount, user ID
- **Consumers**: Offer finalizer, success tracker, recommendation validator
