import { Component, OnInit, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { FriendsApiService } from '../service/friends-apiservice/friends-api.service';
import { Friend } from '../models/friend';
import { History } from '../models/history';
import { ActiveLend } from '../models/active-lend';
import { SortingDataAccessor } from '../models/sortingDataAccessor';

@Component({
  selector: 'app-friends-page',
  templateUrl: './friends-page.component.html',
  styleUrls: ['./friends-page.component.css']
})
export class FriendsPageComponent implements OnInit {
  displayedColumns: string[] = ['Name', 'Contacts'];
  historyDisplayedColumns: string[] = ['Thing.Name', 'LendDate', 'ReturnDate', 'Comment'];
  lendsDisplayedColumns: string[] = ['Thing.Name', 'LendDate', 'Comment'];
  @ViewChildren(MatPaginator) paginators = new QueryList<MatPaginator>();
  @ViewChildren(MatSort) sorts = new QueryList<MatSort>();
  constructor(private api: FriendsApiService) { }

  private friends: MatTableDataSource<Friend>;
  private history: MatTableDataSource<History>;
  private lends: MatTableDataSource<ActiveLend>;
  private lendsFriendId: string;
  private selectedTab: number;
  public selectedFriend: Friend;

  ngOnInit() {
    this.getFriends();
  }

  private getFriends(): void {
    this.api.getFriends()
      .subscribe(fr => {
        this.friends = new MatTableDataSource(fr);
        this.friends.paginator = this.paginators.toArray()[0];
        this.friends.sort = this.sorts.toArray()[0];
        this.friends.sortingDataAccessor = SortingDataAccessor;
      });
  }

  private getLends(): void {
    this.api.getLends(this.selectedFriend.Id)
      .subscribe( lends => {
        this.lends = new MatTableDataSource(lends.ActiveLends);
        this.lends.paginator = this.paginators.toArray()[1];
        this.lends.sort = this.sorts.toArray()[1];
        this.history = new MatTableDataSource(lends.History);
        this.history.paginator = this.paginators.toArray()[2];
        this.history.sort = this.sorts.toArray()[2];
        this.lends.sortingDataAccessor = SortingDataAccessor;
        this.history.sortingDataAccessor = SortingDataAccessor;
      });
  }

  private add(Name: string, Contacts: string): void {
    Name = Name.trim();
    Contacts = Contacts.trim();
    if (!Name && !Contacts) {
      return;
    }
    this.api.addFriend({ Name, Contacts } as Friend)
      .subscribe(fr => {
        this.friends.data.push(fr);
        this.friends._updateChangeSubscription();
      });
  }

  private update(Id: string, Name: string, Contacts: string): void {
    Name = Name.trim();
    Contacts = Contacts.trim();
    if (!Name && !Contacts) {
      return;
    }
    this.api.updateFriend(Id, { Name, Contacts } as Friend)
      .subscribe(() => {
        const fr = this.friends.data.find(c => c.Id === Id);
        fr.Name = Name;
        fr.Contacts = Contacts;
        this.friends._updateChangeSubscription();
      });
  }

  private delete(Id: string): void {
    this.api.deleteFriend(Id)
    .subscribe( () => {
      const index = this.friends.data.findIndex(c => c.Id === Id);
      this.friends.data.splice(index, 1);
      this.friends._updateChangeSubscription();
    });
  }

  private onSelect(friend: Friend): void {
    this.selectedFriend = friend;
    if (this.selectedTab === 2) {
      this.onTabSelect(this.selectedTab);
    }
  }

  private onTabSelect(index: number): void {
    this.selectedTab = index;
    if (index === 2 && this.selectedFriend && this.lendsFriendId !== this.selectedFriend.Id) {
      this.getLends();
      this.lendsFriendId = this.selectedFriend.Id;
    }
  }
}
