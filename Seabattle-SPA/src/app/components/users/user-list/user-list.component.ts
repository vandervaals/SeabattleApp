import { Component, OnInit } from '@angular/core';
import { OnlineUser } from 'src/app/_models/onlineUser';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  onlineUsers: OnlineUser[];

  constructor() { }

  ngOnInit() {
    this.onlineUsers = new Array<OnlineUser>();
    this.onlineUsers.push({ id: 1, username: "Vova", connectionId: "5" },
    { id: 2, username: "peter", connectionId: "6" });
  }

}
