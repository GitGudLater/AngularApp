import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataService } from './data.service';

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

  constructor(private dataService: DataService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }

  ngOnInit() {
    this.identification();
    this.loadProducts();
  }

  favourites() {
    if (!this.personal)
      this.personal = true;
    else
      this.personal = false;
    this.loadProducts();
  }

  identification() {
    //тут будет проброс метода проверки залогиневшегося пользователя
    this.dataService.getUser()
      .subscribe((name: string) => (this.username = name, this.signCheck()));
  }

  signCheck() {
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
    for (var i: number = 0; i < this.products.length; i++) {
      if (this.products[i].id == id) {
        this.products[i].favourite = !this.products[i].favourite;
        break;
      }
    }
    this.dataService.markProduct(id, p);
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
    this.dataService.deleteProduct(p.id)
      .subscribe(data => this.loadProducts());
  }
  add(p: Products) {
    let postedproduct: AddProduct = new AddProduct();
    postedproduct.name = p.name;
    postedproduct.about = p.about;
    postedproduct.cost = p.cost;
    postedproduct.designer = p.designer;
    this.dataService.createProduct(postedproduct);
    this.cancel();
    this.tableMode = false;
  }

}

export class Products {
  id: number;
  name: string;
  cost: number;
  designer: string;
  about: string;
  favourite: boolean;
}


export class AddProduct {
  name: string;
  cost: number;
  designer: string;
  about: string;
}
