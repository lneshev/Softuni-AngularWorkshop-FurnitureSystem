import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditFurnitureComponent } from './create-edit-furniture.component';

describe('CreateEditFurnitureComponent', () => {
  let component: CreateEditFurnitureComponent;
  let fixture: ComponentFixture<CreateEditFurnitureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateEditFurnitureComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateEditFurnitureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
