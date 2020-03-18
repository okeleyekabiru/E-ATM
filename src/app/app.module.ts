import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './NavBar/navbar.component';
import {FormsModule} from '@angular/forms'
import { CardVerification } from './CardVerification/cardverification.component';
@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    CardVerification
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
