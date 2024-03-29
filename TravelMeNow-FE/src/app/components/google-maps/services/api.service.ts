import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AddressDTO, DistanceDTO, PointOfInterestDTO } from 'src/app/model';

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
  ): Observable<PointOfInterestDTO[]> {
    return this._httpClient
      .get(
        `${this.url}/places?latitude=${lat}&longitude=${long}&query=${query}&radius=${radius}`
      )
      .pipe(
        map<any, PointOfInterestDTO[]>((response) => {
          return response;
        })
      );
  }

  getDistanceBetweenPlaces(
    originLatitude: number,
    originLongitude: number,
    destinationLatitude: number,
    destinationLongitude: number
  ): Observable<DistanceDTO> {
    const url = `${this.url}/distance?OriginLatitude=${originLatitude}&OriginLongitude=${originLongitude}&DestLatitude=${destinationLatitude}&DestLongitude=${destinationLongitude}`;

    return this._httpClient
      .get(url)
      .pipe(
        map<any, DistanceDTO>((response) => {
          return response;
        })
      );
  }

  getAddressByCoordinates(
    latitude: number,
    longitude: number
  ): Observable<AddressDTO> {
    const url = `${this.url}/address?Latitude=${latitude}&Longitude=${longitude}`;

    return this._httpClient
      .get(url)
      .pipe(
        map<any, AddressDTO>((response) => {
          return response;
        })
      );
  }
}
