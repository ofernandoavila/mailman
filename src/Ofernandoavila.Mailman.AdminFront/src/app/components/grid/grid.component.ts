import { CommonModule } from "@angular/common";
import { Component, effect, Input, OnInit, signal, TemplateRef } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { SelectModule } from 'primeng/select';
import { TableModule } from 'primeng/table';
import { PaginatorState } from 'primeng/paginator';
import { PaginatorModule } from 'primeng/paginator';
import { CardModule } from 'primeng/card';
import { Observable } from "rxjs";
import { Pagination } from "../../../models/Pagination";

@Component({
    selector: 'app-grid',
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TableModule,
        TagModule,
        IconFieldModule,
        InputIconModule,
        InputTextModule,
        MultiSelectModule,
        SelectModule,
        RouterModule,
        PaginatorModule,
        ButtonModule,
        CardModule
    ],
    templateUrl: './grid.component.html',
})
export class GridComponent implements OnInit {
    data = signal([]);
    @Input() searchLabel: string = "Buscar por E-mail";
    @Input() fetchData!: (pagination: Pagination, filter: any) => Observable<any>;
    @Input() onSearchFilter?: (term: string, filter: any) => any;
    
    @Input() title: string = "";
    @Input() filtro!: TemplateRef<any>;
    @Input() tableHeader!: TemplateRef<any>;
    @Input() tableRow!: TemplateRef<any>;

    totalRecords: number = 0;
    perPage: number = 10;
    page: number = 1;

    search: string = "";

    ngOnInit(): void {
        this.fetch();
    }

    effect = effect(() => {
        this.fetch({ PageNumber: 1 });
    });

    changePage({ page }: PaginatorState) {
        this.fetch({ PageNumber: page! + 1 });
    }

    onSearch() {
        let filter: any | null = null;

        if(this.onSearchFilter) {
            if(!filter)
    			filter = {} as any;
            
            filter = this.onSearchFilter(this.search, filter);
        }

        if(filter === null) return;

        return this.fetch({ PageNumber: 1 }, filter);
    }

    fetch(pagination?: Pagination, filters?: any) {
        if(pagination === undefined)
            pagination = { PageNumber: 1 };

        if(this.onSearchFilter) {
            if(filters === undefined)
                filters = {} as any;

            filters = this.onSearchFilter("", filters);
        }

        this.fetchData(pagination, filters)
            .subscribe({
                next: ({ data }) => {
                    const { pagedData, totalRecords } = data;

                    this.data.set(pagedData);
                    this.totalRecords = totalRecords;
                }
            })

    }
}