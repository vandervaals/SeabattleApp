import { Component, OnInit } from '@angular/core';
import { OnlineUser } from 'src/app/_models/onlineUser';
import { SignalRService } from 'src/app/_services/signalR.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  onlineUsers: OnlineUser[] = [];

  constructor(private signalR: SignalRService) { }

  ngOnInit() {
    this.onlineUsers.push({ username: "Vova", connectionId: "5", isBusy: true },
    { username: "peter", connectionId: "6", isBusy: false });
    this.signalR.onlineUsers.forEach(item => this.onlineUsers.push(item));
    this.signalR.userConnected.subscribe((user: OnlineUser) => {
      this.onlineUsers.push(user);
    });
    this.signalR.userDisconnected.subscribe((user: OnlineUser) => {
      const index: number = this.onlineUsers.indexOf(user);
      if (index !== -1) {
        this.onlineUsers.splice(index, 1);
      }
    });
  }

}
