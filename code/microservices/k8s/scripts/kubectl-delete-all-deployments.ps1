Begin {
    
}

Process {

    function Remove-Deployments {
        kubectl delete -f ../files/ingress-secret.yaml
        kubectl delete -f ../files/ingress-service.yaml
        kubectl delete -f ../files/power-calculation-deployment.yaml
        kubectl delete -f ../files/production-plan-calculator-deployment.yaml

        # dapr components
        kubectl delete -f ../components/redis-pubsub-default.yaml
        kubectl delete -f ../components/redis-state-default.yaml
        kubectl delete -f ../components/redis-pubsub.yaml
        kubectl delete -f ../components/redis-state.yaml
        Write-Output "remove done"
 
        kubectl get pods --namespace=powerplantcodingchallenge 
        kubectl get services --namespace=powerplantcodingchallenge
        kubectl get deployments --namespace=powerplantcodingchallenge
        kubectl get replicaset --namespace=powerplantcodingchallenge
        kubectl get ingress --namespace=powerplantcodingchallenge
        kubectl get components --namespace=powerplantcodingchallenge
        kubectl get components --namespace=default

        kubectl delete -f ../files/namespace.yaml
    }
    
    ######################
    # main execution point
    ######################
    Remove-Deployments 

    Write-Output "work done"
}
End {
    
}