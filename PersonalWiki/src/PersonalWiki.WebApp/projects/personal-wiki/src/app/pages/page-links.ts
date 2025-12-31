import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { PageLinkService, WikiPageService } from '../services';
import { PageLink } from '../models';

@Component({
  selector: 'app-page-link-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDialogModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data.link ? 'Edit' : 'Create' }} Page Link</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field class="form-field">
          <mat-label>Source Page</mat-label>
          <mat-select formControlName="sourcePageId" required>
            <mat-option *ngFor="let page of wikiPages$ | async" [value]="page.wikiPageId">
              {{ page.title }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Target Page</mat-label>
          <mat-select formControlName="targetPageId" required>
            <mat-option *ngFor="let page of wikiPages$ | async" [value]="page.wikiPageId">
              {{ page.title }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Anchor Text</mat-label>
          <input matInput formControlName="anchorText" />
        </mat-form-field>
      </mat-dialog-content>
      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
          {{ data.link ? 'Update' : 'Create' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .form-field {
      width: 100%;
      margin-bottom: 1rem;
    }

    mat-dialog-content {
      min-width: 500px;
      padding-top: 1rem;
    }
  `]
})
export class PageLinkForm {
  private readonly fb = inject(FormBuilder);
  private readonly pageLinkService = inject(PageLinkService);
  private readonly wikiPageService = inject(WikiPageService);
  private readonly dialog = inject(MatDialog);

  data = inject<{ link?: PageLink }>(MatDialog as any);

  wikiPages$ = this.wikiPageService.wikiPages$;

  form: FormGroup;

  constructor() {
    const link = this.data?.link;
    this.form = this.fb.group({
      sourcePageId: [link?.sourcePageId || '', Validators.required],
      targetPageId: [link?.targetPageId || '', Validators.required],
      anchorText: [link?.anchorText || '']
    });

    this.wikiPageService.getAll().subscribe();
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      if (this.data?.link) {
        this.pageLinkService.update({
          pageLinkId: this.data.link.pageLinkId,
          ...formValue
        }).subscribe(() => {
          this.dialog.closeAll();
        });
      } else {
        this.pageLinkService.create(formValue).subscribe(() => {
          this.dialog.closeAll();
        });
      }
    }
  }
}

@Component({
  selector: 'app-page-links',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './page-links.html',
  styleUrl: './page-links.scss'
})
export class PageLinks implements OnInit {
  private readonly pageLinkService = inject(PageLinkService);
  private readonly dialog = inject(MatDialog);

  pageLinks$ = this.pageLinkService.pageLinks$;
  displayedColumns = ['sourcePageId', 'targetPageId', 'anchorText', 'createdAt', 'actions'];

  ngOnInit(): void {
    this.pageLinkService.getAll().subscribe();
  }

  openDialog(link?: PageLink): void {
    this.dialog.open(PageLinkForm, {
      data: { link }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this page link?')) {
      this.pageLinkService.delete(id).subscribe();
    }
  }
}
