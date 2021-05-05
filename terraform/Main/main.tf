terraform {
  backend "azurerm" {
    storage_account_name = "stsignmeupeastus"
    container_name       = "terraform"
    key                  = "terraform.tfstate"

    # rather than defining this inline, the SAS Token can also be sourced
    # from an Environment Variable - more information is available below.
    #sas_token =
  }
}

data "azurerm_subscription" "current" {
}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "dev" {
  name     = "signmeup_dev"
  location = "East US"
}

data "azurerm_client_config" "current" {}

resource "azurerm_app_service_plan" "dev" {
  name                = "signmeup_dev"
  location            = azurerm_resource_group.dev.location
  resource_group_name = azurerm_resource_group.dev.name

  sku {
    tier = "Free"
    size = "F1"
  }
}

resource "azurerm_app_service" "client_service" {
  name                = "app-signmeup-client-svc-dev-eastus"
  location            = azurerm_resource_group.dev.location
  resource_group_name = azurerm_resource_group.dev.name
  app_service_plan_id = azurerm_app_service_plan.dev.id

  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY"                  = azurerm_application_insights.main.instrumentation_key
    "APPINSIGHTS_PROFILERFEATURE_VERSION"             = "1.0.0"
    "APPINSIGHTS_SNAPSHOTFEATURE_VERSION"             = "1.0.0"
    "APPLICATIONINSIGHTS_CONNECTION_STRING"           = azurerm_application_insights.main.connection_string
    "ApplicationInsightsAgent_EXTENSION_VERSION"      = "~2"
    "DiagnosticServices_EXTENSION_VERSION"            = "~3"
    "InstrumentationEngine_EXTENSION_VERSION"         = "disabled"
    "SnapshotDebugger_EXTENSION_VERSION"              = "disabled"
    "XDT_MicrosoftApplicationInsights_BaseExtensions" = "disabled"
    "XDT_MicrosoftApplicationInsights_Mode"           = "recommended"
    "XDT_MicrosoftApplicationInsights_PreemptSdk"     = "disabled"
  }
}

resource "azurerm_app_service" "client_site" {
  name                = "app-signmeup-client-site-dev-eastus"
  location            = azurerm_resource_group.dev.location
  resource_group_name = azurerm_resource_group.dev.name
  app_service_plan_id = azurerm_app_service_plan.dev.id

  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY"                  = azurerm_application_insights.main.instrumentation_key
    "APPINSIGHTS_PROFILERFEATURE_VERSION"             = "1.0.0"
    "APPINSIGHTS_SNAPSHOTFEATURE_VERSION"             = "1.0.0"
    "APPLICATIONINSIGHTS_CONNECTION_STRING"           = azurerm_application_insights.main.connection_string
    "ApplicationInsightsAgent_EXTENSION_VERSION"      = "~2"
    "DiagnosticServices_EXTENSION_VERSION"            = "~3"
    "InstrumentationEngine_EXTENSION_VERSION"         = "disabled"
    "SnapshotDebugger_EXTENSION_VERSION"              = "disabled"
    "XDT_MicrosoftApplicationInsights_BaseExtensions" = "disabled"
    "XDT_MicrosoftApplicationInsights_Mode"           = "recommended"
    "XDT_MicrosoftApplicationInsights_PreemptSdk"     = "disabled"
  }
}

resource "azurerm_application_insights" "main" {
  name                = "ai-signmeup-dev-eastus"
  location            = azurerm_resource_group.dev.location
  resource_group_name = azurerm_resource_group.dev.name
  application_type    = "web"
}

resource "azurerm_app_service" "vendor_service" {
  name                = "app-signmeup-vendor-svc-dev-eastus"
  location            = azurerm_resource_group.dev.location
  resource_group_name = azurerm_resource_group.dev.name
  app_service_plan_id = azurerm_app_service_plan.dev.id

  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY"                  = azurerm_application_insights.main.instrumentation_key
    "APPINSIGHTS_PROFILERFEATURE_VERSION"             = "1.0.0"
    "APPINSIGHTS_SNAPSHOTFEATURE_VERSION"             = "1.0.0"
    "APPLICATIONINSIGHTS_CONNECTION_STRING"           = azurerm_application_insights.main.connection_string
    "ApplicationInsightsAgent_EXTENSION_VERSION"      = "~2"
    "DiagnosticServices_EXTENSION_VERSION"            = "~3"
    "InstrumentationEngine_EXTENSION_VERSION"         = "disabled"
    "SnapshotDebugger_EXTENSION_VERSION"              = "disabled"
    "XDT_MicrosoftApplicationInsights_BaseExtensions" = "disabled"
    "XDT_MicrosoftApplicationInsights_Mode"           = "recommended"
    "XDT_MicrosoftApplicationInsights_PreemptSdk"     = "disabled"
  }
}

resource "azurerm_app_service" "vendor_site" {
  name                = "app-signmeup-vendor-site-dev-eastus"
  location            = azurerm_resource_group.dev.location
  resource_group_name = azurerm_resource_group.dev.name
  app_service_plan_id = azurerm_app_service_plan.dev.id

  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY"                  = azurerm_application_insights.main.instrumentation_key
    "APPINSIGHTS_PROFILERFEATURE_VERSION"             = "1.0.0"
    "APPINSIGHTS_SNAPSHOTFEATURE_VERSION"             = "1.0.0"
    "APPLICATIONINSIGHTS_CONNECTION_STRING"           = azurerm_application_insights.main.connection_string
    "ApplicationInsightsAgent_EXTENSION_VERSION"      = "~2"
    "DiagnosticServices_EXTENSION_VERSION"            = "~3"
    "InstrumentationEngine_EXTENSION_VERSION"         = "disabled"
    "SnapshotDebugger_EXTENSION_VERSION"              = "disabled"
    "XDT_MicrosoftApplicationInsights_BaseExtensions" = "disabled"
    "XDT_MicrosoftApplicationInsights_Mode"           = "recommended"
    "XDT_MicrosoftApplicationInsights_PreemptSdk"     = "disabled"
  }
}

resource "random_string" "sqlserver_client_user" {
  length  = 16
  special = false
}

resource "random_string" "sqlserver_vendor_user" {
  length  = 16
  special = false
}

resource "random_password" "sqlserver_client" {
  length = 16
}

resource "random_password" "sqlserver_vendor" {
  length = 16
  override_special = "!@#$%^_"
}

resource "azurerm_sql_server" "client" {
  name                         = "sql-signmeup-client-dev-eastus"
  location                     = azurerm_resource_group.dev.location
  resource_group_name          = azurerm_resource_group.dev.name
  administrator_login          = random_string.sqlserver_client_user.result
  administrator_login_password = random_password.sqlserver_client.result
  version                      = "12.0"
}

resource "azurerm_sql_database" "client" {
  name                = "db-signmeup-client-dev-eastus"
  location            = azurerm_resource_group.dev.location
  resource_group_name = azurerm_resource_group.dev.name
  server_name         = azurerm_sql_server.client.name
  edition             = "Free"
}

resource "azurerm_sql_server" "vendor" {
  name                         = "sql-signmeup-vendor-dev-centralus"
  location                     = "Central US"
  resource_group_name          = azurerm_resource_group.dev.name
  administrator_login          = random_string.sqlserver_vendor_user.result
  administrator_login_password = random_password.sqlserver_vendor.result
  version                      = "12.0"
}

resource "azurerm_sql_database" "vendor" {
  name                = "db-signmeup-vendor-dev-centralus"
  location            = "Central US"
  resource_group_name = azurerm_resource_group.dev.name
  server_name         = azurerm_sql_server.vendor.name
  edition             = "Free"
}

resource "random_password" "db_vendor" {
  length = 16
  override_special = "!@#$%^_"
}

resource "null_resource" "create-vendor-db-user" {

  provisioner "local-exec" {
    command = <<-EOT
      $ErrorActionPreference = "Stop"

      $queryMaster = "CREATE LOGIN mike WITH PASSWORD = 'Password123!';"
      $queryDb = "CREATE USER michael FOR LOGIN mike
                  ALTER ROLE  db_datareader ADD MEMBER michael
                  ALTER ROLE  db_datawriter ADD MEMBER michael
                  GO"

      $myIp = $(Invoke-RestMethod http://ipinfo.io/json).ip
      az sql server firewall-rule create --resource-group signmeup_dev --server ${azurerm_sql_server.vendor.name} -n temp_createUser --start-ip-address $myIp --end-ip-address $myIp
      Invoke-Sqlcmd -Query $queryMaster -ServerInstance ${azurerm_sql_server.vendor.fully_qualified_domain_name} -Username ${random_string.sqlserver_vendor_user.result} -Password ${random_password.sqlserver_vendor.result}
      Invoke-Sqlcmd -Query $queryDb -Database db-signmeup-vendor-dev-centralus -ServerInstance ${azurerm_sql_server.vendor.fully_qualified_domain_name} -Username ${random_string.sqlserver_vendor_user.result} -Password ${random_password.sqlserver_vendor.result}
      az sql server firewall-rule delete --n temp_createUser -g ${azurerm_resource_group.dev.name} -s ${azurerm_sql_server.vendor.name}
    EOT
    interpreter = ["PowerShell", "-Command"]
  }

  depends_on = [azurerm_sql_database.vendor]
}

#CREATE LOGIN mike WITH PASSWORD = 'password';
#USE db-signmeup-vendor-dev-centralus;  
#CREATE USER michael FOR LOGIN mike

#ALTER ROLE  db_datareader ADD MEMBER michael
#ALTER ROLE  db_datawriter ADD MEMBER michael
#GO
