import apiClient from './apiClient';
import { jwtDecode } from 'jwt-decode';
import { environment } from '../config/environment';

const loginUrl = `${environment.apiUrl}/auth/login`;
const registerUrl = `${environment.apiUrl}/auth/register`;

export interface User {
  id: string;
  userName: string;
  email: string;
  roles: string[];
}

export const authService = {
  register: (body: any) => {
    return apiClient.post(registerUrl, body);
  },

  login: (body: any) => {
    return apiClient.post(loginUrl, body);
  },

  logout: () => {
    localStorage.clear();
  },

  isAuthenticated: () => {
    return authService.getToken() !== null;
  },

  getToken: () => {
    return localStorage.getItem('token');
  },

  getUserFromToken: (): User => {
    const result: User = {
      id: "",
      userName: "",
      email: "",
      roles: []
    };

    if (authService.isAuthenticated()) {
      const accessToken = authService.getToken()!;
      const decodedToken = jwtDecode<any>(accessToken);

      result.id = decodedToken["sub"];
      result.userName = decodedToken["unique_name"];
      result.email = decodedToken["email"];

      if (Array.isArray(decodedToken["role"])) {
        result.roles = decodedToken["role"].map((role: any) => {
          return role;
        });
      }
      else if (typeof decodedToken["role"] === "string") {
        result.roles = [decodedToken["role"]];
      }
    }

    return result;
  }
};