import { HttpClient, HttpRequest, HttpHandler, HttpEvent, HttpEventType, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from '../../environments/environment';
import { MyInterceptor } from './../../my-interceptor.interceptor';
import { map, catchError, filter } from 'rxjs/operators';
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

  getAllFiltered(endpoint: string, filters: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/${endpoint}`, filters);
  }




  //getAllFiltered(endpoint: string, filters: any): Observable<any> {
  //  const req = new HttpRequest('POST', `${this.baseUrl}/${endpoint}`, filters);
  //  return this.handleRequest(req);
  //}

  closeShift(endpoint: string, closeShiftData: any): Observable<T | null> {

    const req = new HttpRequest('POST', `${this.baseUrl}/${endpoint}`, closeShiftData);
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


  resetPassword(endpoint: string, data: any): Observable<T | null> {
    const req = new HttpRequest('POST', `${this.baseUrl}/${endpoint}`, data);
    return this.handleRequest(req).pipe(
      map(event => {
        if (event.type === HttpEventType.Response) {
          return event.body as T;
        }
        return null;
      }),
      catchError(error => {
        console.error('Error occurred during Reset Password:', error);
        return of(null);
      })
    );
  }



  generateReport(endpoint: string, filters: any): Observable<Blob | null> {
    return this.http.post(`${environment.apiUrl}/${endpoint}`, filters, {responseType: 'blob'})
  }

}
