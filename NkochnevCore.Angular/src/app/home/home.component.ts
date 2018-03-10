import { Component, OnInit } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';

import { ArticlePreview } from '../article-preview';
import { ArticleService } from '../article.service';
import { SeoService } from '../seo-service.service';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  previews: ArticlePreview[] = [];

  constructor(private seoService: SeoService, private articleService: ArticleService, public authService: AuthService) {
  }

  ngOnInit() {
    this.articleService.getArticles().subscribe(previews => this.setPreviews(previews));

    const keywords = "Кочнев Николай, Николай Кочнев, Кочнев Николай Иванович, Кочнев Николай Екатеринбург, Николай Кочнев Екатеринбург, Николай Иванович Кочнев, nkochnev, nkochnev.ru, веб-программист, web-программист, Единый Расчетный Центр teamlead, Единый Расчетный Центр";
    const description = 'Кочнев Николай, web-программист и teamlead компании Единый Расчетный Центр';
    const title = 'Кочнев Николай - веб-программист и teamlead';

    this.seoService.setSeoInfo(title, keywords, description);
  }

  setPreviews(previews: ArticlePreview[]): void {
    this.previews = previews;
  }

  logout(){
    this.authService.logout();
  }
}
