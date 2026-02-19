import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Sidebar } from './sidebar';

describe('Sidebar', () => {
  let component: Sidebar;
  let fixture: ComponentFixture<Sidebar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Sidebar, NoopAnimationsModule]
    }).compileComponents();

    fixture = TestBed.createComponent(Sidebar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have content types', () => {
    expect(component.contentTypes.length).toBe(2);
  });

  it('should have genres', () => {
    expect(component.genres.length).toBe(6);
  });

  it('should have moods', () => {
    expect(component.moods.length).toBe(4);
  });

  describe('isContentTypeChecked', () => {
    it('should return true for checked content type', () => {
      component.filter = { ...component.filter, contentTypes: ['movie'] };
      expect(component.isContentTypeChecked('movie')).toBe(true);
    });

    it('should return false for unchecked content type', () => {
      component.filter = { ...component.filter, contentTypes: ['movie'] };
      expect(component.isContentTypeChecked('tvshow')).toBe(false);
    });
  });

  describe('isGenreChecked', () => {
    it('should return true for checked genre', () => {
      component.filter = { ...component.filter, genres: ['action'] };
      expect(component.isGenreChecked('action')).toBe(true);
    });

    it('should return false for unchecked genre', () => {
      component.filter = { ...component.filter, genres: [] };
      expect(component.isGenreChecked('action')).toBe(false);
    });
  });

  describe('isMoodChecked', () => {
    it('should return true for checked mood', () => {
      component.filter = { ...component.filter, moods: ['relaxing'] };
      expect(component.isMoodChecked('relaxing')).toBe(true);
    });
  });

  describe('toggleContentType', () => {
    it('should emit filter change when toggling content type', () => {
      jest.spyOn(component.filterChange, 'emit');
      component.toggleContentType('movie');
      expect(component.filterChange.emit).toHaveBeenCalled();
    });
  });

  describe('toggleGenre', () => {
    it('should emit filter change when toggling genre', () => {
      jest.spyOn(component.filterChange, 'emit');
      component.toggleGenre('action');
      expect(component.filterChange.emit).toHaveBeenCalled();
    });
  });

  describe('toggleMood', () => {
    it('should emit filter change when toggling mood', () => {
      jest.spyOn(component.filterChange, 'emit');
      component.toggleMood('relaxing');
      expect(component.filterChange.emit).toHaveBeenCalled();
    });
  });

  describe('onClearFilters', () => {
    it('should emit clear filters event', () => {
      jest.spyOn(component.clearFilters, 'emit');
      component.onClearFilters();
      expect(component.clearFilters.emit).toHaveBeenCalled();
    });
  });
});
