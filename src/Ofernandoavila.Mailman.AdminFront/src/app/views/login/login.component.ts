import { Component, OnInit } from '@angular/core';
import {
	FormBuilder,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { TokenService } from '../../../services/token.service';
import { AuthService } from '../../../services/auth.service';
import { LogoComponent } from "../../components/logo/logo.component";

@Component({
	selector: 'app-login',
	imports: [
    ButtonModule,
    CardModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    ProgressSpinnerModule,
    LogoComponent
],
	templateUrl: './login.component.html',
	styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
	loading = false;
	error: string | null = null;

	loginForm: FormGroup;

	constructor(
		private readonly fb: FormBuilder,
		private readonly router: Router,
		private readonly authService: AuthService
	) {}

	ngOnInit(): void {
		if (this.authService.isAuthenticated()) {
			this.router.navigate(['dashboard']);
		}

		this.loginForm = this.fb.group({
			user: ['', [Validators.required, Validators.email]],
			password: ['', [Validators.required]],
		});
	}

	onSubmit() {
		if(this.loginForm.invalid) return;

		this.error = null;

		this.isLoading();
		this.authService
			.login(this.loginForm.get('user').value, this.loginForm.get('password').value)
			.subscribe({
				next: () => {
					this.router.navigate(['dashboard']);
					this.isLoading(false);
				},
				error: ({ error }) => {
					this.error = error.message;
            		this.isLoading(false);
				}
			})		
	}

	isLoading(isLoading:boolean = true) {
    this.loading = isLoading;

    if(this.loading) {
      this.loginForm.controls['user'].disable();
      this.loginForm.controls['password'].disable();
    } else {
      this.loginForm.controls['user'].enable();
      this.loginForm.controls['password'].enable();
    }
  }
}
