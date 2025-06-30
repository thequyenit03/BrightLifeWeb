import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocterExpertsComponent } from './docter-experts.component';

describe('DocterExpertsComponent', () => {
  let component: DocterExpertsComponent;
  let fixture: ComponentFixture<DocterExpertsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocterExpertsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocterExpertsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
