import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { Fund } from '../models/fund';
import { FundService } from '../services/fund.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public funds: any;

  public today: number = Date.now();

  dtOptions: DataTables.Settings = {};
  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  //dtTrigger: Subject = new Subject();

  constructor(private fundService: FundService) {
    fundService.getFundList().subscribe(res => this.funds = res);     
  }

  reFreshFundList() {
    this.fundService.updateFundList().subscribe(res => this.funds = res); 
  }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 25
    };
    //this.http.get('data/data.json')
    //  .map(this.extractData)
    //  .subscribe(persons => {
    //    this.persons = persons;
    //    // Calling the DT trigger to manually render the table
    //    this.dtTrigger.next();
    //  });
  }

  //ngOnDestroy(): void {
  //  // Do not forget to unsubscribe the event
  //  this.dtTrigger.unsubscribe();
  //}

  //private extractData(res: Response) {
  //  const body = res.json();
  //  return body.data || {};
  //}

}




