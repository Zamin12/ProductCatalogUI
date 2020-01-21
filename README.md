## Product Catalog UI
  
MVC Web App for Managing product catalog solution.

## Getting Started

This MVC Web App provide UI over web to manage product catalog entities which uses RESTFul service.
It provides add, edit, remove, view and filter, export to excel actions.

Features: 
* ASP.NET Core MVC framework.
* Asynchronous actions.
* Unit tests for controller and service.
* Simple and clear UI design.
* Additional controller for System information and status of RESTFul API service.

## Technologies

Frameworks and packages:

* ASP.NET Core MVC: 3.0
* Newtonsoft.Json: 12.0.3
* DocumentFormat.OpenXml: 2.10.0

Tools: 

* Visual Studio Professional: 16.3.9

## Prerequisites

As configuration you will need clarify and change connection url to API service in ```appsettings.json``` and ```appsettings.Development.json``` files. 

```
{
  "ProductCatalogAPI": {
    "BaseUrl": "https://localhost:44373/api/v1/",
    "SystemInfoUrl": "https://localhost:44373/api/System/"
  },
  ...
}

```

Also it is possible to change Urls and ports for different profiles in ```Properties\launchsettings.json``` 

```
{
  "iisSettings": {
    "windowsAuthentication": false, 
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:9001",
      "sslPort": 44374
    }
  },
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "ProductCatalogUI": {
      "commandName": "Project",
      "launchBrowser": true,
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

## Authors

* **Zamin Ismayilov**
