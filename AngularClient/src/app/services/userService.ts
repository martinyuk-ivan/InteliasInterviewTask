import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
 import{Configuration} from'../config/Configurate';
 import { Observable } from 'rxjs/Rx';
@Injectable()
export class UserService  {

    constructor(private http: Http,private config:Configuration){

       
     }
     
    getPageUsers(pageIndex:number){
        return this.http.get(this.config.getUrlServer()+'/api/userController?pageSize='+this.config.pageSize+'&pageIndex='+pageIndex).map((response) => response.json())
			.catch((error:any) => Observable.throw(error.text()) || 'Server Error');;
    }
    getFilteredPageData(propertyName:string,pageIndex:number)
    {
        return this.http.get(this.config.getUrlServer()+'/api/userController/filterBy?propertyName='+propertyName+'&pageSize='+this.config.pageSize+'&pageIndex='+pageIndex).map((response) => response.json())
			.catch((error:any) => Observable.throw(error.text()) || 'Server Error');
    }
    getSearchedPageData(searchQuery:string,pageIndex:number)
    {
        return this.http.get(this.config.getUrlServer()+'/api/userController/search?searchString='+searchQuery+'&pageSize='+this.config.pageSize+'&pageIndex='+pageIndex).map((response) => response.json())
			.catch((error:any) => Observable.throw(error.text()) || 'Server Error');
    }
     

}