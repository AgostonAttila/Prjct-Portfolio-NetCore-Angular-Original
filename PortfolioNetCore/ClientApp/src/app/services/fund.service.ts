import { Injectable, Inject } from '@angular/core';
import { Fund } from '../models/fund';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';//import { Http } from '@angular/http';


@Injectable()
export class FundService {

  private readonly endpoint = '/api/Fund';

  private baseURLAndEndpoint = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { this.baseURLAndEndpoint = this.baseUrl + this.endpoint; }


  getFundList(): Observable<Fund[]> {
    return this.http.get<Fund[]>(this.baseURLAndEndpoint + '/GetFundList');
  }

  updateFundList(): Observable<Fund[]> {
    return this.http.get<Fund[]>(this.baseURLAndEndpoint + '/ReFreshFundList');
  }


}
