import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { HistoryApiService } from '../service/history-apiservice/history-api.service';
import { History } from '../models/history';

@Component({
  selector: 'app-history-page',
  templateUrl: './history-page.component.html',
  styleUrls: ['./history-page.component.css']
})
export class HistoryPageComponent implements OnInit {

  displayedColumns: string[] = ['Thing', 'Friend', 'LendDate', 'ReturnDate', 'Comment', 'DeleteRecord'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  public deleteIcon = '<i class="material-icons">delete</i>';
  private history: MatTableDataSource<History>;

  constructor(private api: HistoryApiService) { }

  ngOnInit() {
    this.getHistory();
  }

  private getHistory(): void {
    this.api.getHistory()
      .subscribe(hist => {
        this.history = new MatTableDataSource<History>(hist);
        this.history.paginator = this.paginator;
        this.history.sort = this.sort;
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
