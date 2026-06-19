import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { FormsModule } from '@angular/forms';
import { PacienteService } from '../../core/services/paciente.service';
import { Paciente } from '../../shared/models/paciente.model';
import { FilterPaciente } from '../../shared/interfaces/filter-paciente.interface';
import { MatDialog } from '@angular/material/dialog';
import { PacienteFormModalComponent } from '../../shared/modals/paciente-form-modal/paciente-form-modal.component';
import { ConfirmModalComponent } from '../../shared/modals/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-pacientes',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
  ],
  templateUrl: './pacientes.component.html',
  styleUrl: './pacientes.component.css'
})
export class PacientesComponent implements OnInit {
  displayedColumns = [
    'nome',
    'cpf',
    'sexo',
    'status',
    'acoes'
  ];

  pacientes: Paciente[] = [];
  total = 0;
  filter: FilterPaciente = {
    nome: '',
    cpf: '',
    status: '',
    page: 1,
    pageSize: 10
  };

  constructor(
    private pacienteService: PacienteService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.buscar();
  }

  buscar() {
    const query =
      `?page=${this.filter.page}
      &pageSize=${this.filter.pageSize}
      &nome=${this.filter.nome ?? ''}
      &cpf=${this.filter.cpf ?? ''}
      &status=${this.filter.status ?? ''}`;

    this.pacienteService
      .getAll(query.replace(/\s/g, ''))
      .subscribe((response: any) => {

        this.pacientes = response.data;
        this.total = response.total;
      });
  }

  aplicarFiltro() {
    this.filter.page = 1;
    this.buscar();
  }

  trocarPagina(event: PageEvent) {
    this.filter.page = event.pageIndex + 1;

    this.filter.pageSize = event.pageSize;

    this.buscar();
  }

  abrirModal(paciente?: Paciente) {
    const dialogRef = this.dialog.open(
      PacienteFormModalComponent,
      {
        width: '760px',
        maxWidth: '95vw',
        data: paciente ?? null
      }
    );

    dialogRef
      .afterClosed()
      .subscribe(
        (saved) => {
          if (saved) {
            this.buscar();
          }
        }
      );
  }

  temFiltroAtivo() {
    return !!(
      this.filter.nome ||
      this.filter.cpf ||
      this.filter.status
    );
  }

  limparFiltro() {
    this.filter = {
      nome: '',
      cpf: '',
      status: '',
      page: 1,
      pageSize: 10
    };

    this.buscar();
  }

  inativar(id: number) {
    const dialogRef = this.dialog.open(
      ConfirmModalComponent,
      {
        width: '400px',
        data: {
          titulo: 'Inativar Paciente',
          mensagem: 'Deseja realmente inativar este paciente?'
        }
      }
    );

    dialogRef
      .afterClosed()
      .subscribe(
        (confirm) => {
          if (!confirm) {
            return;
          }

          this.pacienteService
            .inactivate(id)
            .subscribe(() => {
              this.buscar();
            }
            );
        }
      );
  }

  ativar(id: number) {
    const dialogRef = this.dialog.open(
      ConfirmModalComponent,
      {
        width: '400px',
        data: {
          titulo: 'Ativar Paciente',
          mensagem: 'Deseja realmente Ativar este paciente?'
        }
      }
    );

    dialogRef
      .afterClosed()
      .subscribe(
        (confirm) => {
          if (!confirm) {
            return;
          }

          this.pacienteService
            .activate(id)
            .subscribe(() => {
              this.buscar();
            }
            );
        }
      );
  }

}
