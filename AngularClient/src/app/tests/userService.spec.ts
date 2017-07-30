import { TestBed,async,inject,tick  } from '@angular/core/testing';
import {UserService} from '../services/userService';
import{Configuration} from'../config/Configurate';
import{Page} from'../user/Models/pagesModel';
import{User} from'../user//Models/userModel';


import {
  HttpModule,
  Http,
  Response,
  ResponseOptions,
  BaseRequestOptions,
  XHRBackend
} from '@angular/http';
import { MockBackend } from '@angular/http/testing';
let userService: UserService;
let mock: MockBackend;
describe('UserService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
        imports:[HttpModule],
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
                   // TestBed.compileComponents();

    mock = TestBed.get(MockBackend); 
    userService = TestBed.get(UserService);
  });
 describe('resturnUserPage()', () => {
it('return badRequest from server',()=>{
    let pageIndex:number=1;
mock.connections.subscribe(connection => {
      connection.mockRespond(new Response(<ResponseOptions>{
      body:"bad inputs  parameters",
      status:404
      }));
    });
    userService.getPageUsers(pageIndex).subscribe((error)=>{       
    expect(error).toBe('bad inputs  parameters');
    },error=>{}); 
    
});
it('should return same currentIdex page ', (() => {
 
    const serverResponse = {
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
let pageIndex:number=1;
mock.connections.subscribe(connection => {

      connection.mockRespond(new Response(<ResponseOptions>{
       body:JSON.stringify(serverResponse)
      }));
    });
    userService.getPageUsers(pageIndex).subscribe((users)=>{       
    expect(users.currentPage).toBe(pageIndex);
    }); 
userService.getSearchedPageData('john',pageIndex).subscribe(users=>{
      expect(users.currentPage).toBe(pageIndex);
});    
    userService.getFilteredPageData('FirstName',pageIndex).subscribe(users=>{
      expect(users.currentPage).toBe(pageIndex);
});
}));
});
});