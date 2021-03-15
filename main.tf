provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "signmeup_rg" {
  name     = "osrglkjrsgseir"
  location = "East US"
}
