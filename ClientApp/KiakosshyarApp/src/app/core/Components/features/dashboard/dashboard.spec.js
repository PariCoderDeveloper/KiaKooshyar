import { TestBed } from '@angular/core/testing';
import { Dashboard } from './dashboard';
describe('Dashboard', () => {
    let component;
    let fixture;
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [Dashboard],
        }).compileComponents();
        fixture = TestBed.createComponent(Dashboard);
        component = fixture.componentInstance;
        await fixture.whenStable();
    });
    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
