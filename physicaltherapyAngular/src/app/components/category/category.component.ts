import { Component, OnInit } from '@angular/core'; 
import { HttpClientModule } from '@angular/common/http'; 
import { RouterModule } from '@angular/router';
import { CategoriesServiceService } from '../../services/categoryService/categories-service.service';
 
@Component({
  selector: 'app-category',
  standalone: true,
  imports: [RouterModule, HttpClientModule],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  category: any[] = []; 
 
  constructor(private _service: CategoriesServiceService) {} 
 
  ngOnInit(): void { 
    this._service.getAllCategories().subscribe({ 
      next: (res) => { 
        console.log(res); 
        this.category = res; 
      }, 
      error: (err) => { 
        console.error(err); 
      }, 
    }); 
  } 
}


