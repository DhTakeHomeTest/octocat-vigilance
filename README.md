# octocat-vigilance - Main Branch Protection WebService For GitHub

octocat-vigilance is a simple web service that listens for organization events to know when a repository has been created. When a new repository is created the main branch is automatically protected. A new issue is also added to the repository mentioning the account owner of the new protections added. Interactivity with GitHub API is using [Octokit](https://github.com/octokit/octokit.net/).

## Getting Started

The first step is determine where the web service will be hosted. From my purposes this is to be deployed directly to Azure App Services. Learning how to get started with App Services and how to configure the azure environment can be found at [Quickstart: Deploy an ASP.NET web app](https://docs.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net60&pivots=development-environment-vs).

Once your App Services instance has been configured, you can configure your publish profile to have your development environment deploy directly to Azure. Details can be found on MSDN: [Publish an ASP.NET Core app to Azure with Visual Studio](https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-azure-webapp-using-vs?view=aspnetcore-5.0).

Once deployed in Azure you are required to configure the settings used by the web service. Inside of Azure portal go to the configuration page for the app service.

There are three settings which need updating:

- GithubClientSettings__User - Your GitHub username
- GithubClientSettings__PAT - A Personal access token to give access to the API. This should be created in accordance with the guide [Creating a personal access token](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token)
- ApplicationInsights__InstrumentationKey - This is the instrumentation key for your application insights instance as set up using the guide above.

![Screen of app service configuration settings](https://raw.githubusercontent.com/DhTakeHomeTest/octocat-vigilance/main/assets/AppServiceConfiguration.png?token=AL4AICJ6F73YB5GNJRCRGSTBRY746)

Following configuration of the web service, next you need to configure a GitHub [Webhook](https://developer.github.com/webhooks/) to point to the url of the app service configured. You can see the base url for the service on the app service overview page. The Webhook is this base url + "GitHubWebhook".

![Screenshot of App Service Overview](https://raw.githubusercontent.com/DhTakeHomeTest/octocat-vigilance/main/assets/AppServiceConfiguration.png?token=AL4AICMWWL4R3WEEX422VJDBRZAGG)

![Screenshot of Github Webhooks configuration](https://raw.githubusercontent.com/DhTakeHomeTest/octocat-vigilance/main/assets/GitHubWebHook.png?token=AL4AICPQEE3CTNAG5DTSHOTBRZADC)



## References Used

[GithubWebhook](https://github.com/PromoFaux/GithubWebhook) - This nuget package is used for defining the proxy classes sent by Github Webhooks.

[Octokit.net](https://github.com/octokit/octokit.net) - This nuget package is used to communicate with the GitHub API, the documentation is referenced from [Octokit.net - docs](https://octokitnet.readthedocs.io/en/latest/)

## Copyright and License

Licensed under the [MIT License](https://opensource.org/licenses/MIT)