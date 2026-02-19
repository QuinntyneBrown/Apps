import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';
import { environment } from '../environments';
import { Neighbor, CreateNeighbor, UpdateNeighbor } from '../models';

@Injectable({
  providedIn: 'root'
})
export class NeighborService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/neighbors`;

  private neighborsSubject = new BehaviorSubject<Neighbor[]>([]);
  neighbors$ = this.neighborsSubject.asObservable();

  private selectedNeighborSubject = new BehaviorSubject<Neighbor | null>(null);
  selectedNeighbor$ = this.selectedNeighborSubject.asObservable();

  getAll() {
    return this.http.get<Neighbor[]>(this.baseUrl).pipe(
      tap(neighbors => this.neighborsSubject.next(neighbors))
    );
  }

  getById(id: string) {
    return this.http.get<Neighbor>(`${this.baseUrl}/${id}`).pipe(
      tap(neighbor => this.selectedNeighborSubject.next(neighbor))
    );
  }

  create(neighbor: CreateNeighbor) {
    return this.http.post<Neighbor>(this.baseUrl, neighbor).pipe(
      tap(newNeighbor => {
        const neighbors = this.neighborsSubject.value;
        this.neighborsSubject.next([...neighbors, newNeighbor]);
      })
    );
  }

  update(neighbor: UpdateNeighbor) {
    return this.http.put<Neighbor>(`${this.baseUrl}/${neighbor.neighborId}`, neighbor).pipe(
      tap(updatedNeighbor => {
        const neighbors = this.neighborsSubject.value.map(n =>
          n.neighborId === updatedNeighbor.neighborId ? updatedNeighbor : n
        );
        this.neighborsSubject.next(neighbors);
        this.selectedNeighborSubject.next(updatedNeighbor);
      })
    );
  }

  delete(id: string) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const neighbors = this.neighborsSubject.value.filter(n => n.neighborId !== id);
        this.neighborsSubject.next(neighbors);
        if (this.selectedNeighborSubject.value?.neighborId === id) {
          this.selectedNeighborSubject.next(null);
        }
      })
    );
  }
}
