import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { ThingsApiService } from '../service/things-apiservice/things-api.service';
import { CategoriesApiService } from '../service/categories-apiservice/categories-api.service';
import { FriendsApiService } from '../service/friends-apiservice/friends-api.service';
import { ThingWithLend } from '../models/thing-with-lend';
import { FormControl} from '@angular/forms';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { Friend } from '../models/friend';
import { Category } from '../models/category';
import { Thing } from '../models/thing';
import { LendsApiService } from '../service/lends-apiservice/lends-api.service';
import { Lend } from '../models/lend';
import { History } from '../models/history';
import { SortingDataAccessor } from '../models/sortingDataAccessor';
import { ThingsFilter } from '../models/filters';

@Component({
  selector: 'app-things-page',
  templateUrl: './things-page.component.html',
  styleUrls: ['./things-page.component.css']
})
export class ThingsPageComponent implements OnInit {

  constructor(
    private api: ThingsApiService,
    private catApi: CategoriesApiService,
    private friendsApi: FriendsApiService,
    private lendsApi: LendsApiService) { }

  private displayedColumns: string[] = ['Name', 'About', 'Lended'];
  private historyDisplayedColumns: string[] = ['Friend.Name', 'LendDate', 'ReturnDate', 'Comment'];
  @ViewChildren(MatPaginator) paginators = new QueryList<MatPaginator>();
  @ViewChildren(MatSort) sorts = new QueryList<MatSort>();
  private things = new MatTableDataSource<ThingWithLend>();
  private history = new MatTableDataSource<History>();
  private friends: Friend[];
  private categories: Category[];
  private categoryControl = new FormControl('0');
  private selectedTab: number;
  private selectedThing: ThingWithLend;
  private selectedLendDate: Date;
  private returnDate: Date;
  private thingsLendId: string;
  private firstCategory: string;
  private firstFriend: string;
  private lendIcon = '<i class="material-icons"> check_circle</i>';
  private date = new FormControl(new Date());
  private isLoading = true;

  ngOnInit() {
    this.returnDate = new Date();
    this.getThings();
    this.getCategories();
    this.getFriends();
  }

  private getThings(): void {
    this.api.getThings()
      .subscribe(th => {
        this.things = new MatTableDataSource<ThingWithLend>(th);
        this.things.paginator = this.paginators.toArray()[0];
        this.things.sort = this.sorts.toArray()[0];
        this.things.sortingDataAccessor = SortingDataAccessor;
        this.things.filterPredicate = ThingsFilter;
        this.isLoading = false;
      });
  }

  private getThingsForCategory(Id: string): void {
    this.isLoading = true;
    this.catApi.getThings(Id)
      .subscribe(th => {
        this.things = new MatTableDataSource<ThingWithLend>(th);
        this.things.paginator = this.paginators.toArray()[0];
        this.things.sort = this.sorts.toArray()[0];
        this.things.sortingDataAccessor = SortingDataAccessor;
        this.things.filterPredicate = ThingsFilter;
        this.selectedThing = null;
        this.isLoading = false;
      });
  }

  private getCategories(): void {
    this.catApi.getCategories()
      .subscribe(cats => {
        this.categories = cats;
        this.firstCategory = this.categories[0].Id;
      });
  }

  private getFriends(): void {
    this.friendsApi.getFriends()
      .subscribe(fr => {
        this.friends = fr;
        this.firstFriend = this.friends[0].Id;
      });
  }

  private getHistory(Id: string): void {
    this.api.getThingLends(Id)
    .subscribe(hist => {
      this.history = new MatTableDataSource<History>(hist.History);
      this.history.paginator = this.paginators.toArray()[1];
      this.history.sort = this.sorts.toArray()[1];
      this.history.sortingDataAccessor = SortingDataAccessor;
    });
  }

  private add(Name: string, About: string, CategoryId: string): void {
    Name = Name.trim();
    About = About.trim();
    if (!Name && !About) {
      return;
    }
    this.api.addThing({ Name, About, CategoryId } as Thing)
      .subscribe(th => {
        if (CategoryId === this.categoryControl.value) {
          this.things.data.push(th);
          this.things._updateChangeSubscription();
        }
      });
  }

  private update(Id: string, Name: string, About: string, CategoryId: string): void {
    Name = Name.trim();
    About = About.trim();
    if (!Name && !About) {
      return;
    }
    this.api.updateThing(Id, { Name, About, CategoryId } as Thing)
      .subscribe( () => {
        const cat = this.things.data.find(c => c.Id === Id);
        cat.Name = Name;
        cat.About = About;
        this.things._updateChangeSubscription();
      });
  }

  private delete(Id: string): void {
    this.api.deleteThing(Id)
    .subscribe( () => {
      const index = this.things.data.findIndex(c => c.Id === Id);
      this.things.data.splice(index, 1);
      this.things._updateChangeSubscription();
      this.selectedThing = null;
      this.selectedLendDate = null;
    });
  }

  private onSelect(thing: ThingWithLend): void {
    this.selectedThing = thing;
    if (thing.Lend) {
      this.selectedLendDate = thing.Lend.LendDate;
    }
    if (this.selectedTab === 3) {
      this.onTabSelect(this.selectedTab);
    }
  }

  private onTabSelect(index: number): void {
    this.selectedTab = index;
    if (index === 3 && this.selectedThing) {
      this.getHistory(this.selectedThing.Id);
      this.thingsLendId = this.selectedThing.Id;
    }
  }

  private addLend(LendDate: Date, Comment: string, FriendId: string): void {
    this.lendsApi.addLend(this.selectedThing.Id, {FriendId, LendDate, Comment} as Lend)
      .subscribe((data) => {
        const index = this.things.data.findIndex(c => c.Id === data.Id);
        this.things.data.splice(index, 1, data);
        this.things._updateChangeSubscription();
        this.selectedThing = data;
        this.selectedLendDate = data.Lend.LendDate;
      });
  }

  private updateLend(LendDate: Date, Comment: string, FriendId: string): void {
    this.lendsApi.updateLend(this.selectedThing.Id, {FriendId, LendDate, Comment} as Lend)
      .subscribe((data) => {
        const index = this.things.data.findIndex(c => c.Id === data.Id);
        this.things.data.splice(index, 1, data);
        this.things._updateChangeSubscription();
        this.selectedThing = data;
      });
  }

  private deleteLend(): void {
    this.lendsApi.deleteLend(this.selectedThing.Id, this.returnDate)
      .subscribe(() => {
        this.selectedThing.Lend = null;
        this.selectedLendDate = null;
        const index = this.things.data.findIndex(c => c.Id === this.selectedThing.Id);
        this.things.data.splice(index, 1, this.selectedThing);
        this.things._updateChangeSubscription();
      });
  }

  private clearDateChanges(): void {
    this.selectedLendDate = this.selectedThing.Lend.LendDate;
  }

  private applyFilter(filterValue: string) {
    this.things.filter = filterValue.trim().toLowerCase();
    if (this.things.paginator) {
      this.things.paginator.firstPage();
    }
  }

  private onCategorySelect(value: string) {
    if (value === '0') {
      this.getThings();
    } else {
      this.getThingsForCategory(value);
    }
  }
}
