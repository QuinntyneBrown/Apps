import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterModule } from '@angular/router';
import { Header } from './header';

describe('Header', () => {
  let component: Header;
  let fixture: ComponentFixture<Header>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Header, RouterModule.forRoot([])]
    }).compileComponents();

    fixture = TestBed.createComponent(Header);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display app title', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Family Calendar');
  });

  it('should have navigation links', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Dashboard');
    expect(compiled.textContent).toContain('Calendar');
    expect(compiled.textContent).toContain('Family Members');
  });

  it('should emit addEventClick when add button is clicked', () => {
    jest.spyOn(component.addEventClick, 'emit');
    component.onAddEventClick();
    expect(component.addEventClick.emit).toHaveBeenCalled();
  });

  it('should have Add Event button', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Add Event');
  });
});
