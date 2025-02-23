import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketsReportComponent } from './tickets-report.component';

describe('TicketsReportComponent', () => {
  let component: TicketsReportComponent;
  let fixture: ComponentFixture<TicketsReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketsReportComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TicketsReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
