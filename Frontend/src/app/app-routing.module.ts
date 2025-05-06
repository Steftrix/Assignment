import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MaritimeDashboardComponent } from './maritime-dashboard/maritime-dashboard.component';

const routes: Routes = [
    { path: '', component: MaritimeDashboardComponent },
    { path: '**', redirectTo: '' }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }