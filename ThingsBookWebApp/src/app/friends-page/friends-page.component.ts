import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { FriendsApiService } from '../service/friends-apiservice/friends-api.service';
import { Friend } from '../models/friend';

@Component({
  selector: 'app-friends-page',
  templateUrl: './friends-page.component.html',
  styleUrls: ['./friends-page.component.css']
})
export class FriendsPageComponent implements OnInit {
  displayedColumns: string[] = ['Name', 'Contacts'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private api: FriendsApiService) { }

  private friends: MatTableDataSource<Friend>;
  // private lends: MatTableDataSource<ThingWithLend>;
  private lendsFriendId: string;
  private selectedTab: number;
  public selectedFriend: Friend;
  // public enableSelect = new FormControl(true);

  ngOnInit() {
    this.getFriends();
  }

  private getFriends(): void {
    this.api.getFriends()
      .subscribe(fr => {
        this.friends = new MatTableDataSource(fr);
        this.friends.paginator = this.paginator;
        this.friends.sort = this.sort;
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
      //
    }
  }
}
