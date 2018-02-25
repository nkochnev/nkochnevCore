import { Injectable } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';

@Injectable()
export class SeoService {

  constructor(private meta: Meta, private title: Title) { }

  setSeoInfo(title: string, keyWords: string, description: string): void {
    this.meta.updateTag({
      name: 'keywords',
      content: keyWords
    });
    this.meta.updateTag({
      name: 'description',
      content: description
    });
    this.meta.updateTag({
      property: 'og:title',
      content: title
    });
    this.meta.updateTag({
      property: 'og:description',
      content: description
    });
    this.meta.updateTag({
      property: 'twitter:title',
      content: title
    });
    this.meta.updateTag({
      property: 'twitter:description',
      content: description
    });

    this.title.setTitle(title);
  }

}
