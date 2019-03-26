import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Injectable()
export class ChosenService {

  private sampleurl = "api/SampleData";
  private relationurl = "api/Relation";
  private chosenurl = "api/Chosen";



  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.sampleurl = baseUrl + "/api/SampleData";
    this.relationurl = baseUrl + "/api/Relation";
    this.chosenurl = baseUrl + "/api/Chosen";
  }


  getProducts() {
    return this.http.get(this.chosenurl);
  }


  getRelations() {
    return this.http.get(this.relationurl);
  }

  checkRelation(rel: Relation) {
    this.http.post(this.relationurl, rel).toPromise();
  }

  createProduct(product: Products) {
    return this.http.post(this.sampleurl, product);
  }
  updateProduct(product: Products) {

    return this.http.put(this.sampleurl + '/' + product.id, product);
  }
  deleteProduct(id: number) {
    return this.http.delete(this.sampleurl + '/' + id);
  }

}

interface Relation {
  userName: string;
  productId: number;
}

export class Relations {
  id: number;
  username: string;
  productid: number;
  checked: boolean;
}

interface Products {
  id: number;
  name: string;
  cost: number;
  designer: string;
  about: string;
}

