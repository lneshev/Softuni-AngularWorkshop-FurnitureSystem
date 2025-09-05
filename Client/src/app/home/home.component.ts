import { Component, OnInit } from '@angular/core';
import { AuthService } from '../authentication/auth.service';


@Component({
    selector: 'app-home',
    imports: [],
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  username: string;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    const user = this.authService.getUserFromToken();
    this.username = user.userName;
  }
}
