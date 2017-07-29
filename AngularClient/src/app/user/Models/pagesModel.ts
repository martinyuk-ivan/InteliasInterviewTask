import {JsonProperty} from "json2typescript";
export class Page<T>{
   
    @JsonProperty('results')
    results: T[];
    @JsonProperty('pageSize')
    pageSize: number;
    @JsonProperty('currentPage')
    currentPage:number;
     @JsonProperty('totalNumberPages')
    totalNumberPages:number; 
}