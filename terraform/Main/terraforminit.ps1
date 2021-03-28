$Env:ARM_SAS_TOKEN=$(az keyvault secret show --vault-name kv-terraform-51916 --name StateStorageSas --query value --output tsv)
terraform init
terraform workspace select Dev
