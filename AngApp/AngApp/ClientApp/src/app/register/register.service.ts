import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';


@Injectable()
export class RegisterService {

  private url = "api/Account";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + "api/Account";
  }

  postLog(reg: Register) {
    this.http.put(this.url, reg).toPromise();
  }
}

interface Register {
  email: string;
  password: string;
  confirmPassword: string;
}
