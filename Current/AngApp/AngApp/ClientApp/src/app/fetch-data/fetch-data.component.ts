import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataService } from './data.service';
import { IdentificationService } from './identification.service';
import { Products } from './models/Products';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  providers: [DataService]
})
export class FetchDataComponent {
  public product: Products;   // изменяемый товар
  public products: Products[];

  tableMode: boolean = true;
  personal: boolean = false;
  userSigned: boolean = false;
  username: string = null;

  constructor(private identificationService: IdentificationService, private dataService: DataService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }

  ngOnInit() {
    //rename
    this.getUserName();
    this.loadProducts();
  }

  favourites() {
    if (!this.personal)
      this.personal = true;
    else
      this.personal = false;
    this.loadProducts();
  }

  getUserName() {
    //тут будет проброс метода проверки залогиневшегося пользователя
    this.identificationService.getUser()
      .subscribe((name: string) => (this.username = name, this.identificateUser()));
  }

  identificateUser() {
    if (this.username != null) {
      this.userSigned = true;
    }
    else {
      this.username = "unsigned";
    }
  }

  getProducts() {
    console.log('getProducts, ' + this.products);
  }

  toggle(id: number, p: Products) {
    this.dataService.markProduct(id, p);
    this.dataService.getProducts(this.personal);
  }
  // получаем данные через сервис
  loadProducts() {
    this.dataService.getProducts(this.personal)
      .subscribe((data: Products[]) => this.products = data);
  }
  // сохранение данных
  save() {
    if (this.product.id == null) {
      this.dataService.createProduct(this.product)
        .subscribe((data: Products) => this.products.push(data));
    } else {
      this.dataService.updateProduct(this.product)
        .subscribe(data => this.loadProducts());
    }
    this.cancel();
  }
  editProduct(p: Products) {
    this.product = p;
  }
  cancel() {
    this.product = new Products();
    this.tableMode = true;
  }
  delete(p: Products) {
    this.dataService.deleteProduct(p.id);
    this.products.splice(this.products.findIndex(data => data==p),1);
      //.subscribe(data => this.loadProducts());
  }
  add() {
    this.cancel();
    this.tableMode = false;
  }

}
