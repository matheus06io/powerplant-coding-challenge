# Power Plant Calculation Project

* [Microservice PowerCalculation](/code/microservices/microservice.powercalculation)

## Architecture

Local:

![architecture-local](/code/docs/arch-local.png)

K8s:

![architecture-k8s](/code/docs/arch-k8s.png)

## How To Run

### Requirements

* Docker
* Tye (for local DEBUG only)
* Dapr
* .Net 7
* SQL

### Run Locally using Tye

* `winget install Dapr.CLI` => install dapr locally.
* `dapr init` => Initialize Dapr in your local environment.
* `build` the application.
* Go to the `\microservices` directory
* `tye run` => run the services using the .NET csproj files in order to DEBUG.

* Urls
  * Power Calculation API Docs => <http://localhost:8888/swagger>
  * HealthCheck => <http://localhost:8888/hc-ui>
  * Tye Dashboard => <http://127.0.0.1:8000/>

### Run Locally using Docker Compose

* Go to the `\microservices` directory
* `docker compose up`

* Urls
  * Power Calculation API Docs => <http://localhost:8888/swagger>
  * HealthCheck => <http://localhost:8888/hc-ui>

### Run Locally using Docker K8s Cluster

Add `127.0.0.1 localdev-tls.me` to your `C:\Windows\System32\drivers\etc\hosts` file

![hosts](/code/docs/hosts.png)

Install ngnix

```console
helm upgrade --install ingress-nginx ingress-nginx  --repo https://kubernetes.github.io/ingress-nginx   --namespace ingress-nginx --create-namespace
```

Install dapr

* `winget install Dapr.CLI` => install dapr locally.

```console
dapr init -k
```

Install redis

```console
helm repo add bitnami https://charts.bitnami.com/bitnami
helm repo update
helm install redis bitnami/redis --set image.tag=6.2
```

Apply k8s configs

* Go to the `\microservices\k8s\scripts` directory

* Create the namespace

kubectl apply -f ../files/namespace.yaml

* Copy redis secret from `default` namespace to `powerplantcodingchallenge` namespace
  
`you first need to install yq => <https://github.com/mikefarah/yq/#install>`

```console
kubectl --namespace=default get secret redis -o yaml | yq 'del(.metadata.creationTimestamp, .metadata.uid, .metadata.resourceVersion, .metadata.namespace)' | kubectl apply --namespace=powerplantcodingchallenge -f -
```

* Using powershell to apply k8s manifest files

Apply Deployments

```console
.\kubectl-apply-all-deployments.ps1     
```

* Using powershell to delete k8s manifest files

Delete Deployments

```console
 .\kubectl-delete-all-deployments.ps1    
```

Add localhost to hosts file

* k8s Urls ()
  * Power Calculation API Docs => <https://localdev-tls.me/swagger>
  * HealthCheck => <https://localdev-tls.me/hc-ui>

## How To Run Unit Tests

* Go to the `\microservices\microservice.powercalculation\test\Microservice.PowerCalculation.Test` directory

```console
 dotnet test --logger "console;verbosity=detailed" 
```

## Async Swagger Endpoints

Those endpoints functionalities are not finished, is to demonstrate how to use long run operations by publishing message to a different service using `Dapr`:

![swagger](/code/docs/swagger.png)

You can use the `/productionplan/asyncCalculation` to tigger the event and check `microservices-power-calculation` log:

```console
INFO Publishing event {"RequestId": "e9759758-1735-4c9c-bd8d-9271f1799945", "Payload": "{\"Load\":0,\"Fuels\":{\"gas(euro/MWh)\":0,\"kerosine(euro/MWh)\":0,\"co2(euro/ton)\":0,\"wind(%)\":0},\"PowerPlants\":[{\"Name\":\"string\",\"Type\":\"GasFired\",\"Efficiency\":0,\"Pmin\":0,\"Pmax\":0}]}", "Id": "15adedc2-be5e-4e8f-8410-00ae3480e1af", "CreationDate": "2023-10-11T18:59:51.6332060Z", "TopicName": null, "$type": "CalculateProductionPlanAsyncIntegrationEvent"} to pubsub.CalculateProductionPlanAsyncIntegrationEvent
```

The other service will receive the message and handle it, you can also check `microservices-production-plan-calculator` log :

```console
CalculateProductionPlanAsyncIntegrationEvent received => EventId: 06b73e1f-456e-493c-b626-620d866cd70f CreationDate: 10/11/2023 18:59:51 RequestId: e9759758-1735-4c9c-bd8d-9271f1799945 Payload: {"Load":0,"Fuels":{"gas(euro/MWh)":0,"kerosine(euro/MWh)":0,"co2(euro/ton)":0,"wind(%)":0},"PowerPlants":[{"Name":"string","Type":"GasFired","Efficiency":0,"Pmin":0,"Pmax":0}]}
```
