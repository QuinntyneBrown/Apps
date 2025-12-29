import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { StatCard } from './stat-card';

describe('StatCard', () => {
  let component: StatCard;
  let fixture: ComponentFixture<StatCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StatCard, NoopAnimationsModule]
    }).compileComponents();

    fixture = TestBed.createComponent(StatCard);
    component = fixture.componentInstance;
    component.icon = '🎬';
    component.value = 100;
    component.label = 'Movies Watched';
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display icon', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.stat-card__icon')?.textContent).toContain('🎬');
  });

  it('should display value', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.stat-card__value')?.textContent).toContain('100');
  });

  it('should display label', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.stat-card__label')?.textContent).toContain('Movies Watched');
  });

  it('should display change when provided', () => {
    component.change = '+10 vs last year';
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.stat-card__change')?.textContent).toContain('+10 vs last year');
  });

  it('should not display change when not provided', () => {
    component.change = undefined;
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.stat-card__change')).toBeFalsy();
  });

  it('should accept string value', () => {
    component.value = '4.5';
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.stat-card__value')?.textContent).toContain('4.5');
  });
});
