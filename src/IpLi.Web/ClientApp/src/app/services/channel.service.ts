import {Injectable} from "@angular/core";
import {HttpClient, HttpResponse} from "@angular/common/http";
import {Observable} from "rxjs";
import {Channel} from "../models/channel";
import {Config} from "protractor";

@Injectable()
export class ChannelService {
  constructor(private http: HttpClient) {
  }

  getPerPage(offset: number, limit: number) : Observable<HttpResponse<Channel[]>> {
    var requestUrl = `http://localhost:51177/api/channels?offset=${offset}&limit=${limit}`;
    return this.http.get<Channel[]>(requestUrl, { observe: 'response' });
  }
}
