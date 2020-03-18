import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './NavBar/navbar.component';
import {FormsModule} from '@angular/forms'
import { CardVerification } from './CardVerification/cardverification.component';
import { HttpClientModule} from  "@angular/common/http";
import { PinComponent } from './CardVerification/Pin.confirmation.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    CardVerification,
    PinComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
