import {Component, OnInit, ViewChild} from "@angular/core";
import {MatTableDataSource, MatPaginator, MatSort, PageEvent} from '@angular/material';
import {Channel} from "../models/channel";
import {ChannelService} from "../services/channel.service";

@Component({
  selector: 'channels',
  templateUrl: './channels.component.html',
  styleUrls: ['./channels.component.css']
})
export class ChannelsComponent implements OnInit {
  displayedColumns: string[] = ['title'];
  dataSource: MatTableDataSource<Channel>;

  constructor(private channelService: ChannelService) {
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngOnInit(): void {
    this.getChannels();
  }

  ngAfterViewInit() {
    this.paginator.page.subscribe(
      (event) => this.getChannels(event)
    );
  }

  public getChannels(event?:PageEvent): void {
    let pageIndex = 0;
    let pageSize = 10;

    if(event){
      pageIndex = event.pageIndex;
      pageSize = event.pageSize;
    }

    let offset = pageIndex * pageSize;
    this.channelService.getPerPage(offset, pageSize)
      .subscribe(response => {
        let totalCount = response.headers.get('x-total-count');
        this.paginator.length = +totalCount;
        this.dataSource = new MatTableDataSource<Channel>(response.body);
      });
  }
}
