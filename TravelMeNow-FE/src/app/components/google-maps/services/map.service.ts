import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import {
  Landmark,
  LandmarkDTO,
  Gap,
  GapDTO,
  GoogleLocation,
  GoogleLocationDTO,
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
  ): Observable<Landmark[]> {
    return this._apiService.getLandmarks(lat, lng, query).pipe(
      map<LandmarkDTO[], Landmark[]>((landmarks) => {
        const result: Landmark[] = landmarks.map(
          (landmark) => {
            return {
              position: {
                lat: landmark.geometry.location.lat,
                lng: landmark.geometry.location.lng,
              },
              name: landmark.name,
              rating: landmark.rating,
              open_now: landmark.opening_hours?.open_Now,
            };
          }
        );
        return result;
      })
    );
  }


  getGapBetweenSpots(
    originLat: number,
    originLong: number,
    destLat: number,
    destLong: number
  ): Observable<Gap> {
    return this._apiService
      .getGapBetweenSpots(originLat, originLong, destLat, destLong)
      .pipe(
        map<GapDTO, Gap>((gap) => {
          return {
            eta: gap.duration.text,
          };
        })
      );
  }

  getGoogleLocationByCoordinates(lat: number, lng: number): Observable<GoogleLocation> {
    return this._apiService.getGoogleLocationByCoordinates(lat, lng).pipe(map<GoogleLocationDTO, GoogleLocation>((googleLocation) => {
        const data = googleLocation.formattedGoogleLocation.split(',');
        const street = data[0].split(" ").slice(1).join(" ");
        return {
          street: street == "no name" ? '-' : street,
          city: data[1],
          country: data[2]
        };
    }));
  }
}
