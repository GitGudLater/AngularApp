import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Injectable()

export class IdentificationService {

  private url = "api/Data";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + "api/Data";
  }

  getUser() {
    return this.http.get(this.url);
  }
}
