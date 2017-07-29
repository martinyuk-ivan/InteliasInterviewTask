import { Component,OnInit,NgModule } from '@angular/core';
import {UserService} from './userService';
import{User} from'./Models/userModel';
import{Configuration} from'../config/Configurate';
import{Page} from'./Models/pagesModel';
import { Observable } from 'rxjs/Rx';
import {PageTypes} from './Models/pageType';
import 'rxjs/Rx';
@Component({
  selector: 'app-users',
  templateUrl: './userComponent.html',
  styleUrls: ['./userComponent.css'],
  providers: [UserService,Configuration]
})
export class userComponent implements OnInit { 
    usersPage: Page<User>;
    pageType:PageTypes;
    users:User[];
    pageIndex:number;
    searchQuery:string;
    filterProperty:string;
    
    constructor(private userService: UserService,private config:Configuration ){
        this.pageIndex=1;
        this.pageType=PageTypes.Sample;
    }      
    ngOnInit(){
       this.refreshUsers();
}  
     
    refreshUsers()
    {
       if(this.pageType==PageTypes.Sample){
        this.userService.getPageUsers(this.pageIndex).subscribe(us=>this.usersPage=us);
       }
      else if(this.pageType==PageTypes.Searchable)
        {
             this.userService.getSearchedPageData(this.searchQuery,this.pageIndex).subscribe(us=>this.usersPage=us);
        }
     else if(this.pageType==PageTypes.Sortable)
        {
            this.userService.getFilteredPageData(this.filterProperty,this.pageIndex).subscribe(us=>this.usersPage=us);
        }
	}   
    fakeArray(size:number)
    {
         let res = [];

    for (let i = 0; i < size; i++) {
        if(i+1<this.config.maxPagex)
            {
        res.push(i+1);
            }
      }

      return res;
    }
   lastPage(){
      if(this.pageIndex < this.usersPage.totalNumberPages){
            this.pageIndex = this.usersPage.totalNumberPages;
            this.refreshUsers();
      }                 
   }
    firstPage()
    {
        if(this.pageIndex != 1){
            this.pageIndex = 1;
            this.refreshUsers();
      }     
    }
    sortBy(propertyName:string)
    {
        this.pageType=PageTypes.Sortable;
        this.filterProperty=propertyName;
    if(this.pageIndex != 1){
        this.pageIndex=1;
    }
     this.refreshUsers();       
    }
    setPage(index : number){
        if  (this.pageIndex!=index)
            {
                this.pageIndex=index;
                this.refreshUsers();
            }
    }
        search ()
        {
            this.pageType=PageTypes.Searchable;
    if(this.pageIndex != 1){
        this.pageIndex=1;
    }
    this.refreshUsers(); 

        }

   
}



