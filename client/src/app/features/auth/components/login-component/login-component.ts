import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login-component.html',
})
export class LoginComponent {
  private fb = inject(FormBuilder);

  selectedAnswer = signal<number>(5);
  answers = [6, 5, 4];

  form = this.fb.group({
    mobile: ['', [Validators.required, Validators.pattern(/^09\d{9}$/)]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    remember: [false],
  });

  selectAnswer(value: number) {
    this.selectedAnswer.set(value);
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    console.log(this.form.value);
  }
}