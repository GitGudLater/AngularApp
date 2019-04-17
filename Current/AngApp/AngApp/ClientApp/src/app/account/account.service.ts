import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';


@Injectable()
export class AccountService {
  private url = "api/Account";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + "api/Account";
  }

  postLog(login: Logg) {
    this.http.post(this.url, login).toPromise();
  }

  dislog() {
    var logout: string = "logout";
    this.http.post(this.url + '/' + logout, logout).toPromise();
  }

  putLog(reg: Register) {
    this.http.put(this.url, reg).toPromise();
  }
}

interface Logg {
  email: string;
  password: string;
}

interface Register {
  email: string;
  password: string;
  confirmPassword: string;
}
