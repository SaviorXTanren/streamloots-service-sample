# streamloots-service-sample
Sample code showing how to connect to Streamloots to receive alerts for a user. The bulk of the code can be found in the following two files:

* StreamlootsModels.cs = All of the model classes used by the service

* StreamlootsService.cs = The service logic to connect and listen for alerts from Streamloots

To use the sample with your own account, do the following steps:

* Create an account / log in to Streamloots: https://www.streamloots.com/

* On your dashboard, head to the Alerts section under Page Configuration

* Under "Alert provider", click on the grayed out box that says "Click here to show URL"

* Copy the GUID value at the end of your alerts URL, this is your alerts ID. The format of the URL is: https://widgets.streamloots.com/alerts/< GUID >

* Build the project and run it with the GUID ID as a command-line parameter
