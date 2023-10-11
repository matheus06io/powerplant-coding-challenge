param (
    [Parameter(Mandatory = $false)]
    [string]
    $RegistryName = "matheus06"
)
Begin {
    
}
Process {
    function Build-Docker-Images {

        docker build ../../../microservices/ -f ../../../microservices/Dockerfile  -t $RegistryName/powercalculation:latest
        docker build ../../../microservices/ -f ../../../microservices/Dockerfile.ppc  -t $RegistryName/productionplancalculator:latest
        Write-Output "build done"
    }

    function Push-Docker-Images {
        docker push $RegistryName/powercalculation:latest
        docker push $RegistryName/productionplancalculator:latest
        Write-Output "push done"
    }
    
    ########################
    # main execution point
    ########################
    Build-Docker-Images
    Push-Docker-Images

    Write-Output "work done"
}
End {
    
}