import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FurnitureService } from '../furniture.service';
import { Furniture } from '../../models/furniture';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-furniture-details',
    imports: [CommonModule],
    templateUrl: './furniture-details.component.html',
    styleUrl: './furniture-details.component.css'
})
export class FurnitureDetailsComponent implements OnInit {
  furniture: Furniture;

  constructor(private route: ActivatedRoute, private furnitureService: FurnitureService) {
  }

  ngOnInit(): void {
    this.route.params.subscribe((data) => {
      let id = data['id'];
      this.furnitureService.getFurniture(id).subscribe(data => {
        this.furniture = data;
      });
    });
  }
}
