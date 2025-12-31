import { TestBed } from '@angular/core/testing';
import { AppRoot } from './app.component';

describe('AppRoot', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppRoot],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppRoot);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have the 'Gift Registry Organizer' title`, () => {
    const fixture = TestBed.createComponent(AppRoot);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('Gift Registry Organizer');
  });
});
