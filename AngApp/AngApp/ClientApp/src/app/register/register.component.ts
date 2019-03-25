import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterService } from './register.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [RegisterService]
})
export class RegisterComponent implements OnInit {

  public account: Register = new Register;

  constructor(private regService: RegisterService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }

  ngOnInit() {
  }

  putReg() {
    this.regService.postLog(this.account);
  }

}


export class Register {
  public email: string;
  public password: string;
  public confirmPassword: string;
}
