import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Artist, CreateArtist, UpdateArtist } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ArtistService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/artists`;

  private artistsSubject = new BehaviorSubject<Artist[]>([]);
  public artists$ = this.artistsSubject.asObservable();

  getArtists(): Observable<Artist[]> {
    return this.http.get<Artist[]>(this.baseUrl).pipe(
      tap(artists => this.artistsSubject.next(artists))
    );
  }

  getArtistById(id: string): Observable<Artist> {
    return this.http.get<Artist>(`${this.baseUrl}/${id}`);
  }

  createArtist(artist: CreateArtist): Observable<Artist> {
    return this.http.post<Artist>(this.baseUrl, artist).pipe(
      tap(newArtist => {
        const currentArtists = this.artistsSubject.value;
        this.artistsSubject.next([...currentArtists, newArtist]);
      })
    );
  }

  updateArtist(artist: UpdateArtist): Observable<Artist> {
    return this.http.put<Artist>(`${this.baseUrl}/${artist.artistId}`, artist).pipe(
      tap(updatedArtist => {
        const currentArtists = this.artistsSubject.value;
        const index = currentArtists.findIndex(a => a.artistId === updatedArtist.artistId);
        if (index !== -1) {
          currentArtists[index] = updatedArtist;
          this.artistsSubject.next([...currentArtists]);
        }
      })
    );
  }

  deleteArtist(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentArtists = this.artistsSubject.value;
        this.artistsSubject.next(currentArtists.filter(a => a.artistId !== id));
      })
    );
  }
}
