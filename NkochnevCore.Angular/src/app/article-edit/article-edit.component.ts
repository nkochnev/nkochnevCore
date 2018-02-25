import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";

import { ArticleService } from '../article.service';
import { Article } from '../article';
import { SeoService } from '../seo-service.service';

@Component({
  selector: 'app-article-edit',
  templateUrl: './article-edit.component.html',
  styleUrls: ['./article-edit.component.css']
})
export class ArticleEditComponent implements OnInit {
  article: Article;
  key: string;

  constructor(private seoService: SeoService, private articleService: ArticleService,
    private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.key = this.route.snapshot.paramMap.get('key');
    if (this.key) {
      this.articleService.getArticleByKey(this.key).subscribe(article => this.setArticle(article));
    } else {
      this.setArticle(new Article());
    }
  }

  setArticle(article: Article): void {
    this.article = article;

    const keywords = article.seoKeyWords;
    const description = article.seoDescription;
    const title = 'Редактирование статьи: ' + article.title;

    this.seoService.setSeoInfo(title, keywords, description);
  }

  onSubmit(): void {

    if (this.key) {
      this.articleService.updateArticle(this.key, this.article).subscribe(
        result => this.router.navigate(['articles/' + this.article.key]));
    } else {
      this.articleService.createArticle(this.article).subscribe(
        result => this.router.navigate(['articles/' + this.article.key]));
    }

  }

}
