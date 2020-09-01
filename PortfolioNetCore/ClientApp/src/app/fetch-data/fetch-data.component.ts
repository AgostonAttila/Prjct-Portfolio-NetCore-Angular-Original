import { Component, Inject } from '@angular/core';
import { Management } from '../models/management';
import { FundDetail } from '../models/funddetail';
import { ManagementService } from '../services/management.service';
import { FundDetailService } from '../services/fund.detail.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public managements: Management[];
  public fundDetails: FundDetail[];

  constructor(private fundDetailService: FundDetailService, private managementService: ManagementService) {
    fundDetailService.getFundDetailList().subscribe(res => { this.fundDetails = res }, error => console.error(error));
    managementService.getManagementList().subscribe(res => { this.managements = res }, error => console.error(error));
  }

  deleteFundDetail(fundDetail) {
    this.fundDetailService.deleteFundDetail(fundDetail).subscribe(res => { this.fundDetails = res }, error => console.error(error));
  }

  deleteManagement(managementName) {
    this.managementService.deleteManagement(managementName).subscribe(res => { this.managements = res }, error => console.error(error));
  }

}




