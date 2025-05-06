import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { Color, ScaleType } from '@swimlane/ngx-charts';

@Component({
  selector: 'app-maritime-dashboard',
  standalone: true,
  imports: [CommonModule, NgxChartsModule],
  templateUrl: './maritime-dashboard.component.html',
  styleUrls: ['./maritime-dashboard.component.scss']
})
export class MaritimeDashboardComponent implements OnInit {
  // Chart Data
  shipData: any[] = [
    { name: 'Ocean Queen', value: 28 },
    { name: 'Blue Wave', value: 32 }
  ];

  voyageData: any[] = [
    { name: 'Jan', value: 5 },
    { name: 'Feb', value: 8 },
    { name: 'Mar', value: 12 }
  ];

  countryVisitData: any[] = [
    { name: 'Germany', value: 15 },
    { name: 'Netherlands', value: 8 },
    { name: 'Belgium', value: 5 }
  ];

  portData: any[] = [
    { name: 'Hamburg', country: 'Germany' },
    { name: 'Rotterdam', country: 'Netherlands' },
    { name: 'Antwerp', country: 'Belgium' }
  ];

  // Correct Color Scheme Configuration
  colorScheme: Color = {
    name: 'custom',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#1a73e8', '#63a4ff', '#a7c7ff']
  };

  constructor() {}

  ngOnInit(): void {
    // Data loading methods can be added here when backend is ready
  }
}