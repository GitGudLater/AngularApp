import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { DataService } from './fetch-data/data.service';
import { AccountComponent } from './account/account.component';
import { RegisterComponent } from './register/register.component';
import { AccountService } from './account/account.service';
import { RegisterService } from './register/register.service';
import { IdentificationService } from './fetch-data/identification.service';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AccountComponent,
    RegisterComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'app-account', component: AccountComponent },
      { path: 'app-register', component: RegisterComponent },
    ])
  ],
  providers: [DataService, AccountService, RegisterService, IdentificationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
