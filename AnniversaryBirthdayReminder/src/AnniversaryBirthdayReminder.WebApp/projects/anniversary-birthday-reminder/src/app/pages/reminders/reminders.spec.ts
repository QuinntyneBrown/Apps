import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { Reminders } from './reminders';
import { DeliveryChannel, ReminderSettings } from '../../models';

describe('Reminders', () => {
  let component: Reminders;
  let fixture: ComponentFixture<Reminders>;

  const mockSettings: ReminderSettings = {
    oneWeekBefore: true,
    threeDaysBefore: true,
    oneDayBefore: true,
    channels: [DeliveryChannel.Email, DeliveryChannel.Push]
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Reminders],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        provideAnimationsAsync()
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Reminders);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display title', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.reminders__title')?.textContent).toContain('Reminder Settings');
  });

  it('should update one week before setting', () => {
    component.updateOneWeekBefore(mockSettings, false);
    component.viewModel$.subscribe(vm => {
      expect(vm.settings.oneWeekBefore).toBe(false);
    });
  });

  it('should toggle channel', () => {
    component.toggleChannel(mockSettings, DeliveryChannel.SMS);
    component.viewModel$.subscribe(vm => {
      expect(vm.settings.channels).toContain(DeliveryChannel.SMS);
    });
  });

  it('should check if has channel', () => {
    expect(component.hasChannel(mockSettings, DeliveryChannel.Email)).toBe(true);
    expect(component.hasChannel(mockSettings, DeliveryChannel.SMS)).toBe(false);
  });
});
