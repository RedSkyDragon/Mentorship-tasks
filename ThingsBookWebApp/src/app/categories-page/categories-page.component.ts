import { Component, OnInit } from '@angular/core';
import { Category } from '../models/category';
import { ApiService } from '../service/apiservice/api.service';

@Component({
  selector: 'app-categories-page',
  templateUrl: './categories-page.component.html',
  styleUrls: ['./categories-page.component.css']
})
export class CategoriesPageComponent implements OnInit {

  constructor(private api: ApiService) { }

  private categories: Category[];

  ngOnInit() {
    this.getCategories();
  }

  private getCategories(): void {
    this.api.getCategories()
      .subscribe(cats => this.categories = cats);
    console.log(this.categories);
  }
}
