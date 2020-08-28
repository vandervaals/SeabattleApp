import { OnlineUser } from './onlineUser';

export interface OnConnect {
    ConnectionId: string;
    Users: Array<OnlineUser>;
}