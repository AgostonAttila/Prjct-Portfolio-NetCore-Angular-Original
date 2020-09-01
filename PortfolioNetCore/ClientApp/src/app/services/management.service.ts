import { Injectable, Inject } from '@angular/core';
import { Management } from '../models/management';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';//import { Http } from '@angular/http';


@Injectable()
export class ManagementService {

  private readonly endpoint = '/api/Management';

  private baseURLAndEndpoint = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { this.baseURLAndEndpoint = this.baseUrl + this.endpoint; }
  
  getManagementList(): Observable<Management[]> {
    return this.http.get<Management[]>(this.baseURLAndEndpoint + '/GetManagementList');
  }

  deleteManagement(managementName: string): Observable<Management[]> {
    return this.http.post<Management[]>(this.baseURLAndEndpoint + '/DeleteManagement/' + managementName, managementName);
  }
}
