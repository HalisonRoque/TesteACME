import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { PacienteService } from '../../../core/services/paciente.service';

@Component({
  selector: 'app-atendimentos-form-modal',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatFormFieldModule,
    MatAutocompleteModule
  ],
  templateUrl: './atendimentos-form-modal.component.html',
  styleUrl: './atendimentos-form-modal.component.css'
})
export class AtendimentosFormModalComponent implements OnInit {
  form!: FormGroup;
  pacientes: any[] = [];
  pacientesFiltrados: any[] = [];

  constructor(
    private fb: FormBuilder,
    private pacienteService: PacienteService,
    private dialog: MatDialogRef<AtendimentosFormModalComponent>,
    @Inject(MAT_DIALOG_DATA)
    public response: any
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      pacienteId: [null],
      pacienteNome: [''],
      data: ['', Validators.required],
      hora: ['', Validators.required],
      descricao: ['', Validators.required],
      status: ['Ativo']
    });

    this.carregarPacientes();

    if (this.response) {
      this.form.patchValue({
        ...this.response,
        pacienteNome: this.response.pacienteNome
      });
    }
  }

  carregarPacientes() {
    this.pacienteService
      .getAll('?status=Ativo&page=1&pageSize=999')
      .subscribe((res: any) => {
        this.pacientes = res.data;
        this.pacientesFiltrados = [];
      });
  }

  abrirAutocomplete() {
    this.pacientesFiltrados = [...this.pacientes];
  }

  filtrarPacientes() {
    const texto = this.form
      .get('pacienteNome')
      ?.value
      ?.toLowerCase()
      ?.trim() ?? '';

    if (!texto) {
      this.pacientesFiltrados = [...this.pacientes];
      return;
    }

    this.pacientesFiltrados = this.pacientes.filter(
      p => p.nome
        .toLowerCase()
        .includes(texto)
    );

  }

  selecionarPaciente(paciente: any) {
    this.form.patchValue({
      pacienteId: paciente.id,
      pacienteNome: paciente.nome
    });
  }

  salvar() {
    if (this.form.invalid) return;
    this.dialog.close(this.form.value);
  }

  fechar() {
    this.dialog.close();
  }
}
