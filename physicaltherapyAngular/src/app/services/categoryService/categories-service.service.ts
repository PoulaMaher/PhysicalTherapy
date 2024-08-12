
import { HttpClient } from '@angular/common/http'; 
import { Injectable } from '@angular/core'; 
import { Observable } from 'rxjs';  
import { environment } from '../../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoriesServiceService {
  constructor(private http: HttpClient) {} 
  getAllCategories(): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/api/Category/GetAllCategories`);
  }
}


 
