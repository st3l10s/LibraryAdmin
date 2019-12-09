import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorComponent } from './author/author.component';
import { AuthorsComponent } from './authors/authors.component';
import { AuthorAddEditComponent } from './author-add-edit/author-add-edit.component';
import { BooksComponent } from './books/books.component';
import { BookAddEditComponent } from './book-add-edit/book-add-edit.component';
import { BookComponent } from './book/book.component';
import { CategoryAddEditComponent } from './category-add-edit/category-add-edit.component';
import { CategoryComponent } from './category/category.component';
import { CategoriesComponent } from './categories/categories.component';


const routes: Routes = [
  { path: '', component: BooksComponent },
  { path: 'book/:id', component: BookComponent },
  { path: 'book/add', component: BookAddEditComponent },
  { path: 'book/edit/:id', component: BookAddEditComponent },
  { path: 'author', component: AuthorsComponent },
  { path: 'author/add', component: AuthorAddEditComponent },
  { path: 'author/:id', component: AuthorComponent },
  { path: 'author/edit/:id', component: AuthorAddEditComponent },
  { path: 'category', component: CategoriesComponent },
  { path: 'category/add', component: CategoryAddEditComponent },
  { path: 'category/:id', component: CategoryComponent },
  { path: 'category/edit/:id', component: CategoryAddEditComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
