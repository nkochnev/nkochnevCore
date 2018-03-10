import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // angular 4.x and greater
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CKEditorModule } from 'ng2-ckeditor';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { ScrollToTopComponent } from './scroll-to-top/scroll-to-top.component';
import { ArticlePreviewComponent } from './article-preview/article-preview.component';
import { ArticlePreviewListComponent } from './article-preview-list/article-preview-list.component';
import { AppRoutingModule } from "./app-routing.module";
import { ArticleComponent } from './article/article.component';
import { ArticleService } from './article.service';
import { SeoService } from './seo-service.service';
import { AuthService } from './auth.service';
import { ArticleEditComponent } from './article-edit/article-edit.component';
import { AuthComponent } from './auth/auth.component';
import { TokenInterceptor } from './tokenInterceptor';
import { AuthGuard } from './auth-guard.service';
import { RoutingState } from './RoutingState';

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
    AuthComponent
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
    RoutingState,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
