#conta de servico
resource "google_service_account" "app_sa" {
       account_id   = "cloudrun-service-account"
       display_name = "Conta Servico CloudRun"
}

#permissao para a conta de serviço rodar o cloudRun
resource "google_project_iam_member" "run_admin" {
  project = var.project_id
  role    = "roles/run.admin"
  member  = "serviceAccount:${google_service_account.app_sa.email}"
}

#permissao para a conta de serviço acessar o bd atraves do cloud run
resource "google_project_iam_member" "cloudrun_sql_access" {
  project = "gcp-learning-env"
  role    = "roles/cloudsql.client"
  member  = "serviceAccount:${google_service_account.app_sa.email}"
}

#permissao para a conta de serviço acessar a secret connectionString
resource "google_secret_manager_secret_iam_member" "db_connection_access" {
  secret_id = google_secret_manager_secret.db_connection_string.id
  role      = "roles/secretmanager.secretAccessor"
  member    = "serviceAccount:${google_service_account.app_sa.email}"
}

#define a secret
resource "google_secret_manager_secret" "db_connection_string" {
  secret_id = "db-connection-string"
  replication {
    auto {}
  }
}

#atribui valor a secret
resource "google_secret_manager_secret_version" "db_connection_string_version" {
  secret      = google_secret_manager_secret.db_connection_string.id
  secret_data = "Host=/cloudsql/${google_sql_database_instance.postgres.connection_name};Username=${var.db_user};Password=${var.db_password};Database=${var.db_name}"
}

#instancia do Cloud SQL
resource "google_sql_database_instance" "postgres" {
    name             = "cloudrun-postgres-instance"
    database_version = "POSTGRES_16"
    region           = var.region
    settings {
        edition = "ENTERPRISE"
        tier = "db-f1-micro"
        disk_size = 11
    }
    deletion_protection  = false
}

#cria o banco de dados dentro da instancia
resource "google_sql_database" "default" {
    name     = var.db_name
    instance = google_sql_database_instance.postgres.name
}

#define o usuario do banco
resource "google_sql_user" "default" {
    name     = var.db_user
    password = var.db_password
    instance = google_sql_database_instance.postgres.name
}

#cloudRun
resource "google_cloud_run_service" "app" {
       name     = var.app_name
       location = var.region
       template {
             metadata {
                annotations = {
                    "run.googleapis.com/cloudsql-instances" = "${var.project_id}:${var.region}:cloudrun-postgres-instance"
                    }
                    }
             spec {
                    containers {
                            image = "gcr.io/${var.project_id}/${var.app_image_name_with_tag}"
                            name = var.app_name
                            ports {
                                    container_port = 8080
                                }
                            env {
                                name  = "ConnectionStrings__DefaultConnection"
                                value_from {
                                    secret_key_ref {
                                    name = google_secret_manager_secret.db_connection_string.secret_id
                                    key  = "latest"
                                    }
                                }
                            }
                        }
                        service_account_name = google_service_account.app_sa.email
                    }
                }
                traffic {
                    percent         = 100
                    latest_revision = true
                }
}

# permite acesso publico a aplicação
resource "google_cloud_run_service_iam_member" "invoker" {
       location        = google_cloud_run_service.app.location
       service         = google_cloud_run_service.app.name
       role            = "roles/run.invoker"
       member          = "allUsers"
}