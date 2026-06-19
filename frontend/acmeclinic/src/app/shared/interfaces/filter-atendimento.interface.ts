export interface FilterAtendimento {
  pacienteId?: number,
  pacienteNome?: string,
  status?: string,
  dataInicio?: string,
  dataFim?: string,
  page: number,
  pageSize: number,
}