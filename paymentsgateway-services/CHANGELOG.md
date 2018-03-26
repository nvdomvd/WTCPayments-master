# [1.0.1](https://bitbucket.org/wb_magentainnova/paymentsgateway-services/branches/compare/v1.0.1%0Dv1.0.0) (2017-11-27)
### Improvements
* **Entities Validation:** Se agrego validacion en el service a las entidades, y al dar error se retorna mensaje apropiado
* **Log:** El log ahora se hace con un archivo distinto por dia

# [1.0.0](https://bitbucket.org/wb_magentainnova/paymentsgateway-services/commits/tag/v1.0.0) (2017-11-17)
### Features
* **Validate credentials:** Validacion de user y password recibidas.
* **Validate operation:** Control de que el cliente ingresado tenga un role para hacer la operación a ejecutar.
* **Create payment:** Recibe los datos y crea el pago en el sistema con el estado adecuado.
* **Confirm payment:** Confirmacion de un pago ingresado anteriormente, asignandole el estado apropiado con la informacion brindada por el gateway.