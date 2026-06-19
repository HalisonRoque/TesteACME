import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AtendimentoService } from '../../core/services/atendimento.service';
import { PacienteService } from '../../core/services/paciente.service';
import { Atendimento } from '../../shared/models/atendimento.model';
import { FilterAtendimento } from '../../shared/interfaces/filter-atendimento.interface';
import { MatTableModule } from '@angular/material/table';
import {
  MatPaginatorModule,
  PageEvent
} from '@angular/material/paginator';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatDialog } from '@angular/material/dialog';
import { AtendimentoFormModalComponent } from '../../shared/modals/atendimento-form-modal/atendimento-form-modal.component';
import { ConfirmModalComponent } from '../../shared/modals/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-atendimento',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,

    MatTableModule,
    MatPaginatorModule,

    MatCardModule,

    MatFormFieldModule,
    MatInputModule,

    MatSelectModule,

    MatButtonModule,

    MatIconModule,

    AtendimentoFormModalComponent,
    ConfirmModalComponent
  ],
  templateUrl: './atendimentos.component.html',
  styleUrl: './atendimentos.component.css'
})
export class AtendimentoComponent implements OnInit {

  displayedColumns = [
    'paciente',
    'dataHora',
    'descricao',
    'status',
    'acoes'
  ];

  atendimentos: Atendimento[] = [];

  pacientes: any[] = [];

  total = 0;

  filter: FilterAtendimento = {
    pacienteId: undefined,
    status: '',
    dataInicio: '',
    dataFim: '',
    page: 1,
    pageSize: 10
  };

  constructor(
    private atendimentoService:
      AtendimentoService,

    private pacienteService:
      PacienteService,

    private dialog:
      MatDialog
  ) { }

  ngOnInit() {
    this.buscarPacientes();
    this.buscar();
  }

  buscarPacientes() {
    this.pacienteService
      .getAll('?status=ATIVO&page=1&pageSize=999')
      .subscribe((res: any) => {

        this.pacientes = res.data;
      });
  }

  buscar() {
    const query =
      `?page=${this.filter.page}
      &pageSize=${this.filter.pageSize}
      &pacienteId=${this.filter.pacienteId ?? ''}
      &status=${this.filter.status ?? ''}
      &dataInicio=${this.filter.dataInicio ?? ''}
      &dataFim=${this.filter.dataFim ?? ''}`;

    this.atendimentoService
      .getAll(query.replace(/\s/g, ''))
      .subscribe((res: any) => {

        this.atendimentos = res.data;

        this.total = res.total;
      });
  }

  aplicarFiltro() {
    this.filter.page = 1;
    this.buscar();
  }

  trocarPagina(event: PageEvent) {
    this.filter.page = event.pageIndex + 1;
    this.buscar();
  }

  abrirModal(atendimento?: Atendimento) {
    const dialogRef = this.dialog.open(
      AtendimentoFormModalComponent,
      {
        width: '800px',
        data: atendimento ?? null
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

  inativar(id: number) {
    const dialogRef = this.dialog.open(
      ConfirmModalComponent,
      {
        width: '400px',
        data: {
          titulo: 'Inativar Atendimento',
          mensagem: 'Deseja continuar?'
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

          this.atendimentoService
            .inactivate(id)
            .subscribe(() => {
              this.buscar();
            }
            );
        }
      );
  }

}
