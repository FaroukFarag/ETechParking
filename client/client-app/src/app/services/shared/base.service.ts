import { HttpClient, HttpRequest, HttpHandler, HttpEvent, HttpEventType } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from '../../environments/environment';
import { MyInterceptor } from './../../my-interceptor.interceptor';
import { map, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs'; 

@Injectable({
  providedIn: 'root'
})
export class BaseService<T> {
  protected baseUrl: string;
  protected http = inject(HttpClient);
  private interceptor = new MyInterceptor();
  constructor() {
    this.baseUrl = `${environment.apiUrl}`;
  }
  private handleRequest(req: HttpRequest<any>): Observable<HttpEvent<any>> {
    return this.interceptor.intercept(req, {
      handle: (request: HttpRequest<any>) => this.http.request(request.method, request.url, {
        body: request.body,
        headers: request.headers,
        responseType: 'json',
        observe: 'events'
      })
    });
  }

  getAll(endpoint: string): Observable<T[]> {
    const req = new HttpRequest('GET', `${this.baseUrl}/${endpoint}`);
    return this.handleRequest(req).pipe(
      map(event => {
        if (event.type === HttpEventType.Response) {
          return event.body as T[]; 
        }
        return []; 
      })
    );
  }

  getAllPaginated(endpoint: string, paginatedModel: any): Observable<T[]> {
    return this.http.post<T[]>(`${this.baseUrl}/${endpoint}`, paginatedModel);
  }

  getById(endpoint: string, id: number | string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}/${id}`);
  }

  create(endpoint: string, entity: T): Observable<T | null> {
    const req = new HttpRequest('POST', `${this.baseUrl}/${endpoint}`, entity);
    return this.handleRequest(req).pipe(
      map(event => {
        if (event.type === HttpEventType.Response) {
          return event.body as T; 
        }
        return null; 
      }),
      catchError(error => {
        console.error('Error occurred during create:', error);
        return of(null); 
      })
    );
  }
  
  update(endpoint: string, entity: T): Observable<T | null> {
    const req = new HttpRequest('PUT', `${this.baseUrl}/${endpoint}`, entity);
    return this.handleRequest(req).pipe(
      map(event => {
        if (event.type === HttpEventType.Response) {
          return event.body as T; 
        }
        return null; 
      })
    );
  }

  delete(endpoint: string): Observable<T | null> {
    const req = new HttpRequest('DELETE', `${this.baseUrl}/${endpoint}`);
    return this.handleRequest(req).pipe(
      map(event => {
        if (event.type === HttpEventType.Response) {
          return event.body as T; 
        }
        return null; 
      })
    );
  }


  deleteRange(endpoint: string, entities: T[] | null): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/${endpoint}`, { body: entities });
  }


  getAllFiltered(endpoint: string, filters: any): Observable<any> {
    const req = new HttpRequest('POST', `${this.baseUrl}/${endpoint}`, filters);
    return this.handleRequest(req);
  }

  closeShift(url: string, data: any) {
    return this.http.post(url, data);
  }

 
  generateReport(endpoint: string): Observable<Blob | null> { // Change return type to Blob | null
    const req = new HttpRequest('GET', `${environment.apiUrl}/${endpoint}`, {
      responseType: 'blob'
    });
    return this.handleRequest(req).pipe(
      map(event => {
        if (event.type === HttpEventType.Response) {
          return event.body as Blob;
        }
        return null; // Return null for non-response events
      }),
      catchError(error => {
        console.error('Error occurred during report generation:', error);
        return of(null); // Return null or handle the error as needed
      })
    );
  }

}
