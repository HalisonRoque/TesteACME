import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class PacienteService extends BaseService {

  url = `${environment.apiUrl}/api/pacientes`

  getAll(query: string = '') {
    return this.http.get(
        `${this.url}/all${query}`
      )
  }

  getById(id: number) {
    return this.http
      .get(
        `${this.url}/${id}`
      )
  }

  create(body: any) {
    return this.http.post(
        `${this.url}/create`,
        body
      )
  }

  update(
    id: number,
    body: any
  ) {
    return this.http.put(
        `${this.url}/${id}/update`,
        body
      )
  }

  activate(id: number) {
    return this.http.patch(
        `${this.url}/${id}/ativar`,
        {}
      )
  }

  inactivate(id: number) {
    return this.http.patch(
        `${this.url}/${id}/inativar`,
        {}
      )
  }

}
