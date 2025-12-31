import { Routes } from '@angular/router';
import { EquipmentList, EquipmentForm } from './pages/equipment';
import { MaintenanceList, MaintenanceForm } from './pages/maintenance';
import { WorkoutMappingList, WorkoutMappingForm } from './pages/workout-mapping';

export const routes: Routes = [
  { path: '', redirectTo: '/equipment', pathMatch: 'full' },
  { path: 'equipment', component: EquipmentList },
  { path: 'equipment/create', component: EquipmentForm },
  { path: 'equipment/edit/:id', component: EquipmentForm },
  { path: 'maintenance', component: MaintenanceList },
  { path: 'maintenance/create', component: MaintenanceForm },
  { path: 'maintenance/edit/:id', component: MaintenanceForm },
  { path: 'workouts', component: WorkoutMappingList },
  { path: 'workouts/create', component: WorkoutMappingForm },
  { path: 'workouts/edit/:id', component: WorkoutMappingForm }
];
