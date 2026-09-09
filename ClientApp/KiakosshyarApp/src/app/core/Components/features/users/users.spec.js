import { TestBed } from '@angular/core/testing';
import { Users } from './users';
describe('Users', () => {
    let component;
    let fixture;
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [Users],
        }).compileComponents();
        fixture = TestBed.createComponent(Users);
        component = fixture.componentInstance;
        await fixture.whenStable();
    });
    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
