import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GoogleLocationDTO, GapDTO, LandmarkDTO } from 'src/app/model';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private readonly _httpClient: HttpClient) {}

  url: string = 'https://192.168.1.6:7274/maps';
  // url: string = 'http://localhost:5217/maps';
  // url: string = 'https://travelmenow.online:7274/maps';
  // url: string = 'http://172.20.10.2:5217/maps';

  getLandmarks(
    lat: number,
    long: number,
    query: string,
    radius: number = 30000 
  ): Observable<LandmarkDTO[]> {
    return this._httpClient
      .get(
        `${this.url}/spots?lat=${lat}&long=${long}&query=${query}&radius=${radius}`
      )
      .pipe(
        map<any, LandmarkDTO[]>((response) => {
          return response;
        })
      );
  }

  getGapBetweenSpots(
    originLat: number,
    originLong: number,
    destinationLat: number,
    destinationLong: number
  ): Observable<GapDTO> {
    const url = `${this.url}/gap?OriginLat=${originLat}&OriginLong=${originLong}&DestLat=${destinationLat}&DestLong=${destinationLong}`;

    return this._httpClient
      .get(url)
      .pipe(
        map<any, GapDTO>((response) => {
          return response;
        })
      );
  }

  getGoogleLocationByCoordinates(
    lat: number,
    long: number
  ): Observable<GoogleLocationDTO> {
    const url = `${this.url}/googlelocation?Lat=${lat}&Long=${long}`;

    return this._httpClient
      .get(url)
      .pipe(
        map<any, GoogleLocationDTO>((response) => {
          return response;
        })
      );
  }
}
