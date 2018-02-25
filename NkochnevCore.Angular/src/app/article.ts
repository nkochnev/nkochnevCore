import { ArticleBase } from './article-base'

export class Article implements ArticleBase {
  key: string;
  title: string;
  created: string;
  modified: string;
  createdIso: string;
  modifiedIso: string;

  content: string;
  previewContent: string;
  seoKeyWords: string;
  seoDescription: string;
}
