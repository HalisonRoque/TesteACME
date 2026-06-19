import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AtendimentoService extends BaseService {

  url = `${environment.apiUrl}/api/atendimentos`

  getAll(query = '') {
    return this.http
      .get(
        `${this.url}/all${query}`
      )
  }

  create(body: any) {
    return this.http
      .post(
        `${this.url}/create`,
        body
      )
  }

  update(id: number, body: any) {
    return this.http.put(
      `${this.url}/update/${id}`,
      body
    );
  }

  inactivate(id: number) {
    return this.http.put(
      `${this.url}/${id}`,
      null
    )
  }
}
