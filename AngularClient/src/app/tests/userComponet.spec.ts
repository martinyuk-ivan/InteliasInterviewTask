import { TestBed,ComponentFixture  } from '@angular/core/testing';
import {UserService} from '../services/userService';
import{Configuration} from'../config/Configurate';
import{Page} from'../user/Models/pagesModel';
import{User} from'../user/Models/userModel';
import { By }              from '@angular/platform-browser';
import { DebugElement }    from '@angular/core';
import{userComponent} from'../user/userComponent';
import { FormsModule } from '@angular/forms';
import {
  HttpModule,
  Http,
  Response,
  ResponseOptions,
  BaseRequestOptions,
  XHRBackend
} from '@angular/http';
import { MockBackend } from '@angular/http/testing';

describe('user component  tests',()=>{
let uComponent: userComponent;
let cFixture: ComponentFixture<userComponent>;
let debugTable: DebugElement;
let htmlTable: HTMLElement;
let debugInput:DebugElement;
let htmlInput:HTMLElement;
let mockService:MockBackend;
let userService: UserService;
let fakeUsers={
    "results": [
        {
            "id": 1,
            "firstName": "John",
            "lastName": "Dou",
            "title": "CTO",
            "soldCount": 12,
            "refer": "Alicia Losg"
        },
        {
            "id": 3,
            "firstName": "Oleg",
            "lastName": "Sample",
            "title": "CEO",
            "soldCount": 111,
            "refer": "Ivan"
        },
        {
            "id": 2,
            "firstName": "Ivan",
            "lastName": "Martynyuk",
            "title": "dev",
            "soldCount": 11,
            "refer": "John"
        },
        {
            "id": 4,
            "firstName": "Andrew",
            "lastName": "Exapmle",
            "title": "Manager",
            "soldCount": 1,
            "refer": "Oleg"
        }
    ],
    "pageSize": 4,
    "currentPage": 1,
    "totalNumberPages": 1
};
beforeEach(() => {
   TestBed.configureTestingModule({
     declarations: [ userComponent ],
      imports:[HttpModule,FormsModule],
      providers: [
             Configuration,
            UserService,
                MockBackend,
            BaseRequestOptions,
            {
          provide: Http,
          deps: [MockBackend, BaseRequestOptions],
          useFactory:
            (backend: XHRBackend, defaultOptions: BaseRequestOptions) => {
                return new Http(backend, defaultOptions);
            }
         }
    ]
   });
  mockService = TestBed.get(MockBackend); 
    userService = TestBed.get(UserService);
    cFixture=TestBed.createComponent(userComponent);
    uComponent=cFixture.componentInstance;
    debugTable=cFixture.debugElement.query(By.css('table'));
    debugInput=cFixture.debugElement.query(By.css('input'));
    htmlTable = debugTable.nativeElement;
      
});
function subscripeOnConnection()
{
      mockService.connections.subscribe(connection => {
      connection.mockRespond(new Response(<ResponseOptions>{
       body:JSON.stringify(fakeUsers)
      }));
    });  
}
 it('when normal table', () => { 
  subscripeOnConnection();
    cFixture.detectChanges();
    //detect length of table has to same as length of fake array
   expect(htmlTable.children[1].children.length).toEqual(fakeUsers.results.length);
       
  fakeUsers={
    "results": [
        {
            "id": 4,
            "firstName": "Andrew",
            "lastName": "Exapmle",
            "title": "Manager",
            "soldCount": 1,
            "refer": "Oleg"
        },
        {
            "id": 2,
            "firstName": "Ivan",
            "lastName": "Martynyuk",
            "title": "dev",
            "soldCount": 11,
            "refer": "John"
        },
        {
            "id": 1,
            "firstName": "John",
            "lastName": "Dou",
            "title": "CTO",
            "soldCount": 12,
            "refer": "Alicia Losg"
        },
        {
            "id": 3,
            "firstName": "Oleg",
            "lastName": "Sample",
            "title": "CEO",
            "soldCount": 111,
            "refer": "Ivan"
        }
    ],
    "pageSize": 4,
    "currentPage": 1,
    "totalNumberPages": 1
};

   fakeUsers= {"results": [
        {
            "id": 1,
            "firstName": "John",
            "lastName": "Dou",
            "title": "CTO",
            "soldCount": 12,
            "refer": "Alicia Losg"
        }],
    "pageSize": 4,
    "currentPage": 1,
    "totalNumberPages": 1
};
    //after click on table header 

 //after search some data
 uComponent.searchQuery="John";
  fakeUsers= {"results": [
        {
            "id": 1,
            "firstName": "John",
            "lastName": "Dou",
            "title": "CTO",
            "soldCount": 12,
            "refer": "Alicia Losg"
        }],
    "pageSize": 4,
    "currentPage": 1,
    "totalNumberPages": 1
};
 uComponent.search();
 
  expect(htmlTable.children[1].children[0].children[0].textContent).toEqual('John');

 });
 it('when bad response from server', () => {
   mockService.connections.subscribe(connection => {
      connection.mockRespond(new Response(<ResponseOptions>{
       body:"bad requests"
      }));
    });  
    uComponent.ngOnInit();
   cFixture.detectChanges();
   uComponent.searchQuery="asdaaas";
   uComponent.search();
  expect(htmlTable.textContent).toContain('');
 });
});