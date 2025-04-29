# Configure GCP project
provider "google" {   
    project = var.project_id   
    region  = var.region 
}