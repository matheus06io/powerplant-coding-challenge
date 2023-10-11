# Microservice Power Calculation

## Service Intro

## Tech Details

* `.Net 7`
* Domain Layer using `Exceptions`
* App Layer using `FluentResult` and `Error Flow Control`.
* Input validation using `FluentValidation`.
* `Mediatr` to dispatch `Commands` and `Queries` and generic `ValidationBehavior` for them.
* Manual Mapping between `DTO` and `Domain Objects`.
* `Dapr` As `EventBus` to publish\subscribe `Integration Events`.
* `Health Check` for Endpoint.
* Unit Tests
