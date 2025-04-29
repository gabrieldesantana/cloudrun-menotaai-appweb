# Cloud Run - Menota AI - App Web

Aplicação web ASP.NET Core, estruturada com **Clean Architecture**, projetada para ser executada no **Google Cloud Run**.  
A infraestrutura é provisionada de forma automatizada utilizando **Terraform**.

## 📦 Estrutura do Projeto

- `src/` - Código-fonte da aplicação ASP.NET Core Web (Clean Architecture)
- `infra/` - Definições de infraestrutura como código com Terraform
- `docker-compose.yml` - Configuração para rodar localmente via Docker
- `.dockerignore` - Arquivos ignorados no build da imagem Docker

## 🚀 Como rodar localmente

1. Clone o repositório:
   ```bash
   git clone https://github.com/gabrieldesantana/cloudrun-menotaai-appweb.git
   cd cloudrun-menotaai-appweb

## 🌎 Provisionamento de Infraestrutura

Toda a infraestrutura necessária (Cloud Run, IAM, VPC, entre outros) é definida usando Terraform:

Acesse a pasta infra/.

Inicialize o Terraform:


```bash
  terraform init
```

Valide os arquivos:

```bash
  terraform validate
```
Provisione os recursos:

```bash
  terraform apply
```

Destrua os recursos:

```bash
  terraform destroy
```

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core Web
- Entity Framework Core
- Clean Architecture
- Docker
- Google Cloud Run
- Google Cloud SQL (Postgresql)
- Terraform

## 📄 TODO
- Melhorar o Readme
