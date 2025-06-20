import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { Router, RouterModule } from "@angular/router";
import { ButtonModule } from "primeng/button";
import { ToolbarModule } from "primeng/toolbar";
import { MenubarModule } from 'primeng/menubar';
import { AvatarModule } from "primeng/avatar";
import { AuthService } from "../../../services/auth.service";
import { MenuItem } from "primeng/api";
import { LogoComponent } from "../logo/logo.component";
import { User } from "../../../models/User";

@Component({
    selector: 'app-basic-view',
    standalone: true,
    imports: [
    CommonModule,
    RouterModule,
    ToolbarModule,
    ButtonModule,
    AvatarModule,
    MenubarModule,
    LogoComponent
],
    templateUrl: "./basic-view.component.html"
})
export class BasicViewComponent {
    avatar: string = "";
    user: User;
  
    constructor(
        private readonly authService: AuthService,
        private readonly router: Router
    ) {
        this.user = this.authService.getCurrentUser();
        this.avatar = this.user.name.split("")[0];
    }

    menuItems: MenuItem[] = [
        { label: 'Users', routerLink: ['../users'] },
        { label: 'Logs', routerLink: ['../logs'] },
    ];

    logoff() {
        this.authService.logout();
        return this.router.navigate(['auth/login']);
    }
}