import { ActiveLend } from './active-lend';
import { History } from './history';

export interface FilteredLends {
    ActiveLends: ActiveLend[];
    History: History[];
}
