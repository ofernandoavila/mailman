import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BasicViewComponent } from "../../components/basic-view/basic-view.component";
import { GridComponent } from "../../components/grid/grid.component";
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { User, UserFilter } from '../../../models/User';
import { Observable } from 'rxjs';
import { Pagination, Paged } from '../../../models/Pagination';
import { UserService } from '../../../services/user.service';
import { ApiResponse } from '../../../models/Api';
import { RolePipe } from '../../../pipes/RolePipe';

@Component({
	selector: 'app-users',
	imports: [
    CommonModule,
    BasicViewComponent,
    GridComponent,
	CheckboxModule,
	ButtonModule,
	RouterModule,
	RolePipe
],
	templateUrl: './users.component.html',
})
export class UsersComponent {
    constructor(private service: UserService) {
	}

	onSearchFilter = (term: string, filter: UserFilter) => {
		if(term !== "") {
			filter.Search = term;
		}

        return filter;
    }

    onFetchUsers = (
        pagination: Pagination,
        filter: UserFilter
    ): Observable<ApiResponse<Paged<User>>> => {
        return this.service.getAll(pagination, filter);
    };
}
