import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { AppLauncher } from '@capacitor/app-launcher';
import { Camera, CameraResultType } from '@capacitor/camera';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
})
export class MapComponent {
  constructor(
    private location: Location
  ) {
    
  }
  goBack() {
    this.location.back();
  }
  
  async launchAR() {
    console.log('Button clicked. Opening AR App...');
    await AppLauncher.openUrl({ url: 'https://192.168.1.6:4200/ar' });
    console.log('AR App opened successfully.');
  }

  async captureImage() {
    const image = await Camera.getPhoto({
      quality: 90,
      allowEditing: false,
      resultType: CameraResultType.Base64,
    });
  }
}
