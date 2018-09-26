import { History } from './history';
import { Category } from './category';
import { Friend } from './friend';
import { Thing } from './thing';

export function HistoryFilter(data: History, filter: string): boolean {
    const lendDate = new Date(data.LendDate).toLocaleDateString('en-EN');
    const returnDate = new Date(data.ReturnDate).toLocaleDateString('en-EN');
    const string = (
        data.Comment + lendDate + returnDate + data.Thing.Name + data.Friend.Name)
        .toLowerCase();
    return string.indexOf(filter.toLowerCase()) !== -1;
}

export function CategoriesFilter(data: Category, filter: string): boolean {
    const string = (data.Name + data.About).toLowerCase();
    return string.indexOf(filter.toLowerCase()) !== -1;
}

export function FriendsFilter(data: Friend, filter: string): boolean {
    const string = (data.Name + data.Contacts).toLowerCase();
    return string.indexOf(filter.toLowerCase()) !== -1;
}

export function ThingsFilter(data: Thing, filter: string): boolean {
    const string = (data.Name + data.About).toLowerCase();
    return string.indexOf(filter.toLowerCase()) !== -1;
}
