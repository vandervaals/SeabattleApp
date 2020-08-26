import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  model: any = { };
  test: string;
  authenticated = true;

  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.model.grant_type = 'password';
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Success');
    }, error => {
      this.alertify.error('Error');
      console.log(error.error);
    }, () => {
      this.router.navigate(['/users']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.authService.decodeToken = null;
    this.alertify.message('logout');
    this.router.navigate(['/home']);
  }

}
