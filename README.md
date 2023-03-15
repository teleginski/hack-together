# Email Automation

This project is an example of how to integrate Microsoft Graph with a Blazor Server App, made during the [Hack Together](https://devblogs.microsoft.com/microsoft365dev/join-us-for-hack-together-microsoft-graph-and-net/) event.

features.

* Filter your emails by specific email (From) or subject
* Filter specific attachment file with email and save in `OneDrive`.
* Create `Todo` task.
* Create `Calendar` event

## Stack

* Blazor Server
* .NET v7.0
* Microsoft Graph.

## Instructions to run

### 1. Register an Azure Active Directory app

  * Reference 
    * [Quickstart: Register an application with the Microsoft identity platform](https://learn.microsoft.com/azure/active-directory/develop/quickstart-register-app)

  * Important:
    * Set the **Redirect URI** drop down to **Web** and enter `https://localhost:5001/signin-oidc`.

### 2. Run app

  * Replace the settings with *appsettings.json* file, by generate in Azure Portal:
    * `CLIENT_ID`
    * `TENANT_ID`
    * `CLIENT_SECRET`