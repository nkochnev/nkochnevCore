import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // angular 4.x and greater
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CKEditorModule } from 'ng2-ckeditor';

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { ScrollToTopComponent } from './components/scroll-to-top/scroll-to-top.component';
import { ArticlePreviewComponent } from './components/article-preview/article-preview.component';
import { ArticlePreviewListComponent } from './components/article-preview-list/article-preview-list.component';
import { AppRoutingModule } from "./app-routing.module";
import { ArticleComponent } from './components/article/article.component';
import { ArticleService } from './services/article.service';
import { SeoService } from './services/seo-service.service';
import { AuthService } from './services/auth.service';
import { ArticleEditComponent } from './components/article-edit/article-edit.component';
import { AuthComponent } from './components/auth/auth.component';
import { TokenInterceptor } from './tokenInterceptor';
import { AuthGuard } from './services/auth-guard.service';
import { NotFoundComponent } from './components/not-found/not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    ScrollToTopComponent,
    ArticlePreviewComponent,
    ArticlePreviewListComponent,
    ArticleComponent,
    ArticleEditComponent,
    AuthComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AngularFontAwesomeModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    CKEditorModule
  ],
  providers: [
    ArticleService,
    SeoService,
    AuthService,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
