import { Component, OnInit, Input } from '@angular/core';
import { Article } from '../../models/article';

@Component({
  selector: 'app-article-preview-list',
  templateUrl: './article-preview-list.component.html',
  styleUrls: ['./article-preview-list.component.css']
})
export class ArticlePreviewListComponent implements OnInit {

  @Input() articles: Article[];

  constructor() { }

  ngOnInit() {
  }

}
