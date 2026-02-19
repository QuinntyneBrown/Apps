import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Company, CreateCompany, UpdateCompany } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/companies`;

  private companiesSubject = new BehaviorSubject<Company[]>([]);
  public companies$ = this.companiesSubject.asObservable();

  getCompanies(): Observable<Company[]> {
    return this.http.get<Company[]>(this.baseUrl).pipe(
      tap(companies => this.companiesSubject.next(companies))
    );
  }

  getCompanyById(id: string): Observable<Company> {
    return this.http.get<Company>(`${this.baseUrl}/${id}`);
  }

  createCompany(company: CreateCompany): Observable<Company> {
    return this.http.post<Company>(this.baseUrl, company).pipe(
      tap(newCompany => {
        const current = this.companiesSubject.value;
        this.companiesSubject.next([...current, newCompany]);
      })
    );
  }

  updateCompany(company: UpdateCompany): Observable<Company> {
    return this.http.put<Company>(`${this.baseUrl}/${company.companyId}`, company).pipe(
      tap(updatedCompany => {
        const current = this.companiesSubject.value;
        const index = current.findIndex(c => c.companyId === updatedCompany.companyId);
        if (index !== -1) {
          current[index] = updatedCompany;
          this.companiesSubject.next([...current]);
        }
      })
    );
  }

  deleteCompany(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.companiesSubject.value;
        this.companiesSubject.next(current.filter(c => c.companyId !== id));
      })
    );
  }
}
