import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FaresComponent } from './fares.component';

describe('FaresComponent', () => {
  let component: FaresComponent;
  let fixture: ComponentFixture<FaresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FaresComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FaresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
