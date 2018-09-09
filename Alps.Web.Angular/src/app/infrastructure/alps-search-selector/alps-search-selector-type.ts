import { Observable } from "rxjs";

export interface AlpsSearchSelectorOption {
    value: any;
    displayValue: string;
}
export interface AlpsSearchSelectorDataSource {
    displayValue: (value: any) => Observable<AlpsSearchSelectorOption | null>;
    search: (term: string) => Observable<AlpsSearchSelectorOption[]>;
    match?: (search: string, entry: AlpsSearchSelectorOption) => boolean;
}