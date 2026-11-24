import { HttpErrorResponse } from '@angular/common/http';
import { Component, DestroyRef, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login-component.html',
})
export class LoginComponent {
  private readonly fb = inject(FormBuilder).nonNullable;
  private readonly authService = inject(AuthService);
  private readonly destroyRef = inject(DestroyRef);
  private toastTimer: ReturnType<typeof setTimeout> | undefined;

  selectedAnswer = signal<number>(5);
  answers = [6, 5, 4];
  isSubmitting = signal(false);
  errorMessage = signal('');
  successMessage = signal('');

  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    remember: [false],
  });

  constructor() {
    this.destroyRef.onDestroy(() => {
      if (this.toastTimer !== undefined) {
        clearTimeout(this.toastTimer);
      }
    });
  }

  selectAnswer(value: number) {
    this.selectedAnswer.set(value);
  }

  submit() {
    if (this.form.invalid || this.isSubmitting()) {
      this.form.markAllAsTouched();
      return;
    }

    const { email, password } = this.form.getRawValue();
    this.isSubmitting.set(true);
    this.errorMessage.set('');

    this.authService
      .login({ email, password })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.isSubmitting.set(false);
          this.successMessage.set('با موفقیت وارد شدی.');

          if (this.toastTimer !== undefined) {
            clearTimeout(this.toastTimer);
          }
          this.toastTimer = setTimeout(() => this.successMessage.set(''), 3000);
        },
        error: (error: unknown) => {
          this.isSubmitting.set(false);
          this.errorMessage.set(this.getLoginErrorMessage(error));
        },
      });
  }

  private getLoginErrorMessage(error: unknown): string {
    if (error instanceof HttpErrorResponse) {
      const body = error.error as
        | { data?: { errors?: unknown }; errors?: unknown }
        | null
        | undefined;
      const errors = body?.data?.errors ?? body?.errors;
      if (Array.isArray(errors) && typeof errors[0] === 'string') {
        return errors[0];
      }
    }

    return 'ورود انجام نشد. ایمیل و رمز عبورت را بررسی کن.';
  }
}