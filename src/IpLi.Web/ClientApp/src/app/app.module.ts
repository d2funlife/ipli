import {NgModule} from '@angular/core';
import {CommonModule} from "@angular/common";
import {Routes, RouterModule} from '@angular/router';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

import {AppComponent} from './app.component';
import {SidenavComponent} from './sidenav/sidenav.component';
import {ToolbarComponent} from "./toolbar/toolbar.component";
import {PlaylistsComponent} from './playlists/playlists.component';
import {ChannelsComponent} from "./channels/channels.component";
import {SourcesComponent} from "./sources/sources.component";

import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {CounterComponent} from './counter/counter.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';

import {MaterialModule} from './shared/material.module';
import {FlexLayoutModule} from '@angular/flex-layout';

const routes: Routes = [
  {path: 'playlists', component: PlaylistsComponent, data :{
    toolbarTitle : "Playlists"
    }},
  {path: 'channels', component: ChannelsComponent},
  {path: 'sources', component: SourcesComponent },
  { path: '',
    redirectTo: '/playlists',
    pathMatch: 'full'
  },
  { path: '**', redirectTo: "/playlists" }
];

@NgModule({
  declarations: [
    AppComponent,
    SidenavComponent,
    ToolbarComponent,
    PlaylistsComponent,
    SourcesComponent,
    ChannelsComponent,

    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
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
