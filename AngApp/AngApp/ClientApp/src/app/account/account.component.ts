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

  constructor(private loggService: AccountService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }

  ngOnInit() {
  }

  postLog() {
    this.loggService.postLog(this.account);
  }

}

export class Logg {
  public email: string;
  public password: string;
}
