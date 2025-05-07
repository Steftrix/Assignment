import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class DataService {
  private baseUrl = 'http://localhost:5288/api/Voyage';

  constructor(private http: HttpClient) {}

  getShips(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/ships`);
  }

  getPorts(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/ports`);
  }

  getCountries(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/countries`);
  }

  getVoyages(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/voyages`);
  }

  getLastyearcountries(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/LastYearCountries`);
  }
}
