import apiClient from './apiClient';
import type { Furniture } from '../models/furniture';
import { environment } from '../config/environment';

const createF = `${environment.apiUrl}/furniture/create`;
const editF = `${environment.apiUrl}/furniture/edit/`;
const allF = `${environment.apiUrl}/furniture/all`;
const detailsF = `${environment.apiUrl}/furniture/details/`;
const userF = `${environment.apiUrl}/furniture/user`;
const deleteF = `${environment.apiUrl}/furniture/delete/`;

export const furnitureService = {
  createFurniture: (data: Furniture) => {
    return apiClient.post(createF, data);
  },

  editFurniture: (id: number, data: Furniture) => {
    return apiClient.post(editF + id, data);
  },

  getAllFurnitures: () => {
    return apiClient.get<{items: Furniture[]} | Furniture[]>(allF);
  },

  getFurniture: (id: number) => {
    return apiClient.get<Furniture>(detailsF + id);
  },

  getFurnituresForUser: () => {
    return apiClient.get<{items: Furniture[]} | Furniture[]>(userF);
  },

  deleteFurniture: (id: number) => {
    return apiClient.delete<Furniture>(deleteF + id);
  }
};
