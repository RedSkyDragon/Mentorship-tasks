import { Thing } from './thing';
import { Friend } from './friend';

export interface History {
    Id: string;
    LendDate: Date;
    ReturnDate: Date;
    Comment: string;
    Thing: Thing;
    Friend: Friend;
}
