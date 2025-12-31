import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { combineLatest, map } from 'rxjs';
import { BillsService, PayeesService, PaymentsService } from '../../services';
import { BillCard } from '../../components';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, RouterLink, BillCard],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {
  private readonly _billsService = inject(BillsService);
  private readonly _payeesService = inject(PayeesService);
  private readonly _paymentsService = inject(PaymentsService);

  viewModel$ = combineLatest([
    this._billsService.bills$,
    this._payeesService.payees$,
    this._paymentsService.payments$,
  ]).pipe(
    map(([bills, payees, payments]) => ({
      upcomingBills: bills.filter(b => b.status === 'Pending').slice(0, 5),
      overdueBills: bills.filter(b => b.status === 'Overdue'),
      recentPayments: payments.slice(0, 5),
      totalBills: bills.length,
      totalPayees: payees.length,
      totalPayments: payments.length,
      monthlyTotal: bills
        .filter(b => b.status === 'Pending')
        .reduce((sum, b) => sum + b.amount, 0),
    }))
  );

  ngOnInit(): void {
    this._billsService.getAll().subscribe();
    this._payeesService.getAll().subscribe();
    this._paymentsService.getAll().subscribe();
  }
}
