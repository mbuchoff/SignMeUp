terraform {
  backend "azurerm" {
    storage_account_name = "stsignmeupeastus"
    container_name = "terraform"
    key = "sasKey"
  }
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
    }
  }
}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "signmeup_rg" {
  name     = "osrglkjrsgseir"
  location = "East US"
}
