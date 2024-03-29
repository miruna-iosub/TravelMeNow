import {
  Component,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { GoogleMap, MapInfoWindow } from '@angular/google-maps';
import { ActivatedRoute } from '@angular/router';
import { openGoogleMaps } from './utils';
import { TranslateService } from '@ngx-translate/core';
import { Address, Marker, MarkerInfo } from 'src/app/model';
import { MapService } from './services/map.service';
import { Geolocation } from '@capacitor/geolocation';
import { first } from 'rxjs';

@Component({
  selector: 'app-google-maps',
  templateUrl: './google-maps.component.html',
  styleUrls: ['./google-maps.component.scss'],
})
export class GoogleMapsComponent implements OnInit {
  @ViewChild(GoogleMap) map!: GoogleMap;
  @ViewChildren(MapInfoWindow) infoWindow!: QueryList<MapInfoWindow>;

  center: google.maps.LatLngLiteral = {
    lat: 48.86257959027724, 
    lng: 2.31659157820982
  };
  markers!: Marker[];
  options: google.maps.MapOptions = {
    styles: [
      {
        featureType: 'poi',
        stylers: [{ visibility: 'off' }],
      },
    ],
    zoom: 14,
    minZoom: 10,
  };
  landmarkInfo: MarkerInfo = {
    name: '',
    rating: '',
    lat: 0,
    lng: 0,
    distance: '0',
    eta: '0',
    open: undefined,
  };
  userLocation: google.maps.LatLngLiteral = {
    lat: 48.86257959027724, 
    lng: 2.31659157820982
  };
  userLocationInfo: Address = {
    street: '',
    city: '',
    country: '',
  };

  constructor(
    private readonly _mapService: MapService,
    private readonly _route: ActivatedRoute,
    private _translate: TranslateService
  ) {}

  ngOnInit(): void {
    this.setInitialPosition();
    setInterval(() => this.setCurrentPosition(), 30 * 1000);
    this._route.queryParams.subscribe((params) => {
      this.getMarkers(params['query']);
    });
  }

  setInitialPosition = async () => {
    await Geolocation.getCurrentPosition({ enableHighAccuracy: true })
      .then((response) => {
        this.center = {
          lat: response.coords.latitude,
          lng: response.coords.longitude,
        };
        this.userLocation = this.center;
      })
      .catch(async (err) => {
        console.log(err);
      });
  };

  setCurrentPosition = async () => {
    await Geolocation.getCurrentPosition({ enableHighAccuracy: true })
      .then((response) => {
        this.userLocation = {
          lat: response.coords.latitude,
          lng: response.coords.longitude,
        };
      })
      .catch(async (err) => {
        console.log(err);
      });
  };

  setCenterBackToUserLocation() {
    this.center = this.userLocation;
    this.map.panTo(this.center);
  }

  setArrivalTime(lat: number, lng: number) {
    const userLat = this.userLocation.lat;
    const userLng = this.userLocation.lng;
    this._mapService
      .getDistanceBetweenPlaces(userLat, userLng, lat, lng)
      .pipe(first())
      .subscribe({
        next: (distance) => {
          this.landmarkInfo.distance = distance.kmNumber;
          this.landmarkInfo.eta = distance.eta;
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  getMarkers(query: string) {
    const imagePath = `../../assets/images/${query}.png`;

    console.log('Image Path:', imagePath);

    this._mapService
      .getLandmarks(this.center.lat, this.center.lng, query)
      .pipe(first())
      .subscribe({
        next: (pointsOfInterest) => {
          this.markers = pointsOfInterest.map((pointOfInterest) => {
            return {
              position: {
                lat: pointOfInterest.position.lat,
                lng: pointOfInterest.position.lng,
              },
              label: {
                color: 'white',
                text: ' ',
              },
              title: pointOfInterest.name,
              options: {
                animation: google.maps.Animation.DROP,
              },
              icon: {
                url: `../../assets/images/${query}.png`,
                scaledSize: new google.maps.Size(32, 32),
              },
              rating: pointOfInterest.rating,
              open_now: pointOfInterest.open_now,
            };
          });
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  openlandmarkInfo(
    marker: any,
    name: string,
    rating: number,
    lat: number,
    lng: number,
    open: boolean
  ) {
    this.center = {
      lat: lat,
      lng: lng,
    };

    this.landmarkInfo.name = name;
    this.landmarkInfo.lat = lat;
    this.landmarkInfo.lng = lng;
    if (rating != 0) {
      this.landmarkInfo.rating = `${rating}`;
    }
    if (open == true) {
      this.landmarkInfo.open = `${this._translate.instant('Yes')}`;
    } else {
      this.landmarkInfo.open = `${this._translate.instant('No')}`;
    }
    this.setArrivalTime(lat, lng);
    this.infoWindow.get(0)!.open(marker);
  }

  openUserLocationInfo(marker: any) {
    this._mapService
      .getAddressByCoordinates(this.center.lat, this.center.lng)
      .pipe(first())
      .subscribe({
        next: (addressInfo) => {
          this.userLocationInfo = addressInfo;
          this.infoWindow.get(1)!.open(marker);
        }
      });
  }

  openGoogleMaps() {
    openGoogleMaps(this.landmarkInfo.lat, this.landmarkInfo.lng);
  }
}