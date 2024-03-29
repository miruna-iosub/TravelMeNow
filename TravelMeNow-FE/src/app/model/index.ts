export interface Landmark {
  position: {
    lat: number;
    lng: number;
  };
  name: string;
  rating: number;
  open_now: boolean;
}

export interface LandmarkDTO {
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

export interface GapDTO {
  gap: {
    text: string;
  };
  duration: {
    text: string;
  };
}

export interface Gap {
  eta: string;
}

export interface GoogleLocationDTO {
  formattedGoogleLocation: string;
}

export interface GoogleLocation {
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
  eta: string;
}