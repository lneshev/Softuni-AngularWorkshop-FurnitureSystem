import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly loginUrl = 'http://localhost:5000/auth/login';
  private readonly registerUrl = 'http://localhost:5000/auth/register';

  constructor(
    private http: HttpClient
  ) { }

  register(body: any) {
    return this.http.post(this.registerUrl, body);
  }

  login(body: any) {
    return this.http.post(this.loginUrl, body);
  }

  logout() {
    localStorage.clear();
  }

  isAuthenticated() {
    return this.getToken() !== null;
  }

  getToken() {
    return localStorage.getItem('token');
  }

  getUserFromToken() {
    var result = {
      id: "",
      userName: "",
      email: "",
      roles: [] as any[]
    };

    if (this.isAuthenticated()) {
      const accessToken = this.getToken()!;
      const decodedToken = jwtDecode<any>(accessToken);

      result.id = decodedToken["sub"];
      result.userName = decodedToken["unique_name"];
      result.email = decodedToken["email"];

      if (Array.isArray(decodedToken["role"])) {
        result.roles = decodedToken["role"].map((role, i) => {
          return role;
        });
      }
      else if (typeof decodedToken["role"] === "string") {
        result.roles = [decodedToken["role"]];
      }
    }

    console.log(result);
    
    return result;
  }
}