import { TestBed } from '@angular/core/testing';
import { Register } from './register';
describe('Register', () => {
    let component;
    let fixture;
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [Register],
        }).compileComponents();
        fixture = TestBed.createComponent(Register);
        component = fixture.componentInstance;
        await fixture.whenStable();
    });
    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
