import { Routes } from '@angular/router';
import { Dashboard, DigitalAccounts, LegacyDocuments, LegacyInstructions, TrustedContacts } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'digital-accounts', component: DigitalAccounts },
  { path: 'legacy-documents', component: LegacyDocuments },
  { path: 'legacy-instructions', component: LegacyInstructions },
  { path: 'trusted-contacts', component: TrustedContacts }
];
