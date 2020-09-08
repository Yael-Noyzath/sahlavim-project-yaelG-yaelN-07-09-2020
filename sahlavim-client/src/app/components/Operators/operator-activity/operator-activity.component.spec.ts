import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OperatorActivityComponent } from './operator-activity.component';

describe('OperatorActivityComponent', () => {
  let component: OperatorActivityComponent;
  let fixture: ComponentFixture<OperatorActivityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OperatorActivityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OperatorActivityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
