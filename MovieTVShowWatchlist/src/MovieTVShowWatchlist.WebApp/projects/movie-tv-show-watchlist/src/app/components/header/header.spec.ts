import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Header } from './header';

describe('Header', () => {
  let component: Header;
  let fixture: ComponentFixture<Header>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Header, NoopAnimationsModule],
      providers: [provideRouter([])]
    }).compileComponents();

    fixture = TestBed.createComponent(Header);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have navigation links', () => {
    expect(component.navLinks.length).toBe(4);
  });

  it('should have watchlist link', () => {
    expect(component.navLinks.find(l => l.path === '/watchlist')).toBeTruthy();
  });

  it('should have history link', () => {
    expect(component.navLinks.find(l => l.path === '/history')).toBeTruthy();
  });

  it('should have statistics link', () => {
    expect(component.navLinks.find(l => l.path === '/statistics')).toBeTruthy();
  });

  it('should have discover link', () => {
    expect(component.navLinks.find(l => l.path === '/discover')).toBeTruthy();
  });

  it('should render logo', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.header__logo')?.textContent).toContain('WatchTrack');
  });

  it('should render navigation links', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    const links = compiled.querySelectorAll('.header__nav-link');
    expect(links.length).toBe(4);
  });
});
