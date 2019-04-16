import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ChosenService } from './chosen.service';


@Component({
  selector: 'app-chosen-products',
  templateUrl: './chosen-products.component.html',
  styleUrls: ['./chosen-products.component.css'],
  providers:[ChosenService]
})
export class ChosenProductsComponent implements OnInit {

  public relations: Relation[];
  public relation: ViewRelation = new ViewRelation;
  public product: Product;   // изменяемый товар
  public products: Product[];
  tableMode: boolean = true;
  costil: number = 0;



  constructor(private dataService: ChosenService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

  }

  ngOnInit() {
    this.loadRelations();
    this.loadProducts();
    //console.log('loadProdSeco' + this.products/*data*/.map(i => { console.log(i.name + ',' + i.cost) }));
  }

  loadRelations() {
    this.dataService.getRelations()
      .subscribe((data: Relation[]) => this.relations = data);
  }

  getProducts() {
    console.log('getProducts, ' + this.products);
  }

  check(id: number, useles: string) {
    this.relation.productId = id;
    var count = this.relations.length;
    if (count > 0) {
      this.relation.userName = this.relations[0].userName;
    }
    else {
      this.relation.userName = null;
    }
    this.dataService.checkRelation(this.relation);
  }
  // получаем данные через сервис
  loadProducts() {
    this.dataService.getProducts()
      .subscribe((data: Product[]) =>/* { console.log('loadProdFirst' + data);*/ this.products = data /*console.log('loadProdSeco' + data.map(i => {console.log(i.name + ',' + i.cost) })); }*/);
  }
  // сохранение данных
  save() {
    if (this.product.id == null) {
      this.dataService.createProduct(this.product)
        .subscribe((data: Product) => this.products.push(data));
    } else {
      this.dataService.updateProduct(this.product)
        .subscribe(data => this.loadProducts());
    }
    this.cancel();
  }
  editProduct(p: Product) {
    this.product = p;
  }
  cancel() {
    this.product = new Product();
    this.tableMode = true;
  }
  delete(p: Product) {
    this.dataService.deleteProduct(p.id)
      .subscribe(data => this.loadProducts());
  }
  
  //!!!Оператор сравнения глючит - пересмотреть возможности вызова оператора сравнения для получения нужного значения истинности
  compareProducts(product: number/*, source*/): boolean {
    let count: number = this.relations.length;
    while (count > 0) {
      if (this.relations[count - 1].productId == product) {
        return true;
      }
      count--;
    }
    return false;
  }
}

export class Product {
  id: number;
  name: string;
  cost: number;
  designer: string;
  about: string;
}

export class ViewRelation {
  userName: string;
  productId: number;
}

export class Relation {
  id: number;
  userName: string;
  productId: number;
  checked: boolean;
}

export class Booleans {
  id: number;
  checked: boolean;
}

