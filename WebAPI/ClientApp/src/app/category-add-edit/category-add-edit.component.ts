import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Category } from '../models/category';
import { CategoryService } from '../services/category.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-category-add-edit',
  templateUrl: './category-add-edit.component.html',
  styleUrls: ['./category-add-edit.component.scss']
})
export class CategoryAddEditComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  formName: string;
  formDescription: string;
  id: number;
  errorMessage: any;
  existingCategory: Category;

  constructor(private categoryService: CategoryService, private formBuilder: FormBuilder,
              private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    this.actionType = 'Crear';
    this.formName = 'name';
    this.formDescription = 'description';
    if (this.avRoute.snapshot.params[idParam]) {
      this.id = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        id: 0,
        name: ['', [Validators.required]],
        description: ['', [Validators.required]],
      }
    );
  }

  ngOnInit() {

    if (this.id > 0) {
      this.actionType = 'Editar';
      this.categoryService.getCategory(this.id)
        .subscribe(data => (
          this.existingCategory = data,
          this.form.controls[this.formName].setValue(data.name),
          this.form.controls[this.formDescription].setValue(data.description)
        ));
    }
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Crear') {
      const category: Category = {
        id: null,
        name: this.form.get(this.formName).value,
        description: this.form.get(this.formDescription).value
      };

      this.categoryService.saveCategory(category)
        .subscribe((data) => {
          this.router.navigate(['/category']);
        });
    }

    if (this.actionType === 'Editar') {
      const category: Category = {
        id: this.existingCategory.id,
        name: this.form.get(this.formName).value,
        description: this.form.get(this.formDescription).value
      };
      this.categoryService.updateCategory(category.id, category)
        .subscribe((data) => {
          this.router.navigate(['/category']);
        });
    }
  }

  cancel() {
    this.router.navigate(['/category']);
  }

  get name() { return this.form.get(this.formName); }
  get description() { return this.form.get(this.formDescription); }
}
