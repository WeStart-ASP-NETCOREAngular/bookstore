export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  Email: string;
  Password: string;
  FirstName: string;
  LastName: string;
}

export interface AuthResponse {
  token: string;
  expiration: Date;
}
