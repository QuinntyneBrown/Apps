import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MiniCalendar } from './mini-calendar';

describe('MiniCalendar', () => {
  let component: MiniCalendar;
  let fixture: ComponentFixture<MiniCalendar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MiniCalendar]
    }).compileComponents();

    fixture = TestBed.createComponent(MiniCalendar);
    component = fixture.componentInstance;
    component.selectedDate = new Date(2025, 11, 29);
    component.eventDates = [new Date(2025, 11, 25), new Date(2025, 11, 31)];
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize currentMonth on init', () => {
    component.ngOnInit();
    expect(component.currentMonth.getMonth()).toBe(11);
    expect(component.currentMonth.getFullYear()).toBe(2025);
  });

  it('should generate calendar days', () => {
    const days = component.getCalendarDays();
    expect(days.length).toBe(42);
  });

  it('should mark current month days correctly', () => {
    const days = component.getCalendarDays();
    const currentMonthDays = days.filter(d => d.isCurrentMonth);
    expect(currentMonthDays.length).toBe(31);
  });

  it('should mark today correctly', () => {
    const today = new Date();
    component.currentMonth = new Date(today.getFullYear(), today.getMonth(), 1);
    const days = component.getCalendarDays();
    const todayDay = days.find(d => d.isToday);
    if (component.currentMonth.getMonth() === today.getMonth()) {
      expect(todayDay).toBeTruthy();
      expect(todayDay?.dayNumber).toBe(today.getDate());
    }
  });

  it('should navigate to previous month', () => {
    jest.spyOn(component.monthChange, 'emit');
    component.previousMonth();
    expect(component.currentMonth.getMonth()).toBe(10);
    expect(component.monthChange.emit).toHaveBeenCalled();
  });

  it('should navigate to next month', () => {
    jest.spyOn(component.monthChange, 'emit');
    component.nextMonth();
    expect(component.currentMonth.getMonth()).toBe(0);
    expect(component.currentMonth.getFullYear()).toBe(2026);
    expect(component.monthChange.emit).toHaveBeenCalled();
  });

  it('should emit dateSelect when selecting a date', () => {
    jest.spyOn(component.dateSelect, 'emit');
    const day = component.getCalendarDays()[15];
    component.selectDate(day);
    expect(component.selectedDate).toEqual(day.date);
    expect(component.dateSelect.emit).toHaveBeenCalledWith(day.date);
  });

  it('should correctly identify selected date', () => {
    const days = component.getCalendarDays();
    const selectedDay = days.find(d => d.dayNumber === 29 && d.isCurrentMonth);
    expect(component.isSelected(selectedDay!)).toBe(true);
  });

  it('should mark days with events', () => {
    component.eventDates = [new Date(2025, 11, 25)];
    const days = component.getCalendarDays();
    const day25 = days.find(d => d.dayNumber === 25 && d.isCurrentMonth);
    expect(day25?.hasEvents).toBe(true);
  });

  it('should have weekDays array with 7 days', () => {
    expect(component.weekDays.length).toBe(7);
    expect(component.weekDays[0]).toBe('Su');
  });
});
