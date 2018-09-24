import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { ActiveLend } from '../models/active-lend';
import { LendsApiService } from '../service/lends-apiservice/lends-api.service';
import { AuthenticationService } from '../service/authentication/authentication.service';
import { SortingDataAccessor } from '../models/sortingDataAccessor';
import { FormControl } from '@angular/forms';
import { Friend } from '../models/friend';
import { Thing } from '../models/thing';
import { ThingsApiService } from '../service/things-apiservice/things-api.service';
import { FriendsApiService } from '../service/friends-apiservice/friends-api.service';
import { Lend } from '../models/lend';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  constructor(
    private lendsApi: LendsApiService,
    private thingsApi: ThingsApiService,
    private friendsApi: FriendsApiService,
    private authService: AuthenticationService) { }

  private displayedColumns: string[] = ['Thing.Name', 'Friend.Name', 'LendDate', 'Comment'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  private activeLends = new MatTableDataSource<ActiveLend>();
  private selectedLend: ActiveLend;
  private returnDate: Date;
  private isLoading = true;
  private date = new FormControl(new Date());
  private friends: Friend[];
  private firstFriend: string;
  private things: Thing[];
  private firstThing: string;

  ngOnInit() {
    if (this.authService.isAuthorized) {
      this.getLends();
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
        this.getFriends();
        this.getThings();
        this.isLoading = false;
      });
  }

  private getFriends(): void {
    this.friendsApi.getFriends()
      .subscribe(data => {
        this.friends = data;
        this.firstFriend = this.friends[0].Id;
      });
  }

  private getThings(): void {
    this.thingsApi.getThings()
      .subscribe(data => {
        this.things = data.filter(th => !this.activeLends.data.find(l => l.Thing.Id === th.Id));
        this.firstThing = this.things[0].Id;
      });
  }

  private returnLend(): void {
    this.lendsApi.deleteLend(this.selectedLend.Thing.Id, this.returnDate)
      .subscribe(() => {
        const index = this.activeLends.data.findIndex(c => c.Thing.Id === this.selectedLend.Thing.Id);
        this.activeLends.data.splice(index, 1);
        this.activeLends._updateChangeSubscription();
        this.getThings();
        this.selectedLend = null;
      });
  }

  private addLend(FriendId: string, ThingId: string, LendDate: Date, Comment: string): void {
    this.lendsApi.addLend(ThingId, {FriendId, LendDate, Comment} as Lend)
      .subscribe(() => {
        this.isLoading = true;
        this.getLends();
      });
  }

  private onSelect(lend: ActiveLend): void {
    this.selectedLend = lend;
  }
}
