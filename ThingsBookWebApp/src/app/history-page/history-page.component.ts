import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { HistoryApiService } from '../service/history-apiservice/history-api.service';
import { History } from '../models/history';
import { SortingDataAccessor } from '../models/sortingDataAccessor';

@Component({
  selector: 'app-history-page',
  templateUrl: './history-page.component.html',
  styleUrls: ['./history-page.component.css']
})
export class HistoryPageComponent implements OnInit {

  constructor(private api: HistoryApiService) { }

  private displayedColumns: string[] = ['Thing.Name', 'Friend.Name', 'LendDate', 'ReturnDate', 'Comment', 'DeleteRecord'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  private deleteIcon = '<i class="material-icons">delete</i>';
  private history = new MatTableDataSource<History>();
  private isLoading = true;

  ngOnInit() {
    this.getHistory();
  }

  private getHistory(): void {
    this.api.getHistory()
      .subscribe(hist => {
        this.history = new MatTableDataSource<History>(hist);
        this.history.paginator = this.paginator;
        this.history.sort = this.sort;
        this.history.sortingDataAccessor = SortingDataAccessor;
        this.isLoading = false;
      });
  }

  private delete(Id: string): void {
    this.api.deleteHistory(Id)
      .subscribe( () => {
        const index = this.history.data.findIndex(h => h.Id === Id);
        this.history.data.splice(index, 1);
        this.history._updateChangeSubscription();
      });
  }

}
