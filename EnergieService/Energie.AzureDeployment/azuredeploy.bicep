param appServiceBaseName string
param appServiceBaseSkuName string
param webAppName string
param webAppInsightsName string
param environmentName string
param functionAppname string
param storageAccountName string
param keyvaultName string
param runtime string = 'dotnet'
param storageAccountType string = 'Standard_LRS'
param containerNames array = ['energy-company-logos','employer-help','energy-analysis'] 
param location string = resourceGroup().location
//param timeforplan string
//param timeforanalysis string
//param timeformonthlyscore string

resource appServiceBase 'Microsoft.Web/serverfarms@2015-08-01' = {
  name: appServiceBaseName
  location: location
  sku: {
    name: appServiceBaseSkuName
  }
  tags: {
    displayName: 'appServiceBase'
  }
  dependsOn: []
}

resource webApp 'Microsoft.Web/sites@2015-08-01' = {
  name: webAppName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: appServiceBase.id
    siteConfig: {
      netFrameworkVersion: 'v6.0'
      httpsOnly: true
      //ftpsState: 'FtpsOnly'
      appSettings: [
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: environmentName
        }
        //{
        //  name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
        //  value: '~2'
        //}
      ]
    }
  }
}

resource storageAccount 'Microsoft.Storage/storageAccounts@2022-05-01' = {
  name: storageAccountName
  location: location
  sku: {
    name: storageAccountType
  }
  kind: 'StorageV2'
  properties: {
    supportsHttpsTrafficOnly: true
    defaultToOAuthAuthentication: true
  }
}

resource functionApp 'Microsoft.Web/sites@2020-06-01' = {
  name: functionAppname
  location: location
  kind: 'functionapp'
  properties: {
    serverFarmId: appServiceBase.id
    siteConfig: {
      appSettings: [
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccountName};EndpointSuffix=${environment().suffixes.storage};AccountKey=${storageAccount.listKeys().keys[0].value}'
        }
        {
          name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccountName};EndpointSuffix=${environment().suffixes.storage};AccountKey=${storageAccount.listKeys().keys[0].value}'
        }
        {
          name: 'WEBSITE_CONTENTSHARE'
          value: toLower(functionAppname)
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '-4'
        }
        {
          name: 'WEBSITE_NODE_DEFAULT_VERSION'
          value: '~14'
        }
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: webAppInsights.properties.InstrumentationKey
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: runtime
        }
        //{
        //  name: 'NotificationProcessorTimeForPlan'
        //  value: timeforplan
        //}
        //{
        //  name: 'NotificationProcessorTimeForAnalysis'
        //  value: timeforanalysis
        //}
        //{
        //  name: 'NotificationProcessorTimeForMonthlyScore'
        //  value: timeformonthlyscore
        //}
      ]
      httpsOnly: true
      use32BitWorkerProcess: false
      netFrameworkVersion: 'v7.0'
    }
      dependsOn: [
        appServiceBase
  ]
  tags: {
    displayName: 'FunctionApp'
  }

  }
}

resource functionAppAuthSettings 'Microsoft.Web/sites/config@2021-01-15' = {
  name: '${functionApp.name}/authsettings'
  properties: {
    unauthenticatedClientAction: 'AllowAnonymous'
  }
}

output functionAppName string = functionApp.name


resource containers 'Microsoft.Storage/storageAccounts/blobServices/containers@2022-09-01' = [for containerName in containerNames : {
  name: '${storageAccount.name}/default/${containerName}'
  properties: {
    publicAccess: 'Container'
    defaultEncryptionScope: '$account-encryption-key'
    denyEncryptionScopeOverride: false
    accessTier: 'Hot'
  }
}]

resource keyvault 'Microsoft.KeyVault/vaults@2022-07-01' = {
  name: keyvaultName
  location: location
  properties: {
    tenantId: subscription().tenantId
    sku: {
      name:'standard'
      family: 'A'
    }
      accessPolicies: [
      
    ]
  }
}


resource webAppInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: webAppInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type:'web'
    Flow_Type: 'Redfield'
    Request_Source: 'rest'
  }
}
