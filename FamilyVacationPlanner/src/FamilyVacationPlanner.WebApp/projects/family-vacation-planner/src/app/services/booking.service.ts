import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Booking, CreateBookingCommand, UpdateBookingCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private readonly apiUrl = `${environment.baseUrl}/api/bookings`;
  private bookingsSubject = new BehaviorSubject<Booking[]>([]);
  public bookings$ = this.bookingsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getBookings(tripId?: string): Observable<Booking[]> {
    const params = tripId ? { tripId } : {};
    return this.http.get<Booking[]>(this.apiUrl, { params }).pipe(
      tap(bookings => this.bookingsSubject.next(bookings))
    );
  }

  getBookingById(bookingId: string): Observable<Booking> {
    return this.http.get<Booking>(`${this.apiUrl}/${bookingId}`);
  }

  createBooking(command: CreateBookingCommand): Observable<Booking> {
    return this.http.post<Booking>(this.apiUrl, command).pipe(
      tap(booking => {
        const currentBookings = this.bookingsSubject.value;
        this.bookingsSubject.next([...currentBookings, booking]);
      })
    );
  }

  updateBooking(bookingId: string, command: UpdateBookingCommand): Observable<Booking> {
    return this.http.put<Booking>(`${this.apiUrl}/${bookingId}`, command).pipe(
      tap(updatedBooking => {
        const currentBookings = this.bookingsSubject.value;
        const index = currentBookings.findIndex(b => b.bookingId === bookingId);
        if (index !== -1) {
          currentBookings[index] = updatedBooking;
          this.bookingsSubject.next([...currentBookings]);
        }
      })
    );
  }

  deleteBooking(bookingId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${bookingId}`).pipe(
      tap(() => {
        const currentBookings = this.bookingsSubject.value;
        this.bookingsSubject.next(currentBookings.filter(b => b.bookingId !== bookingId));
      })
    );
  }
}
