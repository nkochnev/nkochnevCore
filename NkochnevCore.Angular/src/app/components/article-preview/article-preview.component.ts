import { Component, OnInit, Input } from '@angular/core';
import { ArticlePreview } from '../../models/article-preview';

@Component({
  selector: 'app-article-preview',
  templateUrl: './article-preview.component.html',
  styleUrls: ['./article-preview.component.css']
})
export class ArticlePreviewComponent implements OnInit {

  @Input() article: ArticlePreview;

  constructor() { }

  ngOnInit() {
  }

}
