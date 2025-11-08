import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Convoir } from './convoir';

describe('Convoir', () => {
  let component: Convoir;
  let fixture: ComponentFixture<Convoir>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Convoir]
    }).compileComponents();

    fixture = TestBed.createComponent(Convoir);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
