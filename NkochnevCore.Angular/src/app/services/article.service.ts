import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Article } from '../models/article';
import { environment } from '../../environments/environment';

@Injectable()
export class ArticleService {

  constructor(private http: HttpClient) { }

  articleUrl: string = '/api/articles';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  getArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(this.articleUrl);
  }

  getArticleByKey(key: string): Observable<Article> {
    const url = this.articleUrl + '/' + key;
    return this.http.get<Article>(url);
  }

  updateArticle(key: string, article: Article): Observable<Article> {
    const url = this.articleUrl + '/' + key;
    return this.http.put<Article>(url, article, this.httpOptions);
  }

  createArticle(article: Article): Observable<Article> {
    return this.http.post<Article>(this.articleUrl, article, this.httpOptions);
  }
}
