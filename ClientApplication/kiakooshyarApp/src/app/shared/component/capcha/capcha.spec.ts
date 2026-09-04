import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Capcha } from './capcha';

describe('Capcha', () => {
  let component: Capcha;
  let fixture: ComponentFixture<Capcha>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Capcha],
    }).compileComponents();

    fixture = TestBed.createComponent(Capcha);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
