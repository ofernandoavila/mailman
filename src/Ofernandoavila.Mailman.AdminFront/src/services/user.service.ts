import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { TokenService } from './token.service';
import { Paged, Pagination } from '../models/Pagination';
import { buildSearchQuery } from '../utils/search-query';
import { User, UserFilter } from '../models/User';
import { ApiResponse } from '../models/Api';

@Injectable({
	providedIn: 'root',
})
export class UserService {
	private readonly API_URL = `${environment.apiBaseUrl}/User`;

	constructor(private http: HttpClient, private tokenService: TokenService) {}

	getAll(pagination: Pagination, filter: UserFilter) {
		let url = this.API_URL;

		if (pagination) {
			url += '?' + buildSearchQuery(pagination);
		}

		if (filter) {
			const query = buildSearchQuery(filter);

			url += url.includes('?') ? '&' + query : '?' + query;
		}

		return this.http.get<ApiResponse<Paged<User>>>(`${url}`);
	}
}
