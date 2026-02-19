import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Beneficiary, CreateBeneficiary, UpdateBeneficiary } from '../models';

@Injectable({
  providedIn: 'root'
})
export class BeneficiaryService {
  private apiUrl = `${environment.baseUrl}/api/beneficiaries`;
  private beneficiariesSubject = new BehaviorSubject<Beneficiary[]>([]);
  public beneficiaries$ = this.beneficiariesSubject.asObservable();

  private selectedBeneficiarySubject = new BehaviorSubject<Beneficiary | null>(null);
  public selectedBeneficiary$ = this.selectedBeneficiarySubject.asObservable();

  constructor(private http: HttpClient) {}

  getBeneficiaries(planId?: string): Observable<Beneficiary[]> {
    const url = planId ? `${this.apiUrl}?planId=${planId}` : this.apiUrl;
    return this.http.get<Beneficiary[]>(url).pipe(
      tap(beneficiaries => this.beneficiariesSubject.next(beneficiaries))
    );
  }

  getBeneficiaryById(id: string): Observable<Beneficiary> {
    return this.http.get<Beneficiary>(`${this.apiUrl}/${id}`).pipe(
      tap(beneficiary => this.selectedBeneficiarySubject.next(beneficiary))
    );
  }

  createBeneficiary(beneficiary: CreateBeneficiary): Observable<Beneficiary> {
    return this.http.post<Beneficiary>(this.apiUrl, beneficiary).pipe(
      tap(newBeneficiary => {
        const currentBeneficiaries = this.beneficiariesSubject.value;
        this.beneficiariesSubject.next([...currentBeneficiaries, newBeneficiary]);
      })
    );
  }

  updateBeneficiary(id: string, beneficiary: UpdateBeneficiary): Observable<Beneficiary> {
    return this.http.put<Beneficiary>(`${this.apiUrl}/${id}`, beneficiary).pipe(
      tap(updatedBeneficiary => {
        const currentBeneficiaries = this.beneficiariesSubject.value;
        const index = currentBeneficiaries.findIndex(b => b.beneficiaryId === id);
        if (index !== -1) {
          currentBeneficiaries[index] = updatedBeneficiary;
          this.beneficiariesSubject.next([...currentBeneficiaries]);
        }
        if (this.selectedBeneficiarySubject.value?.beneficiaryId === id) {
          this.selectedBeneficiarySubject.next(updatedBeneficiary);
        }
      })
    );
  }

  deleteBeneficiary(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentBeneficiaries = this.beneficiariesSubject.value;
        this.beneficiariesSubject.next(currentBeneficiaries.filter(b => b.beneficiaryId !== id));
        if (this.selectedBeneficiarySubject.value?.beneficiaryId === id) {
          this.selectedBeneficiarySubject.next(null);
        }
      })
    );
  }

  clearSelectedBeneficiary(): void {
    this.selectedBeneficiarySubject.next(null);
  }
}
