import { Pipe, PipeTransform } from "@angular/core";
import { RoleEnum } from "../models/Role";

@Pipe({
    name: 'role'
})
export class RolePipe implements PipeTransform {
    transform(value: any) : string {
        switch(value.description) {
            case "System":
                return RoleEnum.SYSTEM;
            case "Developer":
                return RoleEnum.DEVELOPER;
            default:
                return `Unknow role`;
        }
    }
}