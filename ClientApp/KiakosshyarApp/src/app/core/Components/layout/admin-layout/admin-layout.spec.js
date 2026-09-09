import { TestBed } from '@angular/core/testing';
import { AdminLayout } from './admin-layout';
describe('AdminLayout', () => {
    let component;
    let fixture;
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [AdminLayout],
        }).compileComponents();
        fixture = TestBed.createComponent(AdminLayout);
        component = fixture.componentInstance;
        await fixture.whenStable();
    });
    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
