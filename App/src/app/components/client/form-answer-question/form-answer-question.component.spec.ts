import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormAnswerQuestionComponent } from './form-answer-question.component';

describe('FormAnswerQuestionComponent', () => {
  let component: FormAnswerQuestionComponent;
  let fixture: ComponentFixture<FormAnswerQuestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormAnswerQuestionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FormAnswerQuestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
