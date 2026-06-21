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
import { AtendimentosFormModalComponent } from '../../shared/modals/atendimentos-form-modal/atendimentos-form-modal.component';
import { ConfirmModalComponent } from '../../shared/modals/confirm-modal/confirm-modal.component';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { NotificationSnackBar } from '../../shared/notification/notification.snackBar';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup
} from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  selector: 'app-atendimento',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './atendimentos.component.html',
  styleUrl: './atendimentos.component.css'
})
export class AtendimentoComponent implements OnInit {
  displayedColumns = [
    'paciente',
    'data',
    'hora',
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
  range!: FormGroup;

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
    private snackBar: NotificationSnackBar,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.today = new Date().toISOString().split('T')[0];

    this.range = this.fb.group({
      start: [null],
      end: [null]
    });

    this.buscarPacientes();
    this.buscar();
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
    const { start, end } = this.range.value;

    this.filter.dataInicio = start?.toISOString().split('T')[0] ?? '';
    this.filter.dataFim = end?.toISOString().split('T')[0] ?? '';

    if ((start && !end) || (!start && end)) {
      this.snackBar.warning(
        'Selecione início e fim'
      );
      return;
    }

    this.filter.page = 1;
    this.buscar();
  }

  trocarPagina(event: PageEvent) {
    this.filter.page = event.pageIndex + 1;
    this.buscar();
  }

  abrirModal(atendimento?: any) {
    const dialogRef = this.dialog.open(AtendimentosFormModalComponent, {
      width: '800px',
      data: atendimento ?? null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;

      if (atendimento?.id) {
        this.atendimentoService.update(atendimento.id, result)
          .subscribe(() => this.buscar());
        return;
      }

      this.atendimentoService.create(result)
        .subscribe(() => this.buscar());
    });
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
    this.range.reset();
    this.pacienteBusca = '';
    this.pacientesFiltrados = [...this.pacientes];
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

  inativar(id: number) {
    const dialogRef = this.dialog.open(
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
    const dialogRef = this.dialog.open(
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
