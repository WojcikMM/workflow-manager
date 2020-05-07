# This is kubernetes configuration for Workflow Manager System

## Kubernetes configuration files
* deployment.yaml
* service.yaml - internal service (ClusterIP)
* service-ext.yaml - external service (LoadBalancer)
* volume.yaml
* secret.yaml

## Folder structure

```
kubernetes
|  base
|  |  kustomization.yaml
|  |  identity-service
|  |  |  kustomization.yaml
|  |  |  api
|  |  |  |  deployment.yaml
|  |  |  |  service-ext.yaml
|  |  |  |  ...
|  |  |  mssql
|  |  |  |  deployment.yaml
|  |  |  |  service.yaml
|  |  |  |  volume.yaml
|  |  |  |  secret.yaml
|  |  |  |  ...
|  |  shared
|  |  |  kustomization.yaml
|  |  |  rabbit-mq
|  |  |  |  deployment.yaml
|  |  |  |  service.yaml
|  |  |  |  service-ext.yaml
|  |  |  |  ...
|  |  |  ...
|  environments
|  |  dev ( development configuration )
|  |  |  kustomization.yaml ( main transformation definition file )
|  |  |  set-replicas.yaml ( specific transformation file )
```
* base - contains base service definitions (like deployments services volumes etc.)
    *  _component_name_
        * api (contains backend service)
        * mssql (contains component MSSQL database)
    *  shared - contains common components (like message brokers etc.)
        * _shared_resource_name_
* environments - contains environment specific environment transformation
    * dev ( some environment name)
        * kustomization.yaml (transform base configuration)

_On every level on __base__ configuration there is __kustomization.yaml__ for grouping purpose_

### TODO
1. Add limit and --requests-- for configurations
2. Check [_kustomize_](https://github.com/kubernetes-sigs/kustomize/tree/master/examples) mechanism for features:
    * resources
    * configmapGenerator
    * secretGenerator
    * generatorOptions
    * patchesStrategicMerge
    * patchesJson6902
    * vars
    * images
    * configurations
    * crds