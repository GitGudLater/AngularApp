import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Product } from './fetch-data.component';

@Injectable()
export class DataService {

  private url = "api/SampleData";


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + "/api/SampleData";
  }


  getProducts() {
    return this.http.get(this.url);
  }

  createProduct(product: Products) {
    return this.http.post(this.url, product);
  }
  updateProduct(product: Products) {

    return this.http.put(this.url + '/' + product.id, product);
  }
  deleteProduct(id: number) {
    return this.http.delete(this.url + '/' + id);
  }
}

interface Products {
  id: number;
  name: string;
  cost: number;
  designer: string;
  about: string;
}

