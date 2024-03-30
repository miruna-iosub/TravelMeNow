import { NO_ERRORS_SCHEMA, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartComponent } from './components/start/start.component';
import { MenuComponent } from './components/menu/menu.component';
import { MapComponent } from './components/map/map.component';
import { CommonModule } from '@angular/common';
import { ImageWebcamComponent } from './components/ar/ar.component';
import { BookmarksComponent } from './components/bookmarks/bookmarks.component';


const routes: Routes = [
  {path: 'home', component: StartComponent},
  {path: 'menu', component: MenuComponent},
  {path: 'ar', component: ImageWebcamComponent},
  {path: 'map', component: MapComponent},
  {path: 'bookmarks', component: BookmarksComponent},
  {path: '', redirectTo: '/home', pathMatch: 'full'}
]

@NgModule({
  imports: [
    CommonModule, 
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
