import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Injectable()
export class DataService {

  private url = "api/Data";


  constructor( private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + "api/Data";
  }

  
  getProducts(personal: boolean) {
    let httpAdress: string;
    if (personal)
    {
      httpAdress = "true";
      return this.http.get(this.url/*+'/GetPhones'*/ + '/' + httpAdress);
    }
    else
    {
      httpAdress = "false";
      return this.http.get(this.url/*+'/GetPhones'*/ + '/' + httpAdress);
    }
  }

  createProduct(product: Products) {
    let postedproduct: AddProduct = new AddProduct();
    postedproduct.name = product.name;
    postedproduct.about = product.about;
    postedproduct.cost = product.cost;
    postedproduct.designer = product.designer;
    return this.http.post(this.url, postedproduct);
  }
  updateProduct(_product: Products) {
    let product: ChangeProducts = new ChangeProducts;
    product.id = _product.id;
    product.designer = _product.designer;
    product.about = _product.about;
    product.cost = _product.cost;
    product.name = _product.name;
    return this.http.patch(this.url, _product);
  }
  deleteProduct(id: number) {
    return this.http.delete(this.url + '/' + id);
  }
  markProduct(id: number, p: Products) {
    this.http.put(this.url + '/' + id, p).toPromise();
  }
}

interface Products {
  id: number;
  name: string;
  cost: number;
  designer: string;
  about: string;
  favourite: boolean;
}

export class AddProduct {
  name: string;
  cost: number;
  designer: string;
  about: string;
}

export class ChangeProducts {
  id: number;
  name: string;
  cost: number;
  designer: string;
  about: string;
}

