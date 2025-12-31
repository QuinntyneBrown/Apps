import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MissionStatementService } from '../services';
import { MissionStatement, CreateMissionStatement, UpdateMissionStatement } from '../models';

@Component({
  selector: 'app-mission-statement-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Mission Statement</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field class="dialog__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Mission Statement Text</mat-label>
          <textarea matInput formControlName="text" rows="6" required></textarea>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Statement Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="statementDate">
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-checkbox *ngIf="data" formControlName="isCurrentVersion">
          Set as Current Version
        </mat-checkbox>
      </mat-dialog-content>
      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
          {{ data ? 'Update' : 'Create' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .dialog__field {
      width: 100%;
      margin-bottom: 1rem;
    }
  `]
})
export class MissionStatementDialog {
  private fb = inject(FormBuilder);
  data: MissionStatement | null = inject(MatDialog).openDialogs[0]?.componentInstance?.data || null;

  form = this.fb.group({
    title: [this.data?.title || '', Validators.required],
    text: [this.data?.text || '', Validators.required],
    statementDate: [this.data?.statementDate || new Date()],
    isCurrentVersion: [this.data?.isCurrentVersion || false]
  });

  onSubmit() {
    if (this.form.valid) {
      inject(MatDialog).closeAll();
    }
  }
}

@Component({
  selector: 'app-mission-statements',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule
  ],
  templateUrl: './mission-statements.html',
  styleUrl: './mission-statements.scss'
})
export class MissionStatements implements OnInit {
  private missionStatementService = inject(MissionStatementService);
  private dialog = inject(MatDialog);

  missionStatements$ = this.missionStatementService.missionStatements$;
  displayedColumns = ['title', 'version', 'isCurrentVersion', 'statementDate', 'createdAt', 'actions'];

  ngOnInit() {
    this.missionStatementService.getAll().subscribe();
  }

  openDialog(missionStatement?: MissionStatement) {
    const dialogRef = this.dialog.open(MissionStatementDialog, {
      width: '600px',
      data: missionStatement
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        if (missionStatement) {
          this.updateMissionStatement(missionStatement.missionStatementId, result);
        } else {
          this.createMissionStatement(result);
        }
      }
    });
  }

  createMissionStatement(data: any) {
    const create: CreateMissionStatement = {
      userId: '00000000-0000-0000-0000-000000000000', // Replace with actual user ID
      title: data.title,
      text: data.text,
      statementDate: data.statementDate
    };
    this.missionStatementService.create(create).subscribe();
  }

  updateMissionStatement(id: string, data: any) {
    const update: UpdateMissionStatement = {
      missionStatementId: id,
      title: data.title,
      text: data.text,
      isCurrentVersion: data.isCurrentVersion
    };
    this.missionStatementService.update(update).subscribe();
  }

  deleteMissionStatement(id: string) {
    if (confirm('Are you sure you want to delete this mission statement?')) {
      this.missionStatementService.delete(id).subscribe();
    }
  }
}
