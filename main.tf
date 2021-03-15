provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "signmeup_rg" {
  name     = "rg-signmeup-eastus"
  location = "East US"
}
