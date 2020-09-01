import { Injectable, Inject } from '@angular/core';
import { FundDetail } from '../models/funddetail';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';//import { Http } from '@angular/http';


@Injectable()
export class FundDetailService {

  private readonly endpoint = '/api/FundDetail';

  private baseURLAndEndpoint = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { this.baseURLAndEndpoint = this.baseUrl + this.endpoint; }

  getFundDetailList(): Observable<FundDetail[]> {
    return this.http.get<FundDetail[]>(this.baseURLAndEndpoint + '/GetFundDetailList');
  }


  deleteFundDetail(isin: string): Observable<FundDetail[]> { 
    return this.http.post<FundDetail[]>(this.baseURLAndEndpoint + '/DeleteFundDetail/' + isin, isin);
  }

}
