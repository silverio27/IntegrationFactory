# Integration Factory
#### Biblioteca para facilitar os processos de ETL

## Progresso

Origem | Destino | Testes | Estável
------ | ------- | ----- | -------
SqlSever* | SqlServer | 0% | Não
CSV* | SqlServer | 0% | Não
TXT* | SqlServer | 0% | Não
XML* | SqlServer | 0% | Não
JSON | SqlServer | 0% | Não
REST(Json) | SqlServer | 0% | Não
SOAP(xml) | SqlServer | 0% | Não

# Todo
Implementando dados que integram com SqlServer
### 1º Candidato
* Possibilitar o processamento de valores entre a operação de extração e carga (implementar o Transform)
> 
>   * Exemplos:
>   1. Remover uma coluna de um arquivo de texto plano

### 2º Candidato
* Implementar a origem por dados em memória (MemoryStream)
* Implementar a origem por dados em JSON (Arquivo)
como texto e em memória

### 3º Candidato
* Implementar a origem por dados de uma API Rest
* Implementar a origem por dados de uma API
Soap
> 
> Necessário explicitar a escolha do tipo de retorno

### 4º Candidato
* Eliminar o Dapper das consultas
* O Código tem que ser configurável através de um arquivo json
* Adicionar observadores para o cliente acompanhar o trabalho
* Documentar a implementação da pipeline com exemplos
* Transformar a aplicação em uma CLI


## Nuget
..ainda não publicado

## Como usar
..em construção

# Exemplo WorkService
..ainda não implementado

# Exemplo WebApi
..ainda não implementado
