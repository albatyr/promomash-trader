import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';
import { Observable } from 'rxjs';

export interface CountryDto {
  code: string,
  name: string
}

export interface ProvinceDto {
  id: string,
  name: string
}

@Injectable({providedIn: 'root'})
export class CountriesService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getCountries(): Observable<CountryDto[]> {
    return this.http.get<CountryDto[]>(`${this.baseUrl}/api/cuntries`);
  }

  getProvinces(countryCode: string): Observable<ProvinceDto[]> {
    return this.http.get<ProvinceDto[]>(`${this.baseUrl}/api/cuntries/${countryCode}/provinces`);
  }
}
