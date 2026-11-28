import { provideHttpClient } from '@angular/common/http';
import {
  HttpTestingController,
  provideHttpClientTesting,
} from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router, provideRouter } from '@angular/router';
import { RegisterComponent } from './register-component';

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;
  let httpTestingController: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterComponent],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        provideRouter([]),
      ],
    }).compileComponents();

    httpTestingController = TestBed.inject(HttpTestingController);
    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  afterEach(() => {
    httpTestingController.verify();
    fixture.destroy();
  });

  it('creates an account using the API registration contract', () => {
    const router = TestBed.inject(Router);
    const navigateSpy = vi.spyOn(router, 'navigateByUrl').mockResolvedValue(true);
    component.form.setValue({
      firstName: 'Sara',
      lastName: 'Ahmadi',
      email: 'sara@example.com',
      phoneNumber: '09120000000',
      dateOfBirth: '2000-02-29',
      password: 'Password123',
      confirmPassword: 'Password123',
      terms: true,
    });

    component.submit();

    const request = httpTestingController.expectOne('/api/auth/register');
    expect(request.request.method).toBe('POST');
    expect(request.request.body).toEqual({
      firstName: 'Sara',
      lastName: 'Ahmadi',
      email: 'sara@example.com',
      phoneNumber: '09120000000',
      dateOfBirth: '2000-02-29T00:00:00.000Z',
      password: 'Password123',
      confirmPassword: 'Password123',
    });

    request.flush({
      code: 200,
      message: 'Success',
      data: 'User registered successfully',
    });

    expect(component.isSubmitting()).toBe(false);
    expect(component.successMessage()).toBe('ثبت‌نام با موفقیت انجام شد.');
    expect(navigateSpy).toHaveBeenCalledWith('/auth/login');
  });

  it('does not send the request when the passwords do not match', () => {
    component.form.setValue({
      firstName: 'Sara',
      lastName: 'Ahmadi',
      email: 'sara@example.com',
      phoneNumber: '09120000000',
      dateOfBirth: '2000-02-29',
      password: 'Password123',
      confirmPassword: 'Different123',
      terms: true,
    });

    component.submit();

    httpTestingController.expectNone('/api/auth/register');
    expect(component.errorMessage()).toBe('رمز عبور و تکرار آن یکسان نیست.');
  });

  it('requires agreeing to the terms before submitting', () => {
    component.form.setValue({
      firstName: 'Sara',
      lastName: 'Ahmadi',
      email: 'sara@example.com',
      phoneNumber: '09120000000',
      dateOfBirth: '2000-02-29',
      password: 'Password123',
      confirmPassword: 'Password123',
      terms: false,
    });

    component.submit();

    httpTestingController.expectNone('/api/auth/register');
    expect(component.form.controls.terms.touched).toBe(true);
  });
});
