import {JsonProperty} from "json2typescript";
export class User{
    @JsonProperty('id')
    id: number;
    @JsonProperty('firstName')
    firstName: string;
    @JsonProperty('LastName')
    lastName:string;
    @JsonProperty('title')
    title:string;
    @JsonProperty('soldCount')
    oldCount:number;
    @JsonProperty('refer')
    refer:string;
}