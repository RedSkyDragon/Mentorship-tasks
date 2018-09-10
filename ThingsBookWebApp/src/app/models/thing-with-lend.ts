import { Lend } from './lend';

export interface ThingWithLend {
    Id: string;
    CategoryId: string;
    Name: string;
    About: string;
    Lend: Lend;
}
