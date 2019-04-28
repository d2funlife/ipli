import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "sources",
  templateUrl: "./source.component.html"
})
export class SourceComponent {

  page = 1;
  pageSize = 10;
  collectionSize = 0;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    http.get<Source[]>(baseUrl + "api/sources").subscribe(
      result => {
        this.sources = result;
      },
      error => console.error(error)
    );
  }
}
