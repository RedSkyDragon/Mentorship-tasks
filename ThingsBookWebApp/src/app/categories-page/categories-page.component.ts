import { Component, OnInit, ViewChild } from '@angular/core';
import { Category } from '../models/category';
import { ApiService } from '../service/apiservice/api.service';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { ThingWithLend } from '../models/thing-with-lend';

@Component({
  selector: 'app-categories-page',
  templateUrl: './categories-page.component.html',
  styleUrls: ['./categories-page.component.css']
})
export class CategoriesPageComponent implements OnInit {
  displayedColumns: string[] = ['Name', 'About'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private api: ApiService) { }

  private categories: MatTableDataSource<Category>;
  private things: MatTableDataSource<ThingWithLend>;
  private thingsCatId: string;
  private selectedTab: number;
  public selectedCategory: Category;

  ngOnInit() {
    this.getCategories();
  }

  private getCategories(): void {
    this.api.getCategories()
      .subscribe(cats => {
        this.categories = new MatTableDataSource<Category>(cats);
        this.categories.paginator = this.paginator;
        this.categories.sort = this.sort;
      });
  }

  private add(Name: string, About: string): void {
    Name = Name.trim();
    About = About.trim();
    if (!Name && !About) {
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
    this.api.updateCategory(Id, { Name, About } as Category)
      .subscribe( () => {
        const cat = this.categories.data.find(c => c.Id === Id);
        cat.Name = Name;
        cat.About = About;
        this.categories._updateChangeSubscription();
      });
  }

  private delete(Id: string): void {
    this.api.deleteCategory(Id)
      .subscribe( () => {
        const index = this.categories.data.findIndex(c => c.Id === Id);
        this.categories.data.splice(index, 1);
        this.categories._updateChangeSubscription();
      });
  }

  private getThings(Id: string): void {
    this.api.getThings(Id)
      .subscribe(th => {
        this.things = new MatTableDataSource<ThingWithLend>(th);
        this.thingsCatId = Id;
      });
  }

  private onSelect(category: Category): void {
    this.selectedCategory = category;
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
}
