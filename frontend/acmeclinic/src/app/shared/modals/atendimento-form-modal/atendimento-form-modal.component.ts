import {
  Component,
  Inject,
  OnInit
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormsModule,
  ReactiveFormsModule,
  FormBuilder,
  Validators
} from '@angular/forms';
import {
  MAT_DIALOG_DATA,
  MatDialogRef,
  MatDialogModule
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AtendimentoService } from '../../../core/services/atendimento.service';
import { PacienteService } from '../../../core/services/paciente.service';
import { MatAutocompleteModule } from '@angular/material/autocomplete';

@Component({
  selector: 'app-atendimento-form-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatFormFieldModule,
    MatAutocompleteModule
  ],
  templateUrl: './atendimento-form-modal.component.html',
  styleUrl: './atendimento-form-modal.component.css'
})

export class AtendimentoFormModalComponent implements OnInit {

  pacientes: any[] = [];
  pacienteBusca = '';
  pacientesFiltrados: any[] = [];
  loading = false;
  form!: ReturnType<FormBuilder['group']>;

  constructor(
    private fb: FormBuilder,
    private atendimentoService: AtendimentoService,
    private pacienteService: PacienteService,
    private dialogRef: MatDialogRef<AtendimentoFormModalComponent>,

    @Inject(MAT_DIALOG_DATA)
    public data: any
  ) {
    this.form = this.fb.group({
      pacienteId: [null, Validators.required],
      dataHora: ['', Validators.required],
      descricao: ['', Validators.required],
      status: ['Ativo', Validators.required]
    });
  }

  ngOnInit() {
    this.buscarPacientes();

    setTimeout(() => {
      this.pacientesFiltrados = [...this.pacientes];
    });

    if (this.data) {
      this.form.patchValue(this.data);
      this.pacienteBusca = this.data.pacienteNome;
    }
  }

  buscarPacientes() {
    this.pacienteService
      .getAll(
        '?status=Ativo&page=1&pageSize=999'
      )
      .subscribe(
        (res: any) => {
          this.pacientes = res.data;
          this.pacientesFiltrados = res.data;
        }
      );
  }

  filtrarPacientes() {
    const texto = this.pacienteBusca
      .toLowerCase();

    this.pacientesFiltrados = this.pacientes.filter(
      p => p.nome
        .toLowerCase()
        .includes(texto)
    );
  }

  selecionarPaciente(paciente: any) {
    this.form
      .get('pacienteId')
      ?.setValue(
        paciente.id
      );

    this.pacienteBusca = paciente.nome;
  }

  salvar() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const data = new Date(this.form.value.dataHora!);

    if (data > new Date()) {
      alert(
        'Não é permitido data futura'
      );
      return;
    }

    this.loading = true;

    const req = this.data?.id
      ? this.atendimentoService
        .update(
          this.data.id,
          this.form.value
        )

      : this.atendimentoService
        .create(
          this.form.value
        );

    req.subscribe({
      next: () => {
        this.dialogRef.close(
          true
        );
      },

      complete: () => {
        this.loading = false;
      }
    });
  }

  fechar() {
    this.dialogRef.close();
  }
}
