export interface PointOfInterest {
  position: {
    lat: number;
    lng: number;
  };
  name: string;
  rating: number;
  open_now: boolean;
}

export interface PointOfInterestDTO {
  geometry: {
    location: {
      lat: number;
      lng: number;
    };
  };
  name: string;
  place_id: string;
  rating: number;
  icon: string;
  opening_hours: {
    open_Now: boolean;
  };
}

export interface DistanceDTO {
  distance: {
    text: string;
  };
  duration: {
    text: string;
  };
}

export interface Distance {
  kmNumber: string;
  eta: string;
}

export interface AddressDTO {
  formatted_address: string;
}

export interface Address {
  street: string;
  city: string;
  country: string;
}

export interface Marker {
  position: {
    lat: number;
    lng: number;
  };
  label: {
    color: string;
    text: string;
  };
  title: string;
  options: {
    animation: google.maps.Animation;
  };
  icon: {
    url: string;
    scaledSize: google.maps.Size;
  };
  rating: number;
  open_now: boolean;
}

export interface MarkerInfo {
  name: string;
  rating: string | number;
  lat: number;
  lng: number;
  open: string | undefined;
  distance: string;
  eta: string;
}