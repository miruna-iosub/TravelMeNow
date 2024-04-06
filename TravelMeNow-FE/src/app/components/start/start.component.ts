import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Preferences } from '@capacitor/preferences';

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.css'],
})
export class StartComponent implements OnInit {
  frFlag!: boolean;
  roFlag!: boolean;
  enFlag!: boolean;
  esFlag!: boolean;

  constructor(
    public translate: TranslateService,
  ) {
    translate.addLangs(['en', 'fr', 'ro', 'es']);
  }
  async ngOnInit() {
    Preferences.get({ key: 'languageFlag' }).then((value) => {
      if (value.value != null) {
        this.frFlag = JSON.parse(value.value!);
        this.enFlag = !this.frFlag;
        this.roFlag = !this.enFlag && !this.frFlag;
        this.esFlag = !this.enFlag && !this.frFlag && !this.roFlag;
  
        const defaultLang = this.frFlag ? 'en' : (this.enFlag ? 'fr' : (this.roFlag ? 'es' : (this.esFlag ? 'ro' : 'es')));
        this.translate.setDefaultLang(defaultLang);
      } else {
        this.frFlag = true;
        this.enFlag = false;
        this.roFlag = false;
        this.esFlag = false;
  
        this.translate.setDefaultLang('en');
      }
    });
  }
  
  switchLanguage(lang: string): void {
    this.translate.use(lang);
  }

  async switchToFr() {
    this.switchLanguage('fr');
    await Preferences.set({
      key: 'languageFlag',
      value: 'false',
    });
    this.frFlag = false;
    this.enFlag = true;
    this.roFlag = false;
    this.esFlag = false;
  }

  async switchToEn() {
    this.switchLanguage('en');
    await Preferences.set({
      key: 'languageFlag',
      value: 'true',
    });
    this.roFlag = true;
    this.frFlag = false;
    this.enFlag = false;
    this.esFlag = false;
  }

  async switchToRo() {
    this.switchLanguage('ro');
    await Preferences.set({
      key: 'languageFlag',
      value: 'false',
    });
    this.frFlag = false;
    this.roFlag = false;
    this.enFlag = false;
    this.esFlag = true;
  }

  async switchToEs() {
    this.switchLanguage('es');
    await Preferences.set({
      key: 'languageFlag',
      value: 'false',
    });
    this.frFlag = true;
    this.roFlag = false;
    this.enFlag = false;
    this.esFlag = false;
  }
}
