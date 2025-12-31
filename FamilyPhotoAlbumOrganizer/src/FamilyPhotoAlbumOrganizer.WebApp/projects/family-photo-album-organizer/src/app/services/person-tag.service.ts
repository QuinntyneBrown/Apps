import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { PersonTag, CreatePersonTagCommand } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PersonTagService {
  private personTagsSubject = new BehaviorSubject<PersonTag[]>([]);
  public personTags$ = this.personTagsSubject.asObservable();

  private baseUrl = `${environment.baseUrl}/api/PersonTags`;

  constructor(private http: HttpClient) {}

  loadPersonTags(photoId?: string, personName?: string): void {
    let url = this.baseUrl;
    const params: string[] = [];

    if (photoId) params.push(`photoId=${photoId}`);
    if (personName) params.push(`personName=${personName}`);

    if (params.length > 0) {
      url += '?' + params.join('&');
    }

    this.http.get<PersonTag[]>(url).subscribe(personTags => {
      this.personTagsSubject.next(personTags);
    });
  }

  createPersonTag(command: CreatePersonTagCommand): Observable<PersonTag> {
    return this.http.post<PersonTag>(this.baseUrl, command);
  }

  deletePersonTag(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
