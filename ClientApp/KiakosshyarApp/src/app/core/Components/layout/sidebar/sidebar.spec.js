import { TestBed } from '@angular/core/testing';
import { Sidebar } from './sidebar';
describe('Sidebar', () => {
    let component;
    let fixture;
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [Sidebar],
        }).compileComponents();
        fixture = TestBed.createComponent(Sidebar);
        component = fixture.componentInstance;
        await fixture.whenStable();
    });
    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
