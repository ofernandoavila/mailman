import { Injectable } from "@angular/core";
import { environment } from "../environments/environment";
import { User } from "../models/User";

@Injectable({
    providedIn: 'root'
})
export class TokenService {
    private readonly TOKEN_KEY = `${environment.applicationName}.access_token`;
    private readonly USER_KEY = `${environment.applicationName}.user`;

    getToken() : string | null {
        return localStorage.getItem(this.TOKEN_KEY);
    }

    setToken(token: string | null) {
        if(token) {
            localStorage.setItem(this.TOKEN_KEY, token);
        } else {
            localStorage. removeItem(this.TOKEN_KEY);
        }
    }

    getUser(): User | null {
        const user = localStorage.getItem(this.USER_KEY);

        return user ? JSON.parse(user) : null;
    }

    setUser(user: any | null) {
        if (user) {
            localStorage.setItem(this.USER_KEY, JSON.stringify(user));
        } else {
            localStorage.removeItem(this.USER_KEY);
        }
    }

    clear() {
        localStorage.removeItem(this.TOKEN_KEY);
        localStorage.removeItem(this.USER_KEY);
    }
}