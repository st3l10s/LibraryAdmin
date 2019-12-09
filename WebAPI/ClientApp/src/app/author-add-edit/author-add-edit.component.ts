import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthorService } from '../services/author.service';
import { Author } from '../models/author';

@Component({
  selector: 'app-author-add-edit',
  templateUrl: './author-add-edit.component.html',
  styleUrls: ['./author-add-edit.component.scss']
})
export class AuthorAddEditComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  formName: string;
  formLastName: string;
  formBirthDay: string;
  id: number;
  errorMessage: any;
  existingAuthor: Author;

  constructor(private authorService: AuthorService, private formBuilder: FormBuilder,
              private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    this.actionType = 'Crear';
    this.formName = 'name';
    this.formLastName = 'lastName';
    this.formBirthDay = 'birthDay';
    if (this.avRoute.snapshot.params[idParam]) {
      this.id = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        id: 0,
        name: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        birthDay: ['', [Validators.required]],
      }
    );
  }

  ngOnInit() {

    if (this.id > 0) {
      this.actionType = 'Editar';
      this.authorService.getAuthor(this.id)
        .subscribe(data => (
          this.existingAuthor = data,
          this.form.controls[this.formName].setValue(data.name),
          this.form.controls[this.formBirthDay].setValue(data.birthDay),
          this.form.controls[this.formLastName].setValue(data.lastName)
        ));
    }
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Crear') {
      const author: Author = {
        id: null,
        birthDay: this.form.get(this.formBirthDay).value,
        name: this.form.get(this.formName).value,
        lastName: this.form.get(this.formLastName).value
      };

      this.authorService.saveAuthor(author)
        .subscribe((data) => {
          this.router.navigate(['/author']);
        });
    }

    if (this.actionType === 'Editar') {
      const author: Author = {
        id: this.existingAuthor.id,
        birthDay: this.form.get(this.formBirthDay).value,
        name: this.form.get(this.formName).value,
        lastName: this.form.get(this.formLastName).value
      };
      this.authorService.updateAuthor(author.id, author)
        .subscribe((data) => {
          this.router.navigate(['/author']);
        });
    }
  }

  cancel() {
    this.router.navigate(['/author']);
  }

  get name() { return this.form.get(this.formName); }
  get lastName() { return this.form.get(this.formLastName); }
  get birthDay() {return this.form.get(this.formBirthDay); }
}
