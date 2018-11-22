
## File Helpers
 https://www.filehelpers.net/quickstart/

## Running the app

```
cd C:\Users\mike\source\repos\ReadZippedCsv\ReadZippedCsv\bin\Debug\netcoreapp2.1
dotnet ReadZippedCsv.dll "C:\Users\mike\source\repos\ReadZippedCsv\ReadZippedCsv.Tests\TestData\DataLocksTestData.zip" /silent

dotnet ReadZippedCsv.dll "C:\Users\mike\Downloads\Reports 20180828-051544.zip" 
```

## Powershell

Self-sign scripts - https://www.hanselman.com/blog/SigningPowerShellScripts.aspx

Sign with 
``` 
Set-AuthenticodeSignature "C:\Users\mike\source\repos\ReadZippedCsv\SetupAzure.dev.ps1" @(Get-ChildItem cert:\CurrentUser\My -codesign)[0]
```

``` 
Set-AuthenticodeSignature "C:\Users\mike\source\repos\ReadZippedCsv\SetupAzure.ps1" @(Get-ChildItem cert:\CurrentUser\My -codesign)[0]
```

## Azure Functions

Note that the latest version has also broken blob triggers; fix is 
https://github.com/Azure/azure-functions-vs-build-sdk/issues/230

## Function Extensions and Binding

How to setup injection
https://blog.wille-zone.de/post/azure-functions-proper-dependency-injection/

For notes on the latest version of functions and what has changed see
https://github.com/Azure/azure-functions-vs-build-sdk/issues/232


More info
https://github.com/Azure/azure-webjobs-sdk/wiki/Creating-custom-input-and-output-bindings

https://blog.mexia.com.au/dependency-injections-on-azure-functions-v2

https://medium.com/@yuka1984/azure-functions-dependency-injection-with-injection-config-87fe5762c895

https://www.neovolve.com/2018/04/05/dependency-injection-and-ilogger-in-azure-functions/


## Azure setup

https://mohitgoyal.co/2018/01/26/provision-azure-storage-account-and-automate-file-upload-and-deletion-using-powershell/

```
Login-AzureRMAccount

# Create resource group
$resourceGroupName = "data-locks-rg"
$location = "uk west"
New-AzureRmResourceGroup -Name $resourceGroupName -Location $location

# Create storage account
$strAccountName = "datalockssa"
$location = "uk west"
$sku = "Standard_LRS"
New-AzureRmStorageAccount -ResourceGroupName $resourceGroupName -AccountName $strAccountName -SkuName $sku -Location $location

# Creates Azure Blob Storage Container

$strAccountKey = "72WjNT0uGfOh2tzs5JI2hyMKREoKci2dlBkkP9/xywJ0hqL4M4UljfGHEXYFKSU+zwh6XogOjBGB1rFxRNggcA==" # Replace this with your storage key
$strContext = New-AzureStorageContext -StorageAccountName $strAccountName -StorageAccountKey $strAccountKey
$containerName = "datalocks-uploads"
New-AzureStorageContainer -Name $containerName -Context $strcontext
```


To do this on local storage:

```
$strContext = New-AzureStorageContext -Local
$containerName = "datalocks-uploads"
New-AzureStorageContainer -Name $containerName -Context $strcontext

```

To upload files

```
# Based on these being in the powershell context:
$strContext =
$containerName = "datalocks-uploads"

# Upload a file
$file = Get-Item -Path "C:\Users\mike\Downloads\Reports 20180828-051544.zip"

Set-AzureStorageBlobContent -Blob $($file.Name) -Container $containerName -File $($file.FullName) -Con
text $strcontext
```

To send file as an http post 
https://stackoverflow.com/questions/38164723/uploading-file-to-http-via-powershell 
```
$uri = "http://localhost:7071/api/UpdateDataLocksHttp"
$uploadPath = "C:\Users\mike\Downloads\Reports 20180828-051544.zip"
Invoke-RestMethod -Uri $uri -Method Post -InFile $uploadPath -UseDefaultCredentials

#New functions (V2)
$uri = "http://localhost:7071/api/UpdateDataLocks"
$uploadPath = "C:\Users\mike\Downloads\Reports 20180926-051521.zip"
Invoke-RestMethod -Uri $uri -Method Post -InFile $uploadPath -UseDefaultCredentials
```

## Azure SQL Database

```
# Login-AzureRmAccount

$resourceGroupName = "data-locks-rg"
$location = "uk west"

# Set an admin login and password for your server
$adminlogin = "ServerAdmin"
$password = ""
# Set server name - the logical server name has to be unique in the system
$servername = "server-$(Get-Random)"

# The database name
$databasename = "DataLocks.Database"
# The ip address range that you want to allow to access your server
$startip = "0.0.0.0"
#$endip = "0.0.0.0"
$endip = "255.255.255.255"
# DON'T DO ABOVE - this allows all - get a more realistic range

# Create a resource group
# $resourcegroup = New-AzureRmResourceGroup -Name $resourceGroupName -Location $location

# Create a server with a system wide unique server name
$server = New-AzureRmSqlServer -ResourceGroupName $resourceGroupName `
    -ServerName $servername `
    -Location $location `
    -SqlAdministratorCredentials $(New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $adminlogin, $(ConvertTo-SecureString -String $password -AsPlainText -Force))

# Create a server firewall rule that allows access from the specified IP range
$serverfirewallrule = New-AzureRmSqlServerFirewallRule -ResourceGroupName $resourcegroupname `
    -ServerName $servername `
    -FirewallRuleName "AllowedIPs" -StartIpAddress $startip -EndIpAddress $endip

#Could deploy from VS, or do this:
# Create a blank database with an S0 performance level
$database = New-AzureRmSqlDatabase  -ResourceGroupName $resourceGroupName `
    -ServerName $servername `
    -DatabaseName $databasename `
    -RequestedServiceObjectiveName "S0" #`
#    -SampleName "AdventureWorksLT"
```
