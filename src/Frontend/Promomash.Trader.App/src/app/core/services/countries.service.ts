import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';

@Injectable({providedIn: 'root'})
export class CountriesService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getCountries() {
    return this.http.get<any[]>(`${this.baseUrl}/api/cuntries`);
  }

  getProvinces(countryCode: string) {
    return this.http.get<any[]>(`${this.baseUrl}/api/cuntries/${countryCode}/provinces`);
  }
}
