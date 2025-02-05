import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';

@Injectable({providedIn: 'root'})
export class UserService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  register(userData: any) {
    return this.http.post(`${this.baseUrl}/api/user/registration`, userData);
  }
}
