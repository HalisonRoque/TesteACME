export interface Paciente {
  id: number,
  nome: string,
  dataNascimento: string,
  cpf: string,
  sexo: string,
  cep: string,
  cidade: string,
  bairro: string,
  endereco: string,
  complemento?: string,
  status: string,
}