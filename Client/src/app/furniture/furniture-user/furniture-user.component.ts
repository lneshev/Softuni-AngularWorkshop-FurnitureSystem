import { Component, OnInit } from '@angular/core';
import { Furniture } from '../../models/furniture';
import { FurnitureService } from '../furniture.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'app-furniture-user',
    imports: [CommonModule, RouterLink],
    templateUrl: './furniture-user.component.html',
    styleUrl: './furniture-user.component.css'
})
export class FurnitureUserComponent implements OnInit {
  furnitures: Furniture[]

  constructor(private furnitureService: FurnitureService) {
  }

  ngOnInit(): void {
    this.getFurnituresForUser();
  }

  getFurnituresForUser() {
    this.furnitureService.getFurnituresForUser().subscribe((data: any) => {
      this.furnitures = data.items;
    });
  }

  deleteFurniture(id: number) {
    this.furnitureService.deleteFurniture(id).subscribe((data) => {
      this.getFurnituresForUser();
    });
  }
}