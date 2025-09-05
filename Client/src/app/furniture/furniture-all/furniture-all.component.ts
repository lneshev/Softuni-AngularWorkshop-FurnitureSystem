import { Component, OnInit } from '@angular/core';
import { Furniture } from '../../models/furniture';
import { FurnitureService } from '../furniture.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../authentication/auth.service';

@Component({
    selector: 'app-furniture-all',
    imports: [CommonModule, RouterLink],
    templateUrl: './furniture-all.component.html',
    styleUrl: './furniture-all.component.css'
})
export class FurnitureAllComponent implements OnInit {
  furnitures: Furniture[]
  isUserAdmin: boolean;

  constructor(private furnitureService: FurnitureService, private authService: AuthService) {
  }

  ngOnInit(): void {
    this.getAllFurnitures();
    this.isUserAdmin = this.authService.getUserFromToken().roles[0] === 'Super Admin';
  }

  getAllFurnitures() {
    setTimeout(() => {
      this.furnitureService.getAllFurnitures().subscribe((data: any) => {
        this.furnitures = data.items;
      });
    }, 1000);
  }

  deleteFurniture(id: number) {
    this.furnitureService.deleteFurniture(id).subscribe((data) => {
      this.getAllFurnitures();
    });
  }
}