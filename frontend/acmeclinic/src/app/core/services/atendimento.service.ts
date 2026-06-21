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
    console.log("body", body);
    return this.http
      .post(
        `${this.url}/create`,
        body
      )
  }

  update(id: number, body: any) {
    console.log("body", body);
    return this.http.put(
      `${this.url}/${id}/update`,
      body
    );
  }

  inactivate(id: number) {
    const test = this.http.patch(
      `${this.url}/${id}/inativar`,
      {}
    )
    return test
  }


  activate(id: number) {
    const test = this.http.patch(
      `${this.url}/${id}/ativar`,
      {}
    )
    return test
  }
}
