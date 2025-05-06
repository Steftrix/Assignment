// maritime.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MaritimeService {
  private apiUrl = 'http://localhost:5288/api'; // Update port if different

  constructor(private http: HttpClient) {}

  // Ships
  getShips(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/ships`); // GET /api/ships
  }

  getShip(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/ships/${id}`); // GET /api/ships/1
  }

  createShip(ship: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/ships`, ship); // POST /api/ships
  }

  updateShip(id: number, ship: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/ships/${id}`, ship); // PUT /api/ships/1
  }

  deleteShip(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/ships/${id}`); // DELETE /api/ships/1
  }
}