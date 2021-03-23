provider "azurerm" {
  features {}
}

data "azurerm_client_config" "current" {}

resource "azurerm_resource_group" "terraform" {
  name     = "terraform"
  location = "East US"
}

resource "random_integer" "kv_terraform_eastus" {
  min = 10000
  max = 99999
}

resource "azurerm_key_vault" "terraform" {
  name                        = "kv-terraform-${random_integer.kv_terraform_eastus.result}"
  location                    = azurerm_resource_group.terraform.location
  resource_group_name         = azurerm_resource_group.terraform.name
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  sku_name = "standard"

  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = data.azurerm_client_config.current.object_id

    key_permissions = [
      "Get",
    ]

    secret_permissions = [
      "Get"
    ]

    storage_permissions = [
      "Get",
    ]
  }
}
