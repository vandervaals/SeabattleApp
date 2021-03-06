import { Injectable, EventEmitter } from '@angular/core';
import { SignalR, ISignalRConnection } from 'ng2-signalr';
import { OnlineUser } from '../_models/onlineUser';
import { OnConnect } from '../_models/onConnect';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private connection: ISignalRConnection;
  userConnected = new EventEmitter<OnlineUser>();
  userDisconnected = new EventEmitter<OnlineUser>();
  connsectionId: string;

  onlineUsers: OnlineUser[] = [];

  constructor(private hub: SignalR,
  ) {
    this.buildConnection();
  }

  private buildConnection = () => {
    this.hub.connect()
      .then((c) => {
        this.connection = c;
        this.connection
          .listenFor<OnConnect>('OnConnected')
          .subscribe((answer) => {
            answer.Users.forEach(item => this.onlineUsers.push(item));
            answer.Users.forEach(item => this.userConnected.emit(item));
            this.connsectionId = answer.ConnectionId;
          });
        this.connection
          .listenFor<OnlineUser>('OnNewUserConnected')
          .subscribe((dto) => {
            this.onlineUsers.push(dto);
            this.userConnected.emit(dto);
          });
        this.connection
          .listenFor<OnlineUser>('OnUserDisconnected')
          .subscribe((dto) => {
            const index: number = this.onlineUsers.indexOf(dto);
            if (index !== -1) {
              this.onlineUsers.splice(index, 1);
            }
            this.userDisconnected.emit(dto);
          });
      })
      .catch((reason) =>
        console.error(`Cannot connect to hub sample ${reason}`)
      );
  }

  public userConnect(username: string) {
    if (this.connection) {
      this.connection.invoke('Connect', username);
    }
  }

}
