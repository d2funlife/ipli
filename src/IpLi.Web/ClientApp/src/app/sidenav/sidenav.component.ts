import { Component, OnInit } from '@angular/core';

const SMALL_WIDTH_BREAKPOINT = 720;

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent implements OnInit {
  //
  // private mediaMatcher: MediaQueryList =
  //   matchMedia(`(max-width: ${SMALL_WIDTH_BREAKPOINT}px)`);

  constructor() {


    // this.mediaMatcher.addEventListener(mql =>
    //   zone.run(() => this.mediaMatcher = mql));
  }

  ngOnInit() {
  }

  isScreenSmall(): boolean {
    return false;
  }

}
