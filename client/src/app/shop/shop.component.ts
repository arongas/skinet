import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  totalCount:number=0;
  shopParams: ShopParams = new ShopParams();
  @ViewChild('search') searchInput?:ElementRef;

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price Low to high', value: 'priceAsc' },
    { name: 'Price High to low', value: 'priceDesc' }
  ]

  constructor(private shopService: ShopService) {

  }

  ngOnInit(): void {
    this.getBrands();
    this.getTypes();
    this.getProducts();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(
      {
        next: response => {
          this.products = response.data,
          this.shopParams.pageNumber=response.pageIndex
          this.shopParams.pageSize=response.pageSize
          this.totalCount=response.count

        },
        error: error => console.log(error)
      }
    )
  }
  getBrands() {
    this.shopService.getBrands().subscribe(
      {
        next: response => this.brands = [{ id: 0, name: 'All' }, ...response],
        error: error => console.log(error)
      }
    )
  }
  getTypes() {
    this.shopService.getTypes().subscribe(
      {
        next: response => this.types = [{ id: 0, name: 'All' }, ...response],
        error: error => console.log(error)
      }
    )
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }

  onSortSelected(event: any) {
    this.shopParams.sort = event.target.value;    
    this.getProducts();
  }

  onPageChanged(page: number){
    if (this.shopParams.pageNumber!==page){
        this.shopParams.pageNumber=page
        this.getProducts()
    }
  }

  onSearch(){
    this.shopParams.search=this.searchInput?.nativeElement.value;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }

  onReset(){
    if (this.searchInput) this.searchInput.nativeElement.value="";
    this.shopParams=new ShopParams();
    this.getProducts();
  }
}
