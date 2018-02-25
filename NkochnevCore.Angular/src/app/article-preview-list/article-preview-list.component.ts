import { Component, OnInit, Input } from '@angular/core';
import { ArticlePreview } from '../article-preview';

@Component({
  selector: 'app-article-preview-list',
  templateUrl: './article-preview-list.component.html',
  styleUrls: ['./article-preview-list.component.css']
})
export class ArticlePreviewListComponent implements OnInit {

  @Input() articles: ArticlePreview[];

  constructor() { }

  ngOnInit() {
  }

}
