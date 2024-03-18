import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ImageService {
  constructor(private http: HttpClient) {}

  sendImage(imageData: string) {
    const body = { image: imageData };
    return this.http.post('https://192.168.1.6:8005/image_api', body);
  }
}
