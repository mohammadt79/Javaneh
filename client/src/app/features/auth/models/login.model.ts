export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
}

export interface ApiResponse<T> {
  success: boolean;
  statusCode: number;
  data: T;
}

export interface LoginErrorResponse {
  errors: string[];
}
