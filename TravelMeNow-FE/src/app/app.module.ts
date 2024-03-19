import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { IonicModule } from '@ionic/angular';
import { GoogleMapsModule } from '@angular/google-maps'
import { StartComponent } from './components/start/start.component';
import { MenuComponent } from './components/menu/menu.component';
import { MapComponent } from './components/map/map.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { GoogleMapsComponent } from './components/google-maps/google-maps.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { ImageWebcamComponent } from './components/ar/ar.component';
import { WebcamModule } from 'ngx-webcam';
import { BookmarksComponent } from './components/bookmarks/bookmarks.component';

@NgModule({
  declarations: [
    AppComponent,
    StartComponent,
    MenuComponent,
    MapComponent,
    GoogleMapsComponent,
    ImageWebcamComponent,
    BookmarksComponent
  ],
  imports: [
    BrowserModule,
    WebcamModule,
    AppRoutingModule,
    IonicModule.forRoot(),
    GoogleMapsModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient]
      }
    }),
    BrowserAnimationsModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA] 
})
export class AppModule { }
export function httpTranslateLoader(http: HttpClient ){
  return new TranslateHttpLoader(http);
}