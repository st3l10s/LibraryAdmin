import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Book } from '../models/book';
import { BookSave } from '../models/bookSave';
import { Category } from '../models/category';
import { BookService } from '../services/book.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Author } from '../models/author';
import { Observable } from 'rxjs';
import { AuthorService } from '../services/author.service';
import { CategoryService } from '../services/category.service';

@Component({
  selector: 'app-book-add-edit',
  templateUrl: './book-add-edit.component.html',
  styleUrls: ['./book-add-edit.component.scss']
})
export class BookAddEditComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  id: number;
  formName: string;
  formIsbn: string;
  formAuthorId: string;
  formCategories: string;
  errorMessage: any;
  existingBook: Book;
  authors$: Observable<Author[]>;
  allCategories$: Observable<Category[]>;

  constructor(private bookService: BookService, private formBuilder: FormBuilder, private router: Router,
              private avRoute: ActivatedRoute, private authorService: AuthorService,
              private categoryService: CategoryService) {
    const idParam = 'id';
    this.actionType = 'Crear';
    this.formName = 'name';
    this.formIsbn = 'isbn';
    this.formAuthorId = 'authorId';
    this.formCategories = 'categories';
    if (this.avRoute.snapshot.params[idParam]) {
      this.id = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group({
      id: 0,
      name: ['', [Validators.required]],
      isbn: ['', [Validators.required]],
      authorId: [0, [Validators.required]],
      categories: [[], [Validators.required]]
    });

    this.authors$ = this.authorService.getAuthors();
    this.allCategories$ = this.categoryService.getCategories();
  }

  ngOnInit() {
    if (this.id > 0) {
      this.actionType = 'Editar';
      this.bookService.getBook(this.id)
      .subscribe(data => (
        this.existingBook = data,
        this.form.controls[this.formName].setValue(data.name),
        this.form.controls[this.formIsbn].setValue(data.isbn),
        this.form.controls[this.formAuthorId].setValue(data.author.id),
        this.form.controls[this.formCategories].setValue(data.categories)
      ));
    }
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Crear') {
      const book: BookSave = {
        name: this.form.get(this.formName).value,
        isbn: this.form.get(this.formIsbn).value,
        authorId: parseInt(this.form.get(this.formAuthorId).value, 10),
        categories: this.form.get(this.formCategories).value
      };

      this.bookService.saveBook(book)
      .subscribe(() => {
        this.router.navigate(['/']);
      });
    }

    if (this.actionType === 'Editar') {
      const book: BookSave = {
        name: this.form.get(this.formName).value,
        isbn: this.form.get(this.formIsbn).value,
        authorId: parseInt(this.form.get(this.formAuthorId).value, 10),
        categories: this.form.get(this.formCategories).value
      };

      this.bookService.updateBook(this.id, book)
      .subscribe(() => {
        this.router.navigate(['/']);
      });
    }
  }

  cancel() {
    this.router.navigate(['/']);
  }

  get name() { return this.form.get(this.formName); }
  get isbn() { return this.form.get(this.formIsbn); }
  get authorId() { return this.form.get(this.formAuthorId); }
  get categories() { return this.form.get(this.formCategories); }
}
