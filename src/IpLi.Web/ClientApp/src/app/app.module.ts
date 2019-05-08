import {NgModule} from '@angular/core';
import {CommonModule} from "@angular/common";
import {Routes, RouterModule} from '@angular/router';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

import {AppComponent} from './app.component';
import {SidenavComponent} from './sidenav/sidenav.component';
import {PlaylistsComponent} from './playlists/playlists.component';
import {ToolbarComponent} from "./toolbar/toolbar.component";
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {CounterComponent} from './counter/counter.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';

import {MaterialModule} from './shared/material.module';
import {FlexLayoutModule} from '@angular/flex-layout';

const routes: Routes = [
  {
    path: 'playlists', component: AppComponent,
    children: [
      {path: '', component: PlaylistsComponent}
    ]
  },
  {path: '**', redirectTo: 'playlists'}
];

@NgModule({
  declarations: [
    AppComponent,
    SidenavComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    PlaylistsComponent,
    ToolbarComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    BrowserAnimationsModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    MaterialModule,
    FlexLayoutModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
