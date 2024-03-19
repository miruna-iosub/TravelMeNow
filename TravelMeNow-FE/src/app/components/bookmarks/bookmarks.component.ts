import { Component, Injectable, NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Observable, Subject } from 'rxjs';

interface BuildingInfo {
  name: string;
  details: string[];
  link: string;
  opening_hours: string;
}

@Component({
  templateUrl: './bookmarks.component.html',
  styleUrls: ['./bookmarks.component.scss'],

})

@Injectable({
  providedIn: 'root',
})

export class BookmarksComponent {
  constructor(
  ) { }

  public bookmarks: BuildingInfo[] = [];

  private trigger: Subject<any> = new Subject();
  webcamImage: any;
  private nextWebcam: Subject<any> = new Subject();
  public poiDetail: any = null;
  public buildingInfo: BuildingInfo | null = null;

  sysImage = '';

  ngOnInit() {
    this.loadBookmarks();
}

  public get triggerObservable(): Observable<void> {
    return this.trigger.asObservable();
  }

  public get invokeObservable(): Observable<any> {
    return this.trigger.asObservable();
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

      this.loadBookmarks();
    }
  }

  private getBookmarks(): BuildingInfo[] {
    const bookmarks = localStorage.getItem('bookmarks');
    return bookmarks ? JSON.parse(bookmarks) : [];
  }

  public isBookmarked: boolean = false;


  public loadBookmarks(): void {
    const bookmarks = localStorage.getItem('bookmarks');
    this.bookmarks = bookmarks ? JSON.parse(bookmarks) : [];
  }
  public clearBookmarks(): void {
    localStorage.removeItem('bookmarks');
    this.bookmarks = [];
  }

}