import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {Product} from "../models/product";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClient: HttpClient) { }

  getProducts() : Observable<Product[]> {
    return this.httpClient.get<Product[]>("http://localhost:5200/get-products");
  }
}
