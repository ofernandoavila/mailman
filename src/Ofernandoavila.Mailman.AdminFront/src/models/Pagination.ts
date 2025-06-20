export interface Paged<T> {
    totalRecords: number;
    pagedData: T[];
}

export interface Pagination {
    PageNumber?: number;
    PageSize?: number;
    OrderBy?: number;
    Active?: boolean;
    Desc?: boolean;
}