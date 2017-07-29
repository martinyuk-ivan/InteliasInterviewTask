import { Injectable } from '@angular/core';

@Injectable()
export class Configuration
{
    urlServer:string;
    maxPagex:number;
    pageSize:number;
    constructor()
    {
        this.urlServer='http://localhost:51171';      
        this.maxPagex=5;
        this.pageSize=2;
    }

    
    getUrlServer():string
    {
        return this.urlServer;
    }

}