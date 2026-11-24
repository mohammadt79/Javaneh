import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { Observable, map, tap } from 'rxjs';
import { ApiResponse, LoginRequest, LoginResponse } from '../models/login.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly accessTokenState = signal<string | null>(null);

  readonly accessToken = this.accessTokenState.asReadonly();

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http
.post<ApiResponse<LoginResponse>>(
  'http://localhost:5000/api/auth/login',
  credentials
)      .pipe(
        map((response) => response.data),
        tap(({ token }) => this.accessTokenState.set(token)),
      );
  }
}
