import { inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export abstract class BaseService {
  protected http = inject(HttpClient)
}