import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  jwtHelper = new JwtHelperService();
  baseUrl = environment.apiUrl;
  decodeToken: any;

  constructor(private http: HttpClient) { }

  login(model: any) {
    const reqHeader = new HttpHeaders().set('Accept', 'application/json')
                                      .set('Accept-Language', 'en-gb')
                                      .set('Audience', 'Any')
                                      .set('Content-Type', 'application/x-www-form-urlencoded');

    const request = 'grant_type=password&username=' + model.username + '&password=' + model.password;
    return this.http.post(this.baseUrl + 'oauth2/token', request, {headers : reqHeader})
    .pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.access_token);
          this.decodeToken = this.jwtHelper.decodeToken(user.access_token);
        }
      })
    );
  }

  register(user: any) {
    return this.http.post(this.baseUrl + 'api/users/register', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

}
