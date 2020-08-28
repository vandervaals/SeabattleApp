import { Ship } from './ship';

export interface CreateGameRequest {
    ConnectionId: string;
    Ships: Array<Ship>;
}