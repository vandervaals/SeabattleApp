import { Component, OnInit, Input } from '@angular/core';
import { OnlineUser } from 'src/app/_models/onlineUser';

@Component({
  selector: 'app-user-row',
  templateUrl: './user-row.component.html',
  styleUrls: ['./user-row.component.scss']
})
export class UserRowComponent implements OnInit {
  @Input() user: OnlineUser;

  constructor() { }

  ngOnInit() {
  }

  inviteUser() {
    console.log(this.user.ConnectionId);
  }

}
