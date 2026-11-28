import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { Observable, map, tap } from 'rxjs';
import {
  ApiResponse,
  LoginRequest,
  LoginResponse,
} from '../models/login.model';
import { RegisterRequest } from '../models/register.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly accessTokenState = signal<string | null>(null);

  readonly accessToken = this.accessTokenState.asReadonly();

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http
      .post<ApiResponse<LoginResponse>>('/api/auth/login', credentials)
      .pipe(
        map((response) => response.data),
        tap(({ token }) => this.accessTokenState.set(token)),
      );
  }

  register(request: RegisterRequest): Observable<string> {
    return this.http
      .post<ApiResponse<string>>('/api/auth/register', request)
      .pipe(map((response) => response.data));
  }
}
