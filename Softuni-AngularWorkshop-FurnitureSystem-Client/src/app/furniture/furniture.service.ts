import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Furniture } from '../models/furniture';

const createF = 'http://localhost:5000/furniture/create';
const editF = 'http://localhost:5000/furniture/edit/';
const allF = 'http://localhost:5000/furniture/all';
const detailsF = 'http://localhost:5000/furniture/details/';
const userF = 'http://localhost:5000/furniture/user';
const deleteF = 'http://localhost:5000/furniture/delete/';

@Injectable({
  providedIn: 'root'
})
export class FurnitureService {
  constructor(private http: HttpClient) { }

  createFurniture(data: Furniture) {
    return this.http.post(createF, data);
  }

  editFurniture(id: number, data: Furniture) {
    return this.http.post(editF + id, data);
  }

  getAllFurnitures() {
    return this.http.get<Furniture[]>(allF);
  }

  getFurniture(id: number) {
    return this.http.get<Furniture>(detailsF + id);
  }

  getFurnituresForUser() {
    return this.http.get<Furniture[]>(userF);
  }

  deleteFurniture(id: number) {
    return this.http.delete<Furniture>(deleteF + id);
  }
}
