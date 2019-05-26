import {Component, OnInit} from "@angular/core";
import {MatTableDataSource, MatPaginator, MatSort} from '@angular/material';
import {PlaylistShort} from "../models/playlistShort";

@Component({
  selector: 'playlists',
  templateUrl: './playlists.component.html',
  styleUrls: ['./playlists.component.css']
})
export class PlaylistsComponent implements OnInit {
  dataSource: MatTableDataSource<PlaylistShort>;

  constructor() {
  }

  ngOnInit(): void {
  }
}
