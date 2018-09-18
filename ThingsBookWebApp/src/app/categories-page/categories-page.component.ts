import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { Category } from '../models/category';
import { CategoriesApiService } from '../service/categories-apiservice/categories-api.service';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { ThingWithLend } from '../models/thing-with-lend';
import { FormControl } from '@angular/forms';
import { SortingDataAccessor } from '../models/sortingDataAccessor';
import { AuthenticationService } from '../service/authentication/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-categories-page',
  templateUrl: './categories-page.component.html',
  styleUrls: ['./categories-page.component.css']
})
export class CategoriesPageComponent implements OnInit {

  constructor(private api: CategoriesApiService, private authService: AuthenticationService, private router: Router) { }

  private displayedColumns: string[] = ['Name', 'About'];
  @ViewChildren(MatPaginator) paginators = new QueryList<MatPaginator>();
  @ViewChildren(MatSort) sorts = new QueryList<MatSort>();
  private categories = new MatTableDataSource<Category>();
  private things = new MatTableDataSource<ThingWithLend>();
  private thingsCatId: string;
  private selectedTab: number;
  private selectedCategory: Category;
  private enableSelect = new FormControl(true);
  private replacement: Category[];
  private replace: string;
  private isLoading = true;

  ngOnInit() {
    this.getCategories();
  }

  private getCategories(): void {
    if (this.checkExpiration()) {
      return;
    }
    this.api.getCategories()
      .subscribe(cats => {
        this.categories = new MatTableDataSource<Category>(cats);
        this.categories.paginator = this.paginators.toArray()[0];
        this.categories.sort = this.sorts.toArray()[0];
        this.categories.sortingDataAccessor = SortingDataAccessor;
        this.isLoading = false;
      });
  }

  private add(Name: string, About: string): void {
    Name = Name.trim();
    About = About.trim();
    if (!Name && !About) {
      return;
    }
    if (this.checkExpiration()) {
      return;
    }
    this.api.addCategory({ Name, About } as Category)
      .subscribe(cat => {
        this.categories.data.push(cat);
        this.categories._updateChangeSubscription();
      });
  }

  private update(Id: string, Name: string, About: string): void {
    Name = Name.trim();
    About = About.trim();
    if (!Name && !About) {
      return;
    }
    if (this.checkExpiration()) {
      return;
    }
    this.api.updateCategory(Id, { Name, About } as Category)
      .subscribe( () => {
        const cat = this.categories.data.find(c => c.Id === Id);
        cat.Name = Name;
        cat.About = About;
        this.categories._updateChangeSubscription();
      });
  }

  private delete(Id: string, replace: boolean): void {
    if (this.checkExpiration()) {
      return;
    }
    if (replace) {
      this.api.deleteAndReplaceCategory(Id, this.replace)
      .subscribe( () => {
        const index = this.categories.data.findIndex(c => c.Id === Id);
        this.categories.data.splice(index, 1);
        this.categories._updateChangeSubscription();
        this.getThings(Id);
      });
    } else {
      this.api.deleteCategory(Id)
      .subscribe( () => {
        const index = this.categories.data.findIndex(c => c.Id === Id);
        this.categories.data.splice(index, 1);
        this.categories._updateChangeSubscription();
      });
    }
  }

  private getThings(Id: string): void {
    if (this.checkExpiration()) {
      return;
    }
    this.api.getThings(Id)
      .subscribe(th => {
        this.things = new MatTableDataSource<ThingWithLend>(th);
        this.things.paginator = this.paginators.toArray()[1];
        this.things.sort = this.sorts.toArray()[1];
        this.things.sortingDataAccessor = SortingDataAccessor;
        this.thingsCatId = Id;
      });
  }

  private onSelect(category: Category): void {
    this.selectedCategory = category;
    this.replacement = this.categories.data.filter(c => c.Id !== this.selectedCategory.Id);
    this.replace = this.replacement[0].Id;
    if (this.selectedTab === 2) {
      this.onTabSelect(this.selectedTab);
    }
  }

  private onTabSelect(index: number): void {
    this.selectedTab = index;
    if (index === 2 && this.selectedCategory && this.thingsCatId !== this.selectedCategory.Id) {
      this.getThings(this.selectedCategory.Id);
    }
  }

  private checkExpiration(): boolean {
    if (this.authService.hasExpired) {
      this.authService.login(this.router.url);
      return true;
    }
    return false;
  }
}
