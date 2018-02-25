import {ArticleBase} from './article-base';

export class ArticlePreview implements ArticleBase {
  key: string;
  title: string;
  created: string;
  modified: string;
  createdIso: string;
  modifiedIso: string;

  previewContent: string;
}
