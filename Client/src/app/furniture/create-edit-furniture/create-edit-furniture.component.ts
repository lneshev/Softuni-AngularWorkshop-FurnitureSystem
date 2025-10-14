
import { Component, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { FurnitureService } from '../furniture.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-create-furniture',
  imports: [ReactiveFormsModule],
  templateUrl: './create-edit-furniture.component.html',
  styleUrl: './create-edit-furniture.component.css'
})
export class CreateEditFurnitureComponent implements OnInit {
  id: number;
  form: any;
  loading: boolean;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private furnitureService: FurnitureService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.loading = false;
    this.form = this.fb.group({
      id: [0],
      make: ['', [Validators.required, Validators.minLength(4)]],
      model: ['', [Validators.required, Validators.minLength(4)]],
      year: [0, [Validators.required, Validators.min(1950), Validators.max(2050)]],
      description: ['', [Validators.required, Validators.minLength(10)]],
      price: [0, [Validators.required, Validators.min(0)]],
      image: ['', [Validators.required]],
      material: ['']
    });

    this.route.params.subscribe((data) => {
      this.id = data['id'];
      if (this.id) {
        this.loading = true;
        this.furnitureService.getFurniture(this.id).subscribe({
          next: (data) => {
            this.form.patchValue(data);
            this.loading = false;
          },
          error: () => {
            this.loading = false;
          }
        });
      }
    });
  }

  createFurniture() {
    console.log(this.form);
    this.furnitureService.createFurniture(this.form.value).subscribe((data) => {
      this.router.navigate(['/furniture/all'])
    });
  }

  editFurniture() {
    console.log(this.form);
    this.furnitureService.editFurniture(this.id, this.form.value).subscribe((data) => {
      this.router.navigate(['/furniture/all'])
    });
  }

  get f() {
    return this.form.controls;
  }
}
