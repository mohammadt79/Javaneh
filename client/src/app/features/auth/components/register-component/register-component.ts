import { HttpErrorResponse } from '@angular/common/http';
import { Component, DestroyRef, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register-component.html',
})
export class RegisterComponent {
  private readonly fb = inject(FormBuilder).nonNullable;
  private readonly authService = inject(AuthService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly router = inject(Router);

  isSubmitting = signal(false);
  errorMessage = signal('');
  successMessage = signal('');
  selectedAnswer = signal<number>(5);
  answers = [6, 5, 4];

  form = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phoneNumber: ['', Validators.required],
    dateOfBirth: ['', Validators.required],
    password: [
      '',
      [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$/),
      ],
    ],
    confirmPassword: ['', Validators.required],
    terms: [false, Validators.requiredTrue],
  });

  selectAnswer(value: number): void {
    this.selectedAnswer.set(value);
  }

  submit(): void {
    if (this.form.invalid || this.isSubmitting()) {
      this.form.markAllAsTouched();
      return;
    }

    const {
      firstName,
      lastName,
      email,
      phoneNumber,
      dateOfBirth,
      password,
      confirmPassword,
    } = this.form.getRawValue();

    if (password !== confirmPassword) {
      this.errorMessage.set('رمز عبور و تکرار آن یکسان نیست.');
      return;
    }

    this.isSubmitting.set(true);
    this.errorMessage.set('');
    this.successMessage.set('');

    this.authService
      .register({
        firstName,
        lastName,
        email,
        phoneNumber,
        dateOfBirth: new Date(`${dateOfBirth}T00:00:00.000Z`).toISOString(),
        password,
        confirmPassword,
      })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.isSubmitting.set(false);
          this.successMessage.set('ثبت‌نام با موفقیت انجام شد.');
          void this.router.navigateByUrl('/auth/login');
        },
        error: (error: unknown) => {
          this.isSubmitting.set(false);
          this.errorMessage.set(this.getRegisterErrorMessage(error));
        },
      });
  }

  private getRegisterErrorMessage(error: unknown): string {
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

    return 'ثبت‌نام انجام نشد. اطلاعات واردشده را بررسی کن.';
  }
}
