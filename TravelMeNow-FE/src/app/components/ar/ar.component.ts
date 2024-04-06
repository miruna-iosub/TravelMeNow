import { Component, Injectable, NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { WebcamImage, WebcamInitError, WebcamUtil } from 'ngx-webcam';
import { ImageService } from './services/image.service';
import { TranslateService } from '@ngx-translate/core';
import { Location } from '@angular/common';

interface BuildingInfo {
  name: string;
  details: string[];
  link: string;
  opening_hours: string;
}

@Component({
  selector: 'app-image-webcam',
  templateUrl: './ar.component.html',
  styleUrls: ['./ar.component.css'],

})

@Injectable({
  providedIn: 'root',
})

export class ImageWebcamComponent {
  constructor(
    private imageService: ImageService,
    private _translate: TranslateService,
    private location: Location
  ) { }

  public bookmarks: BuildingInfo[] = [];

  private trigger: Subject<any> = new Subject();
  webcamImage: any;
  private nextWebcam: Subject<any> = new Subject();
  public poiDetail: any = null;
  public buildingInfo: BuildingInfo | null = null;

  sysImage = '';

  ngOnInit() {
    this.checkBookmark();
  }

  public getSnapshot(): void {
    this.trigger.next(void 0);
  }

  public get triggerObservable(): Observable<void> {
    return this.trigger.asObservable();
  }

  switchLanguage(language: string) {
    this._translate.use(language);
  }

  public recognizeImg(webcamImage: WebcamImage): void {
    const base64Data = webcamImage.imageAsDataUrl.split(',')[1];
    this.imageService.sendImage(base64Data).subscribe(
      (data: any) => {
        const landmarkKey = `landmark_details.${data.name}`;

        this._translate.get(landmarkKey).subscribe((translations: any) => {
          this.buildingInfo = {
            name: translations.name,
            details: [translations.details],
            link: translations.link,
            opening_hours: translations.opening_hours
          };
        });
      },
      (error) => {
        console.error('Error sending image', error);
      }
    );
  }

  goBack() {
    this.location.back();
  }

  public get invokeObservable(): Observable<any> {
    return this.trigger.asObservable();
  }

  public get nextWebcamObservable(): Observable<any> {
    return this.nextWebcam.asObservable();
  }

  public toggleBookmark(): void {
    if (this.buildingInfo) {
      const bookmarks = this.getBookmarks();
      const index = bookmarks.findIndex((b: BuildingInfo) => b.name === this.buildingInfo?.name);

      if (index > -1) {
        bookmarks.splice(index, 1);
      } else {
        bookmarks.push(this.buildingInfo);
      }

      localStorage.setItem('bookmarks', JSON.stringify(bookmarks));

      this.checkBookmark();
      this.loadBookmarks();
    }
  }

  private getBookmarks(): BuildingInfo[] {
    const bookmarks = localStorage.getItem('bookmarks');
    return bookmarks ? JSON.parse(bookmarks) : [];
  }

  public isBookmarked: boolean = false;

  private checkBookmark(): void {
    if (this.buildingInfo) {
      const bookmarks = this.getBookmarks();
      this.isBookmarked = bookmarks.some((b: BuildingInfo) => b.name === this.buildingInfo?.name);
      console.log(this.isBookmarked);
    }
  }

  public loadBookmarks(): void {
    this.bookmarks = this.getBookmarks();
    console.log('Bookmarks loaded:', this.bookmarks);
  }

  public clearBookmarks(): void {
    localStorage.removeItem('bookmarks');
    this.bookmarks = [];
  }
}