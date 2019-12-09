import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule} from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthorComponent } from './author/author.component';
import { AuthorsComponent } from './authors/authors.component';
import { AuthorAddEditComponent } from './author-add-edit/author-add-edit.component';
import { AuthorService } from './services/author.service';
import { BooksComponent } from './books/books.component';
import { BookComponent } from './book/book.component';
import { BookAddEditComponent } from './book-add-edit/book-add-edit.component';
import { CategoryComponent } from './category/category.component';
import { CategoriesComponent } from './categories/categories.component';
import { CategoryAddEditComponent } from './category-add-edit/category-add-edit.component';


@NgModule({
  declarations: [
    AppComponent,
    AuthorComponent,
    AuthorsComponent,
    AuthorAddEditComponent,
    BooksComponent,
    BookComponent,
    BookAddEditComponent,
    CategoryComponent,
    CategoriesComponent,
    CategoryAddEditComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule,
    NgSelectModule
  ],
  providers: [
    AuthorService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
