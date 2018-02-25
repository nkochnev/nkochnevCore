import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";

import { ArticleService } from '../article.service';
import { Article } from '../article';
import { SeoService } from '../seo-service.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {
  article: Article;

  constructor(private seoService: SeoService, private articleService: ArticleService, 
    private route: ActivatedRoute) { }

  ngOnInit() {
    const key = this.route.snapshot.paramMap.get('key');
    this.articleService.getArticleByKey(key).subscribe(article=>this.setArticle(article));
  }

  setArticle(article : Article) : void {
    this.article = article;

    const keywords = article.seoKeyWords;
    const description = article.seoDescription;
    const title = article.title;

    this.seoService.setSeoInfo(title, keywords, description);
  }

}
