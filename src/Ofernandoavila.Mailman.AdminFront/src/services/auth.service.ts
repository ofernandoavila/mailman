import { Injectable } from "@angular/core";
import { environment } from "../environments/environment";
import { HttpClient } from "@angular/common/http";
import { TokenService } from "./token.service";
import { tap } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private readonly API_URL = `${environment.apiBaseUrl}`;

    constructor(
        private http: HttpClient,
        private tokenService: TokenService
    ) {}

    login(user: string, password: string) {
        return this.http.post(`${this.API_URL}/login`, { email: user, password }).pipe(
            tap((response: any) => {
                this.tokenService.setToken(response.data.accessToken);
                this.tokenService.setUser(response.data.userToken);
            })
        )
    }

    logout() {
        this.tokenService.clear();
    }

    isAuthenticated() {
        return !!this.tokenService.getToken();
    }

    getCurrentUser() {
        return this.tokenService.getUser();
    }
}