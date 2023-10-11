Begin {
    
}

Process {

    function Invoke-All-Deployments {
        kubectl apply -f ../files/namespace.yaml
        kubectl apply -f ../files/ingress-secret.yaml
        kubectl apply -f ../files/ingress-service.yaml
        kubectl apply -f ../files/power-calculation-deployment.yaml
        kubectl apply -f ../files/production-plan-calculator-deployment.yaml

        # dapr components
        kubectl apply -f ../components/redis-pubsub-default.yaml
        kubectl apply -f ../components/redis-state-default.yaml
        kubectl apply -f ../components/redis-pubsub.yaml
        kubectl apply -f ../components/redis-state.yaml
        Write-Output "apply done"


        kubectl get pods --namespace=powerplantcodingchallenge 
        kubectl get services --namespace=powerplantcodingchallenge
        kubectl get deployments --namespace=powerplantcodingchallenge
        kubectl get replicaset --namespace=powerplantcodingchallenge
        kubectl get ingress --namespace=powerplantcodingchallenge
        kubectl get components --namespace=powerplantcodingchallenge
        kubectl get components --namespace=default
    }
    
    ######################
    # main execution point
    ######################
    Invoke-All-Deployments 

    Write-Output "work done"
}
End {
    
}