import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Ship } from '../_models/ship';
import { map } from 'rxjs/operators';
import { Shot } from '../_models/shot';
import { Answer } from '../_models/answer';
import { CreateGameRequest } from '../_models/createGameRequest';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  baseUrl = environment.apiUrl + 'api/games';

  constructor(private http: HttpClient) { }

  register(ships: Array<Ship>, connectionId: string) {
    const request: CreateGameRequest = { ConnectionId: connectionId, Ships: ships }
    return this.http.post<number>(this.baseUrl + '/register', request)
    .pipe(
      map((response: number) => {
        return response;
      })
    );
  }

  shot(shot: Shot) {
    return this.http.post<Answer>(this.baseUrl + '/shot', shot)
    .pipe(
      map((response: Answer) => {
        return response;
      })
    );
  }

}
