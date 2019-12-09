import { Author } from './author';
import { Category } from './category';

export class Book {
    id: number;
    name: string;
    isbn: number;
    authorId: number;
    author?: Author;
    categories: Category[];
}
