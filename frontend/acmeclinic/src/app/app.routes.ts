import { Routes } from '@angular/router';

export const routes: Routes = [

  {
    path: 'home',
    redirectTo: 'home',
    pathMatch: 'full'
  },

  {
    path: 'home',
    loadComponent: () => import('./pages/home/home.component').then(
      m => m.HomeComponent
    )
  },

  {
    path: 'pacientes',
    loadComponent: () => import('./pages/pacientes/pacientes.component').then(
      m => m.PacientesComponent
    )

  },

  {
    path: 'atendimentos',
    loadComponent: () => import('./pages/atendimentos/atendimentos.component').then(
      m => m.AtendimentoComponent
    )
  }
];
