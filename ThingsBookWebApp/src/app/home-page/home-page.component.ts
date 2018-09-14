import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { ActiveLend } from '../models/active-lend';
import { LendsApiService } from '../service/lends-apiservice/lends-api.service';
import { AuthenticationService } from '../service/authentication/authentication.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  constructor(private lendsApi: LendsApiService, private authService: AuthenticationService) { }

  displayedColumns: string[] = ['Thing', 'Friend', 'LendDate', 'Comment'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  private activeLends: MatTableDataSource<ActiveLend>;
  public selectedLend: ActiveLend;

  ngOnInit() {
    if (this.authService.isAuthorized) {
      this.getLends();
    }
  }

  private getLends(): void {
    this.lendsApi.getLends()
      .subscribe(data => {
        this.activeLends = new MatTableDataSource(data);
        this.activeLends.paginator = this.paginator;
        this.activeLends.sort = this.sort;
      });
  }

  private onSelect(lend: ActiveLend): void {
    this.selectedLend = lend;
  }
}
