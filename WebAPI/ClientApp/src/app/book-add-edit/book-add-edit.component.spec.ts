import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookAddEditComponent } from './book-add-edit.component';

describe('BookAddEditComponent', () => {
  let component: BookAddEditComponent;
  let fixture: ComponentFixture<BookAddEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookAddEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
