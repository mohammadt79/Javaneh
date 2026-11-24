import { provideHttpClient } from '@angular/common/http';
import {
  HttpTestingController,
  provideHttpClientTesting,
} from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { AuthService } from '../../services/auth.service';

import { LoginComponent } from './login-component';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let httpTestingController: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginComponent],
      providers: [provideHttpClient(), provideHttpClientTesting(), provideRouter([])],
    }).compileComponents();

    httpTestingController = TestBed.inject(HttpTestingController);
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  afterEach(() => {
    httpTestingController.verify();
    fixture.destroy();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('sends email credentials and shows a toast after a successful login', () => {
    component.form.setValue({
      email: 'student@example.com',
      password: 'Password123',
      remember: false,
    });

    component.submit();

    const request = httpTestingController.expectOne('/api/auth/login');
    expect(request.request.method).toBe('POST');
    expect(request.request.body).toEqual({
      email: 'student@example.com',
      password: 'Password123',
    });

    request.flush({
      success: true,
      statusCode: 200,
      data: { token: 'test-token' },
    });
    fixture.detectChanges();

    expect(TestBed.inject(AuthService).accessToken()).toBe('test-token');
    expect(component.successMessage()).toBe('با موفقیت وارد شدی.');
    expect(fixture.nativeElement.querySelector('[role="status"]').textContent).toContain(
      'با موفقیت وارد شدی.',
    );
  });

  it('shows the backend error when login fails', () => {
    component.form.setValue({
      email: 'student@example.com',
      password: 'wrong-password',
      remember: false,
    });

    component.submit();
    httpTestingController
      .expectOne('/api/auth/login')
      .flush(
        {
          success: true,
          statusCode: 401,
          data: { errors: ['Invalid email or password.'] },
        },
        { status: 401, statusText: 'Unauthorized' },
      );

    expect(component.errorMessage()).toBe('Invalid email or password.');
    expect(component.successMessage()).toBe('');
  });
});
