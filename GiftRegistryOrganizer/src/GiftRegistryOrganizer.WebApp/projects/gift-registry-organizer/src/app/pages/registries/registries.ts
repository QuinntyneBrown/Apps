import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { RegistryService } from '../../services';
import { RegistryCard } from '../../components/registry-card/registry-card';
import { RegistryDialog } from '../../components/registry-dialog/registry-dialog';
import { Registry } from '../../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-registries',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    RegistryCard
  ],
  templateUrl: './registries.html',
  styleUrl: './registries.scss'
})
export class Registries implements OnInit {
  private registryService = inject(RegistryService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);
  private router = inject(Router);

  registries$!: Observable<Registry[]>;
  loading$!: Observable<boolean>;

  private userId = '00000000-0000-0000-0000-000000000001';

  ngOnInit(): void {
    this.registries$ = this.registryService.registries$;
    this.loading$ = this.registryService.loading$;
    this.loadRegistries();
  }

  loadRegistries(): void {
    this.registryService.getRegistries(this.userId).subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(RegistryDialog, {
      width: '600px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.registryService.createRegistry(result).subscribe({
          next: () => {
            this.snackBar.open('Registry created successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to create registry', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onEditRegistry(registry: Registry): void {
    const dialogRef = this.dialog.open(RegistryDialog, {
      width: '600px',
      data: { registry, userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.registryService.updateRegistry(registry.registryId, result).subscribe({
          next: () => {
            this.snackBar.open('Registry updated successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to update registry', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onDeleteRegistry(registry: Registry): void {
    if (confirm(`Are you sure you want to delete ${registry.name}?`)) {
      this.registryService.deleteRegistry(registry.registryId).subscribe({
        next: () => {
          this.snackBar.open('Registry deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete registry', 'Close', { duration: 3000 });
        }
      });
    }
  }

  onViewItems(registry: Registry): void {
    this.registryService.setSelectedRegistry(registry);
    this.router.navigate(['/registry-items'], { queryParams: { registryId: registry.registryId } });
  }
}
