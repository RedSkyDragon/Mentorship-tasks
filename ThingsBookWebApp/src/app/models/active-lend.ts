import { Thing } from './thing';
import { Friend } from './friend';

export interface ActiveLend {
    LendDate: Date;
    Comment: string;
    Thing: Thing;
    Friend: Friend;
}
