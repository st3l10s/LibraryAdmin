import { CategoryBookSave } from './categoryBookSave';

export class BookSave {
    name: string;
    isbn: number;
    authorId: number;
    categories: CategoryBookSave[];
}
