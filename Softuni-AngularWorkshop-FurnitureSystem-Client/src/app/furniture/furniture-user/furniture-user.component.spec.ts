import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FurnitureUserComponent } from './furniture-user.component';

describe('FurnitureUserComponent', () => {
  let component: FurnitureUserComponent;
  let fixture: ComponentFixture<FurnitureUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FurnitureUserComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FurnitureUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
