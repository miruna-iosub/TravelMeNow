import { CapacitorConfig } from '@capacitor/cli';


const config: CapacitorConfig = {
  appId: 'com.license.travelmenow',
  appName: 'TravelMeNow',
  webDir: 'dist/travelmenow',
  bundledWebRuntime: false,
  server: {
    url: 'http://172.20.10.2:4200',
    cleartext: true
  }
};

export default config;
