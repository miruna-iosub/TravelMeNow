import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  constructor(
    private location: Location,
){}

  goBack() {
    this.location.back();
  }
}
