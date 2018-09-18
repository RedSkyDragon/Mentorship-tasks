import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { ActiveLend } from '../models/active-lend';
import { LendsApiService } from '../service/lends-apiservice/lends-api.service';
import { AuthenticationService } from '../service/authentication/authentication.service';
import { SortingDataAccessor } from '../models/sortingDataAccessor';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  constructor(private lendsApi: LendsApiService, private authService: AuthenticationService) { }

  private displayedColumns: string[] = ['Thing.Name', 'Friend.Name', 'LendDate', 'Comment'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  private activeLends = new MatTableDataSource<ActiveLend>();
  private selectedLend: ActiveLend;
  private returnDate: Date;
  private isLoading = true;

  ngOnInit() {
    if (this.authService.isAuthorized) {
      this.getLends();
      console.log(this.activeLends);
      this.returnDate = new Date();
    }
  }

  private getLends(): void {
    this.lendsApi.getLends()
      .subscribe(data => {
        this.activeLends = new MatTableDataSource(data);
        this.activeLends.paginator = this.paginator;
        this.activeLends.sort = this.sort;
        this.activeLends.sortingDataAccessor = SortingDataAccessor;
        this.isLoading = false;
      });
  }

  private returnLend(): void {
    this.lendsApi.deleteLend(this.selectedLend.Thing.Id, this.returnDate)
      .subscribe(() => {
        const index = this.activeLends.data.findIndex(c => c.Thing.Id === this.selectedLend.Thing.Id);
        this.activeLends.data.splice(index, 1);
        this.activeLends._updateChangeSubscription();
        this.selectedLend = null;
      });
  }

  private onSelect(lend: ActiveLend): void {
    this.selectedLend = lend;
  }
}
