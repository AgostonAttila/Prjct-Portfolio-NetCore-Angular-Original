
import { Injectable, Inject } from '@angular/core';
import { Fund } from '../models/fund';
import { FundDetail } from '../models/funddetail';
import { Management } from '../models/management';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';//import { Http } from '@angular/http';


@Injectable()
export class PortfolioService {

  private readonly portfolioEndpoint = '/api/SampleData';

  private baseURLAndEndpoint = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { this.baseURLAndEndpoint = this.baseUrl + this.portfolioEndpoint; }


  getFundList(): Observable<Fund[]> {
    return this.http.get<Fund[]>(this.baseURLAndEndpoint + '/GetFundList');
  }

  updateFundList(): Observable<Fund[]> {
    return this.http.get<Fund[]>(this.baseURLAndEndpoint + '/ReFreshFundList');
  }

  getFundDetailList(): Observable<FundDetail[]> {
    return this.http.get<FundDetail[]>(this.baseURLAndEndpoint + '/GetFundDetailList');
  }

  getManagementList(): Observable<Management[]> {
    return this.http.get<Management[]>(this.baseURLAndEndpoint + '/GetManagementList');
  }

  DeleteFundDetail(isin: string): Observable<FundDetail[]> {

    console.log(isin);

    return this.http.post<FundDetail[]>(this.baseURLAndEndpoint + '/DeleteFundDetail/' + isin , isin);
  }

  DeleteManagement(managementName: string): Observable<Management[]> {
    return this.http.post<Management[]>(this.baseURLAndEndpoint + '/DeleteManagement/' + managementName, managementName);
  }
}



