import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';


@Injectable()
export class RelationService {

  private url = "api/Relation";


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + "api/Relation";
  }

  getRelations() {
    return this.http.get(this.url);
  }

  checkRelation(rel: Relation) {
    this.http.post(this.url, rel).toPromise();
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

