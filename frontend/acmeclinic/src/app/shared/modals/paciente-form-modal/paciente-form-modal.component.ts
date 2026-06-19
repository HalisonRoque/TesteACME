import {
  Component,
  Inject,
  OnInit
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  FormBuilder,
  Validators
} from '@angular/forms';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { PacienteService } from '../../../core/services/paciente.service';

@Component({
  selector: 'app-paciente-form-modal',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatFormFieldModule
  ],
  templateUrl: './paciente-form-modal.component.html',
  styleUrl: './paciente-form-modal.component.css'
})
export class PacienteFormModalComponent implements OnInit {

  form!: ReturnType<FormBuilder['group']>;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private pacienteService: PacienteService,
    private dialog: MatDialogRef<PacienteFormModalComponent>,

    @Inject(MAT_DIALOG_DATA)
    public data: any
  ) {
    this.form = this.fb.group({
      nome: ['', Validators.required],
      dataNascimento: ['', Validators.required],
      cpf: ['', Validators.required],
      sexo: ['', Validators.required],
      cep: ['', Validators.required],
      cidade: ['', Validators.required],
      bairro: ['', Validators.required],
      endereco: ['', Validators.required],
      complemento: [''],
      status: ['ATIVO', Validators.required]
    });

  }

  ngOnInit() {
    if (this.data) {
      this.form.patchValue(
        this.data
      );
    }
  }

  salvar() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    const req = this.data?.id
      ? this.pacienteService
        .update(
          this.data.id,
          this.form.value
        )
      : this.pacienteService
        .create(
          this.form.value
        );

    req.subscribe({
      next: () => {
        this.dialog.close(
          true
        );
      },

      complete: () => {
        this.loading = false;
      }
    });
  }

  fechar() {
    this.dialog.close();
  }
}
