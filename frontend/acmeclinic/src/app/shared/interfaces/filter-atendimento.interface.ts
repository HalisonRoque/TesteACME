export interface FilterAtendimento {
  pacienteId?: number,
  status?: string,
  dataInicio?: string,
  dataFim?: string,
  page: number,
  pageSize: number,
}