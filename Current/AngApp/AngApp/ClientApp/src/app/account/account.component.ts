import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AccountService } from './account.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css'],
  providers: [AccountService]
})
export class AccountComponent implements OnInit {

  public account: Logg = new Logg;

  public regAccount: Register = new Register;

  registration: boolean = false;


  constructor(private loggService: AccountService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }

  ngOnInit() {
  }

  toggleRegistrator() {
    this.registration = !this.registration;
  }

  dislog() {
    this.loggService.dislog();
  }

  postLog() {
    this.loggService.postLog(this.account);
  }

  putReg() {
    this.loggService.putLog(this.regAccount);
  }

}

export class Logg {
  public email: string;
  public password: string;
}

export class Register {
  public email: string;
  public password: string;
  public confirmPassword: string;
}
