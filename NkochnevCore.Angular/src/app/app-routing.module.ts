import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ArticleComponent } from './article/article.component';
import { ArticleEditComponent } from './article-edit/article-edit.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', component: HomeComponent },
  { path: 'articles/:key', component: ArticleComponent },
  { path: 'articles/edit/:key', component: ArticleEditComponent },
  { path: 'createarticle', component: ArticleEditComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
