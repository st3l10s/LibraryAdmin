import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../models/book';
import { BookService } from '../services/book.service';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {

  books$: Observable<Book[]>;

  constructor(private bookService: BookService) { }

  ngOnInit() {
    this.loadBooks();
  }

  loadBooks() {
    this.books$ = this.bookService.getBooks();
  }

  delete(id: number) {
    const ans = confirm('Quiere eliminar el libro con id: ' + id + '?');

    if (ans) {
      this.bookService.deleteBook(id).subscribe((data) => {
        this.loadBooks();
      });
    }
  }
}
