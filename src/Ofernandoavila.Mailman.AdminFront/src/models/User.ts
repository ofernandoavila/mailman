import { RoleEnum } from "./Role";

export interface User {
    id: string;
    roleId: string;
    active: boolean;
    firstAccess: boolean;
    refreshToken: string;
    name: string;
    email: string;
    role: RoleEnum;
}

export interface UserFilter {
    RoleId?: string;
    Search?: string;
}