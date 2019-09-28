import { Component, OnInit } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';

import { Article } from '../../models/article';
import { ArticleService } from '../../services/article.service';
import { SeoService } from '../../services/seo-service.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  previews: Article[] = [];

  constructor(private articleService: ArticleService, public authService: AuthService) {
  }

  ngOnInit() {
    this.articleService.getArticles().subscribe(previews => this.setPreviews(previews));
  }

  setPreviews(previews: Article[]): void {
    this.previews = previews;
  }

  logout() {
    this.authService.logout();
  }
}