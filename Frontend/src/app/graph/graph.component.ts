import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-graph-chart',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule, NgxChartsModule]
})
export class GraphComponent {
  years: number[] = [];
  selectedYear: number = new Date().getFullYear();
  chartData: any[] = [];

  private apiUrl = 'http://localhost:5288/api/linechart'; 
  constructor(private http: HttpClient) {
    this.initializeYears();
    this.fetchTopDepartures();
  }

  initializeYears() {
    this.http.get<number[]>(`${this.apiUrl}/years`).subscribe({
      next: (years) => {
        this.years = years; 
        if (this.years.length > 0) {
          this.selectedYear = this.years[0]; 
          this.fetchTopDepartures();
        }
      },
      error: (err) => {
        console.error('Error fetching years:', err);
      }
    });
  }

  fetchTopDepartures() {
    this.http.get<any[]>(`${this.apiUrl}?year=${this.selectedYear}`).subscribe({
      next: (data) => {
        this.chartData = data.map(item => ({
          name: item.CountryName,
          value: item.DepartureCount
        }));
      },
      error: (err) => {
        console.error('Error fetching top departures:', err);
      }
    });
  }
}