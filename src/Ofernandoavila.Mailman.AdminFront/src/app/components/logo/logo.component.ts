import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";

@Component({
    selector: 'app-logo',
    standalone: true,
    imports: [
        CommonModule
    ],
    templateUrl: "./logo.component.html"
})
export class LogoComponent {
    constructor(
    ) {
    }
}