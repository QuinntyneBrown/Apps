import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

interface CalendarDay {
  date: Date;
  dayNumber: number;
  isCurrentMonth: boolean;
  isToday: boolean;
  hasEvents: boolean;
}

@Component({
  selector: 'app-mini-calendar',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatButtonModule, DatePipe],
  templateUrl: './mini-calendar.html',
  styleUrl: './mini-calendar.scss'
})
export class MiniCalendar {
  @Input() selectedDate: Date = new Date();
  @Input() eventDates: Date[] = [];
  @Output() dateSelect = new EventEmitter<Date>();
  @Output() monthChange = new EventEmitter<Date>();

  currentMonth: Date = new Date();
  weekDays = ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'];

  ngOnInit(): void {
    this.currentMonth = new Date(this.selectedDate.getFullYear(), this.selectedDate.getMonth(), 1);
  }

  getCalendarDays(): CalendarDay[] {
    const days: CalendarDay[] = [];
    const year = this.currentMonth.getFullYear();
    const month = this.currentMonth.getMonth();
    const today = new Date();

    const firstDay = new Date(year, month, 1);
    const lastDay = new Date(year, month + 1, 0);
    const startDay = firstDay.getDay();
    const totalDays = lastDay.getDate();

    const prevMonthLastDay = new Date(year, month, 0).getDate();
    for (let i = startDay - 1; i >= 0; i--) {
      const date = new Date(year, month - 1, prevMonthLastDay - i);
      days.push({
        date,
        dayNumber: prevMonthLastDay - i,
        isCurrentMonth: false,
        isToday: this.isSameDay(date, today),
        hasEvents: this.hasEventsOnDate(date)
      });
    }

    for (let i = 1; i <= totalDays; i++) {
      const date = new Date(year, month, i);
      days.push({
        date,
        dayNumber: i,
        isCurrentMonth: true,
        isToday: this.isSameDay(date, today),
        hasEvents: this.hasEventsOnDate(date)
      });
    }

    const remainingDays = 42 - days.length;
    for (let i = 1; i <= remainingDays; i++) {
      const date = new Date(year, month + 1, i);
      days.push({
        date,
        dayNumber: i,
        isCurrentMonth: false,
        isToday: this.isSameDay(date, today),
        hasEvents: this.hasEventsOnDate(date)
      });
    }

    return days;
  }

  previousMonth(): void {
    this.currentMonth = new Date(
      this.currentMonth.getFullYear(),
      this.currentMonth.getMonth() - 1,
      1
    );
    this.monthChange.emit(this.currentMonth);
  }

  nextMonth(): void {
    this.currentMonth = new Date(
      this.currentMonth.getFullYear(),
      this.currentMonth.getMonth() + 1,
      1
    );
    this.monthChange.emit(this.currentMonth);
  }

  selectDate(day: CalendarDay): void {
    this.selectedDate = day.date;
    this.dateSelect.emit(day.date);
  }

  isSelected(day: CalendarDay): boolean {
    return this.isSameDay(day.date, this.selectedDate);
  }

  private isSameDay(date1: Date, date2: Date): boolean {
    return (
      date1.getFullYear() === date2.getFullYear() &&
      date1.getMonth() === date2.getMonth() &&
      date1.getDate() === date2.getDate()
    );
  }

  private hasEventsOnDate(date: Date): boolean {
    return this.eventDates.some(eventDate => this.isSameDay(new Date(eventDate), date));
  }
}
