# pruebaTecnica
Prueba Técnica Dev Sr. - Ariel Flores

******************************
*********DATOS PRUEBA*********
******************************
CLIENTE 1
Id Cliente: 1
Id Cuenta: 1
# Cuenta: 3039905461606924

--------------------------------------------------------------------

La prueba se desarrolló en Visual Studio C# Net Framework 8.0
Se puede testear por medio de swagger

--------------------------------------------------------------------

1. Se pueden crear nuevos clientes
2. Se pueden crear nuevas cuentas bancarias asociadas al cliente por medio del 'Id Cliente'
	Los números de cuenta se crean automáticamente validando que no existan.
3. Se puede consultar el saldo de la cuenta por medio del 'número cuenta'
4. Se pueden realizar depositos (TipoTransaccion = 'Deposito') y retiros (TipoTransaccion = 'Retiro')
	Se realizan las respectivas validaciones de fondos suficientes, y se actualizan los saldos con los movimientos.
5. Se puede obtener un resumen de las transacciones realizadas por 'número de cuenta'


-----------------------------------------------------------------------------

HE PUBLICADO EL API EN UN SERVIDOR, SE PUEDE ACCEDER POR EL SIGUIENTE URL:
http://104.237.6.49:3010/swagger/index.html

CLIENTE 1
Id Cliente: 1
Id Cuenta: 1
# Cuenta: 8087365210377875


