import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SignalRModule, SignalRConfiguration, ConnectionTransports } from 'ng2-signalr';
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { BattleareaComponent } from './components/battlearea/battlearea.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './components/home/home.component';
import { NavComponent } from './components/nav/nav.component';
import { RegisterComponent } from './components/register/register.component';
import { UserListComponent } from './components/users/user-list/user-list.component';
import { UserRowComponent } from './components/users/user-row/user-row.component';
import { appRoutes } from './routes';
import { AlertifyService } from './_services/alertify.service';
import { environment } from 'src/environments/environment';
import { SignalRService } from './_services/signalR.service';
import { GameService } from './_services/game.service';

export function initConfig(): SignalRConfiguration {
  const cfg = new SignalRConfiguration();

  cfg.hubName = 'hub';
  cfg.url = environment.apiUrl;
  cfg.transport = [
    ConnectionTransports.webSockets,
    ConnectionTransports.longPolling,
    ConnectionTransports.auto
  ];

  return cfg;
}

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    BattleareaComponent,
    HomeComponent,
    NavComponent,
    RegisterComponent,
    UserListComponent,
    UserRowComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    SignalRModule.forRoot(initConfig),
    JwtModule.forRoot({
      config: {
         tokenGetter: tokenGetter,
         allowedDomains: ['localhost:52844'],
         disallowedRoutes: ['localhost:52844/oauth2/token',
                            'localhost:52844/api/users',
                            'localhost:52844/hub']
      }
   })
  ],
  providers: [
    AuthService,
    AlertifyService,
    SignalRService,
    GameService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
