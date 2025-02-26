import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Locationsv2Component } from './locationsv2.component';

describe('Locationsv2Component', () => {
  let component: Locationsv2Component;
  let fixture: ComponentFixture<Locationsv2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Locationsv2Component]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Locationsv2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
