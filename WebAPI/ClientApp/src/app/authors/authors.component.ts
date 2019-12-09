import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorService } from '../services/author.service';
import { Author } from '../models/author';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.scss']
})
export class AuthorsComponent implements OnInit {
  authors$: Observable<Author[]>;

  constructor(private authorService: AuthorService) {
  }

  ngOnInit() {
    this.loadAuthors();
  }

  loadAuthors() {
    this.authors$ = this.authorService.getAuthors();
  }

  delete(id: number) {
    const ans = confirm('Quiere eliminar el autor con id: ' + id + '?');
    if (ans) {
      this.authorService.deleteAuthor(id).subscribe((data) => {
        this.loadAuthors();
      });
    }
  }
}
