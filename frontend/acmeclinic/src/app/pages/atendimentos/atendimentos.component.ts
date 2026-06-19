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
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { NotificationSnackBar } from '../../shared/notification/notification.snackBar';

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
    MatAutocompleteModule
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
  today!: string;
  atendimentos: Atendimento[] = [];
  pacientes: any[] = [];
  pacienteBusca: any = '';
  pacientesFiltrados: any[] = [];
  total = 0;
  dataInvalida = false;

  filter: FilterAtendimento = {
    pacienteId: undefined,
    status: '',
    dataInicio: '',
    dataFim: '',
    page: 1,
    pageSize: 10
  };

  constructor(
    private atendimentoService: AtendimentoService,
    private pacienteService: PacienteService,
    private dialog: MatDialog,
    private snackBar: NotificationSnackBar
  ) { }

  ngOnInit() {
    this.today = new Date().toISOString().split('T')[0];
    this.buscarPacientes();
    this.buscar();
  }

  private toDate(value: string): Date {
    return new Date(value + 'T00:00:00');
  }

  buscarPacientes() {
    this.pacienteService
      .getAll('?status=Ativo&page=1&pageSize=999')
      .subscribe((res: any) => {

        this.pacientes = res.data;

        this.pacientesFiltrados = res.data;
      });
  }

  buscar() {
    const query =
      `?page=${this.filter.page}
      &pageSize=${this.filter.pageSize}
      &pacienteId=${this.filter.pacienteId ?? ''}
      &pacienteNome=${this.filter.pacienteNome ?? ''}
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
    this.validarDatas();

    if (this.dataInvalida) {
      return;
    }

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

  temFiltroAtivo() {
    return !!(
      this.filter.pacienteId ||
      this.filter.pacienteNome ||
      this.filter.status ||
      this.filter.dataInicio ||
      this.filter.dataFim
    );
  }

  limparFiltro() {
    this.pacienteBusca = '';
    this.pacientesFiltrados = this.pacientes;
    this.filter = {
      pacienteId: undefined,
      pacienteNome: undefined,
      status: '',
      dataInicio: '',
      dataFim: '',
      page: 1,
      pageSize: 10
    };

    this.buscar();
  }

  filtrarPacientes() {
    const texto = (this.pacienteBusca || '')
      .toString()
      .trim()
      .toLowerCase();

    this.filter.pacienteNome = texto || undefined;

    this.pacientesFiltrados = this.pacientes.filter(
      p => p.nome
        ?.toLowerCase()
        .includes(texto)
    );

    if (!texto) {
      this.pacientesFiltrados = [...this.pacientes];
      this.filter.pacienteId = undefined;
      this.filter.pacienteNome = undefined;
    }
  }

  selecionarPaciente(paciente: any) {
    if (!paciente) {
      this.pacienteBusca = '';
      this.filter.pacienteId = undefined;
      this.filter.pacienteNome = undefined;
      return;
    }

    this.filter.pacienteId = paciente.id;
    this.filter.pacienteNome = paciente.nome;
    this.pacienteBusca = paciente.nome;
  }

  // validarDatas() {
  //   this.dataInvalida = false;

  //   const inicio = this.filter.dataInicio;
  //   const fim = this.filter.dataFim;

  //   const hoje = new Date();
  //   hoje.setHours(0, 0, 0, 0);

  //   if ((!!inicio && !fim) || (!inicio && !!fim)) {
  //     this.dataInvalida = true;
  //     return;
  //   }

  //   if (inicio && fim) {
  //     const dInicio = this.toDate(inicio);
  //     const dFim = this.toDate(fim);

  //     if (dInicio > hoje || dFim > hoje) {
  //       this.dataInvalida = true;
  //       return;
  //     }

  //     if (dFim < dInicio) {
  //       this.dataInvalida = true;
  //       return;
  //     }
  //   }
  // }

  validarDatas() {
    console.log('==============================');
    console.log('VALIDANDO DATAS');
    console.log('Data início:', this.filter.dataInicio);
    console.log('Data fim:', this.filter.dataFim);

    this.dataInvalida = false;

    const inicio = this.filter.dataInicio;
    const fim = this.filter.dataFim;

    const hoje = new Date();
    hoje.setHours(0, 0, 0, 0);

    console.log('HOJE:', hoje);

    if ((inicio && !fim) || (!inicio && fim)) {
      this.dataInvalida = true;

      this.snackBar.warning('Preencha início e fim juntos');
      return;
    }

    if (inicio && fim) {
      const dInicio = new Date(inicio + 'T00:00:00');
      const dFim = new Date(fim + 'T00:00:00');

      console.log('Data início convertida:', dInicio);
      console.log('Data fim convertida:', dFim);

      if (dInicio > hoje || dFim > hoje) {
        console.warn('❌ ERRO: Data maior que hoje');
        this.dataInvalida = true;
        return;
      }

      if (dFim < dInicio) {
        console.warn('❌ ERRO: Data fim menor que início');
        this.dataInvalida = true;
        return;
      }
    }

    console.log('✔ Datas válidas');
  }

  inativar(id: number) {
    const dialogRef =
      this.dialog.open(
        ConfirmModalComponent,
        {
          width: '420px',
          data: {
            titulo: 'Inativar Atendimento',
            mensagem: 'Deseja realmente inativar este atendimento?'
          }
        }
      );

    dialogRef
      .afterClosed()
      .subscribe(confirm => {
        if (!confirm) {
          return;
        }

        this.atendimentoService
          .inactivate(id)
          .subscribe(() => {
            this.buscar();
          });
      });
  }

  ativar(id: number) {
    const dialogRef =
      this.dialog.open(
        ConfirmModalComponent,
        {
          width: '420px',
          data: {
            titulo: 'Ativar Atendimento',
            mensagem: 'Deseja realmente ativar este atendimento?'
          }
        }
      );

    dialogRef
      .afterClosed()
      .subscribe(confirm => {
        if (!confirm) {
          return;
        }

        this.atendimentoService
          .activate(id)
          .subscribe(() => {
            this.buscar();
          });
      });
  }
}
