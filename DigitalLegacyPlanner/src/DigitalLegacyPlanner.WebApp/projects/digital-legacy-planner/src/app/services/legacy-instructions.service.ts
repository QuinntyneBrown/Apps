import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { LegacyInstruction, CreateLegacyInstructionCommand, UpdateLegacyInstructionCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class LegacyInstructionsService {
  private readonly baseUrl = `${environment.baseUrl}/api/LegacyInstructions`;
  private instructionsSubject = new BehaviorSubject<LegacyInstruction[]>([]);
  public instructions$ = this.instructionsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(userId?: string, category?: string): Observable<LegacyInstruction[]> {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (category) params.push(`category=${category}`);

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<LegacyInstruction[]>(url).pipe(
      tap(instructions => this.instructionsSubject.next(instructions))
    );
  }

  getById(id: string): Observable<LegacyInstruction> {
    return this.http.get<LegacyInstruction>(`${this.baseUrl}/${id}`);
  }

  create(command: CreateLegacyInstructionCommand): Observable<LegacyInstruction> {
    return this.http.post<LegacyInstruction>(this.baseUrl, command).pipe(
      tap(instruction => {
        const current = this.instructionsSubject.value;
        this.instructionsSubject.next([...current, instruction]);
      })
    );
  }

  update(id: string, command: UpdateLegacyInstructionCommand): Observable<LegacyInstruction> {
    return this.http.put<LegacyInstruction>(`${this.baseUrl}/${id}`, command).pipe(
      tap(updated => {
        const current = this.instructionsSubject.value;
        const index = current.findIndex(i => i.legacyInstructionId === id);
        if (index !== -1) {
          current[index] = updated;
          this.instructionsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.instructionsSubject.value;
        this.instructionsSubject.next(current.filter(i => i.legacyInstructionId !== id));
      })
    );
  }
}
