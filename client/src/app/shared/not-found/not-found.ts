import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [RouterLink],
  template: `
    <main dir="rtl" class="mx-auto max-w-xl px-6 py-24 text-center">
      <p class="text-sm font-bold text-primary">۴۰۴</p>
      <h1 class="mt-3 text-2xl font-extrabold text-ink">این صفحه هنوز آماده نیست.</h1>
      <p class="mt-3 text-sm text-slate-500">
        نشانی واردشده صفحه‌ای در دسترس ندارد.
      </p>
      <a
        routerLink="/auth/login"
        class="mt-8 inline-flex rounded-xl bg-primary px-5 py-3 text-sm font-bold text-white"
      >
        رفتن به صفحه ورود
      </a>
    </main>
  `,
})
export class NotFoundComponent {}
