import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Header } from './header';

describe('Header', () => {
  let component: Header;
  let fixture: ComponentFixture<Header>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Header, RouterTestingModule, NoopAnimationsModule]
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
    expect(compiled.textContent).toContain('Gift Idea Tracker');
  });

  it('should have navigation links', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Dashboard');
    expect(compiled.textContent).toContain('Recipients');
    expect(compiled.textContent).toContain('Ideas');
    expect(compiled.textContent).toContain('Purchases');
  });

  it('should emit addGiftIdeaClick when add button is clicked', () => {
    jest.spyOn(component.addGiftIdeaClick, 'emit');
    component.onAddGiftIdeaClick();
    expect(component.addGiftIdeaClick.emit).toHaveBeenCalled();
  });

  it('should have add idea button', () => {
    const compiled = fixture.nativeElement;
    const addButton = compiled.querySelector('button[mat-raised-button]');
    expect(addButton).toBeTruthy();
    expect(addButton.textContent).toContain('Add Idea');
  });
});
