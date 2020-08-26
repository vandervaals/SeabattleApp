/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { BattleareaComponent } from './battlearea.component';

describe('BattleareaComponent', () => {
  let component: BattleareaComponent;
  let fixture: ComponentFixture<BattleareaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BattleareaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BattleareaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
