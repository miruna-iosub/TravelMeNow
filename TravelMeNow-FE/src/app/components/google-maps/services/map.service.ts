import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import {
  PointOfInterest,
  PointOfInterestDTO,
  Distance,
  DistanceDTO,
  Address,
  AddressDTO,
} from 'src/app/model';
import { ApiService } from './api.service';
@Injectable({
  providedIn: 'root',
})
export class MapService {
  constructor(private readonly _apiService: ApiService) {}

  getLandmarks(
    lat: number,
    lng: number,
    query: string
  ): Observable<PointOfInterest[]> {
    return this._apiService.getLandmarks(lat, lng, query).pipe(
      map<PointOfInterestDTO[], PointOfInterest[]>((pointsOfInterest) => {
        const result: PointOfInterest[] = pointsOfInterest.map(
          (pointOfInterest) => {
            return {
              position: {
                lat: pointOfInterest.geometry.location.lat,
                lng: pointOfInterest.geometry.location.lng,
              },
              name: pointOfInterest.name,
              rating: pointOfInterest.rating,
              open_now: pointOfInterest.opening_hours?.open_Now,
            };
          }
        );
        return result;
      })
    );
  }


  getDistanceBetweenPlaces(
    originLat: number,
    originLong: number,
    destLat: number,
    destLong: number
  ): Observable<Distance> {
    return this._apiService
      .getDistanceBetweenPlaces(originLat, originLong, destLat, destLong)
      .pipe(
        map<DistanceDTO, Distance>((distance) => {
          return {
            kmNumber: distance.distance.text,
            eta: distance.duration.text,
          };
        })
      );
  }

  getAddressByCoordinates(lat: number, lng: number): Observable<Address> {
    return this._apiService.getAddressByCoordinates(lat, lng).pipe(map<AddressDTO, Address>((address) => {
        const data = address.formatted_address.split(',');
        const street = data[0].split(" ").slice(1).join(" ");
        return {
          street: street == "no name" ? '-' : street,
          city: data[1],
          country: data[2]
        };
    }));
  }
}
