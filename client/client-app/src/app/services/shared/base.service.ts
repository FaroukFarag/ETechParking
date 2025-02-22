import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BaseService<T> {
  protected baseUrl: string;
  protected http = inject(HttpClient);

  constructor() {
    this.baseUrl = `${environment.apiUrl}`;
  }

  getAll(endpoint: string): Observable<T[]> {
    return this.http.get<T[]>(`${this.baseUrl}/${endpoint}`);
  }

  getAllPaginated(endpoint: string, paginatedModel: any): Observable<T[]> {
    return this.http.post<T[]>(`${this.baseUrl}/${endpoint}`, paginatedModel);
  }

  getById(endpoint: string, id: number | string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}/${id}`);
  }

  create(endpoint: string, entity: T): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${endpoint}`, entity);
  }

  update(endpoint: string, entity: T): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${endpoint}`, entity);
  }

  delete(endpoint: string): Observable<T> {
    return this.http.delete<T>(`${this.baseUrl}/${endpoint}`);
  }

  deleteRange(endpoint: string, entities: T[] | null): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/${endpoint}`, { body: entities });
  }

  getAllFiltered(endpoint:string,filters: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/${endpoint}`, filters);
  }

  closeShift(url: string, data: any) {
    return this.http.post(url, data);
  }

 
  generateReport(endpoint: string): Observable<Blob> {
    return this.http.get(`${environment.apiUrl}/${endpoint}`, {
      responseType: 'blob' // Ensure the response type is set to blob
    });
  }

}
