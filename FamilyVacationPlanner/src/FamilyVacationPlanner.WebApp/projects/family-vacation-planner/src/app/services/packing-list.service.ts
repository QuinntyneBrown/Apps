import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { PackingList, CreatePackingListCommand, UpdatePackingListCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PackingListService {
  private readonly apiUrl = `${environment.baseUrl}/api/packinglists`;
  private packingListsSubject = new BehaviorSubject<PackingList[]>([]);
  public packingLists$ = this.packingListsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getPackingLists(tripId?: string): Observable<PackingList[]> {
    const params = tripId ? { tripId } : {};
    return this.http.get<PackingList[]>(this.apiUrl, { params }).pipe(
      tap(packingLists => this.packingListsSubject.next(packingLists))
    );
  }

  getPackingListById(packingListId: string): Observable<PackingList> {
    return this.http.get<PackingList>(`${this.apiUrl}/${packingListId}`);
  }

  createPackingList(command: CreatePackingListCommand): Observable<PackingList> {
    return this.http.post<PackingList>(this.apiUrl, command).pipe(
      tap(packingList => {
        const currentPackingLists = this.packingListsSubject.value;
        this.packingListsSubject.next([...currentPackingLists, packingList]);
      })
    );
  }

  updatePackingList(packingListId: string, command: UpdatePackingListCommand): Observable<PackingList> {
    return this.http.put<PackingList>(`${this.apiUrl}/${packingListId}`, command).pipe(
      tap(updatedPackingList => {
        const currentPackingLists = this.packingListsSubject.value;
        const index = currentPackingLists.findIndex(p => p.packingListId === packingListId);
        if (index !== -1) {
          currentPackingLists[index] = updatedPackingList;
          this.packingListsSubject.next([...currentPackingLists]);
        }
      })
    );
  }

  deletePackingList(packingListId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${packingListId}`).pipe(
      tap(() => {
        const currentPackingLists = this.packingListsSubject.value;
        this.packingListsSubject.next(currentPackingLists.filter(p => p.packingListId !== packingListId));
      })
    );
  }
}
