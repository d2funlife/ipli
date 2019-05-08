import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `<app-sidenav></app-sidenav>`,
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';
  toolbarTitle = 'apptool';

  constructor() {}

  ngOnInit(): void {
  }
}
