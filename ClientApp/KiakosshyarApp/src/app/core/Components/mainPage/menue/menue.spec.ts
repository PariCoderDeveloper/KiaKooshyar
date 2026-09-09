import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Menue } from './menue';

describe('Menue', () => {
  let component: Menue;
  let fixture: ComponentFixture<Menue>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Menue],
    }).compileComponents();

    fixture = TestBed.createComponent(Menue);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
