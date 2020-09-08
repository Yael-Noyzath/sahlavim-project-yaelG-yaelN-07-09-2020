import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AfternoonMainComponent } from './afternoon-main.component';

describe('AfternoonMainComponent', () => {
  let component: AfternoonMainComponent;
  let fixture: ComponentFixture<AfternoonMainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AfternoonMainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AfternoonMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
