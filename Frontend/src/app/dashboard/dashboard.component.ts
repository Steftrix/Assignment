// dashboard.component.ts
import { Component } from '@angular/core';
import { DataService } from '../services/data.service'; 
import { CommonModule } from '@angular/common'; // Import CommonModule
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule if needed
import { HttpClient } from '@angular/common/http'; // 
import { GraphComponent } from '../graph/graph.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  standalone: true, // If this is a standalone component
  imports: [CommonModule, GraphComponent] // Add CommonModule here
})
export class DashboardComponent {
  data: any[] = [];
  columns: string[] = [];

  constructor(private dataService: DataService) {}

  clearData() {
    this.data = [];
    this.columns = [];
  }

  fetchData(entity: string) {
    this.data = []; // Clear the data and columns
    this.columns = [];
  
    // Construct the method name dynamically
    const methodName = `get${this.capitalizeFirstLetter(entity)}` as keyof DataService;
  
    // Check if the method exists in the DataService
    if (typeof this.dataService[methodName] !== 'function') {
      console.error(`Error: Method ${methodName} does not exist on DataService.`);
      return;
    }
  
    this.dataService[methodName]().subscribe({
      next: (res: any[]) => {
        this.data = res;
        if (res.length > 0) {
          this.columns = Object.keys(res[0]);
        }
      },
      error: (err: any) => {
        console.error('Error fetching data:', err);
        this.data = [];
        this.columns = [];
      }
    });
  }
  private capitalizeFirstLetter(string: string): string {
    return string.charAt(0).toUpperCase() + string.slice(1);
  }
}

