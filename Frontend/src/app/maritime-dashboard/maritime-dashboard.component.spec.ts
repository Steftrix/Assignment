import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaritimeDashboardComponent } from './maritime-dashboard.component';

describe('MaritimeDashboardComponent', () => {
  let component: MaritimeDashboardComponent;
  let fixture: ComponentFixture<MaritimeDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaritimeDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MaritimeDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
