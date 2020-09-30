# Integration Factory
#### Biblioteca para facilitar os processos de ETL

## Progresso

Origem | Destino | Testes 
------ | ------- | ----- 
SqlSever* | SqlServer | Não 
Texto plano (CSV e TXT) | SqlServer | Sim 
XML* | SqlServer | Não 
JSON | SqlServer | Não 
REST(Json) | SqlServer | Não 
SOAP(xml) | SqlServer | Não 

# Todo
Implementando dados que integram com SqlServer
### 1º Candidato
* Criar teste para origem em XML
* Criar teste para origem em SqlServer

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
* O Código tem que ser configurável através de um arquivo json
* Adicionar observadores para o cliente acompanhar o trabalho
* Documentar a implementação da pipeline com exemplos
* Eliminar o Dapper das consultas
* Transformar a aplicação em uma CLI


## Nuget
..ainda não publicado

## Como usar
..em construção

# Exemplo WorkService
..ainda não implementado

# Exemplo WebApi
..ainda não implementado
