## AverageLatency Application

A simple app that retrieves data on the average latency of different services

## Running the app

Either run the app directly in VS Studio if installed or by running the command "docker run" in your shell command prompt 
when being in the root application folder. The swagger should be available for use on http://localhost:5129/swagger/index.html or 
http://localhost:29951/swagger/index.html depending on how you run it.

## Structure

Due to the fact that the purpose of the application being quite small, I only used two projects, one for the application and one for the unittests.
A map structure and flow through the application has been implemented though in order to facilitate potential scaling of the application in the future.

## Assumptions

In the assignment description there was a note explaining how the data from the external service could return duplicate requestId's and that theese
should be filtered out from the response of the api. Using that info I made the assumption that there should only be one entry 
of a request (identified by the requestId), regardless of how many entries of that id that is retrieved from the external service on different days.

Due to the large amounts of duplicates returned from the external service this means that the number of requests stays quite the same when requesting 
larger datespans of data.

## Packages Installed

FluentValidation - Easy and developer friendly validation and testing of validation
FluentAssertion - Developer friendly package for writing assertions while testing