import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../authentication/auth.service';
import { CommonModule } from '@angular/common';
import { DropdownDirective } from './dropdown.directive';
import { CollapseDirective } from './collapse.directive';

@Component({
    selector: 'app-navigation',
    templateUrl: './navigation.component.html',
    styleUrls: ['./navigation.component.css'],
    imports: [CommonModule, RouterModule, DropdownDirective, CollapseDirective]
})
export class NavigationComponent implements OnInit {
  constructor(
    public authService: AuthService,
    private router: Router
  ) {  }

  ngOnInit() {
  }

  logout() {
    this.authService.logout();
    
    this.router.navigate([ '/signin' ]);
  }
}
