# Deployment instructions: (ENG)

TODO: complete

# Instrucciones de despliegue: (ES)
En este anexo se describirán las instrucciones para desplegar y operar
el prototipo. El código se encuentra en un repositorio público[^34].
Para seguir estas instrucciones, deberemos clonarlo con `git` o
descargarlo como un fichero zip.

Requisitos
----------

Para poder ejecutarlo existen los siguientes requisitos. Todos los
programas y lenguajes requeridos son compatibles con las principales
plataformas (Windows, Linux y Mac).

-   SDK de .NET v6.0 o superior[^1].

-   Docker Compose v2.0 o superior [^2].

-   Powershell v5 o Powershell Core[^3] (para ejecutar el *script* de
    compilación).

-   Python 3.7 o superior[^4] (para generar los API clients).

Generar API Clients
-------------------

Si se hiciera algún cambio sobre los *endpoints* que exponen los
servicios, tendremos que regenerar los API Clients. Para ello, contamos
con un *script* escrito en Python. Este compila todos los proyectos,
genera las especificaciones OpenAPI y, a partir de ellas, genera los
clientes.

El *script* se encuentra en la ruta
`src/AutoAdaptativeSystem/GenerateApiClient.py`. Para ejecutarlo, basta
con invocarlo con Python: `python3 GenerateApiClient.py`. Para que
funcione correctamente, todos los proyectos deben encontrarse en un
estado compilable.

Si hubiera algún cambio incompatible que haga fallar la compilación,
tendremos que corregirlo antes de poder continuar. Por ejemplo, acceder
al servicio que falla y solucionar los errores. Hecho esto, podremos
ejecutar de nuevo el *script*. Tendremos que hacer esto hasta que se
generen todos los clientes correctamente.

Despliegue
----------

Para compilar y ejecutar la solución contamos con un *script* de
Powershell. Este se encuentra en la ruta
`src/AutoAdaptativeSystem/build.ps1`. Se encarga de:

1.  Compilar todos los proyectos de la solución.

2.  Publicar todos los proyectos a una carpeta común.

3.  Crear los contenedores de Docker y levantar la solución con Docker
    Compose.

Para ejecutarlo, usaremos la instrucción `pwsh build.ps1`. Si todo ha
ido bien, veremos en la consola lo siguiente:

    Container publish-prometheus-1                    Started         3.4s
    Container publish-climatisation_monitor-1         Started         1.7s
    Container publish-monitoring-1                    Started         2.9s
    Container publish-loki-1                          Started         3.4s
    Container publish-jaeger-1                        Started         2.5s
    Container publish-climatisation_airconditioner-1  Started         1.5s
    Container publish-grafana-1                       Started         1.9s
    Container publish-rabbitmq-1                      Started         1.7s
    Container publish-planning-1                      Started         3.8s
    Container publish-climatisation_effectors-1       Started         4.2s
    Container publish-climatisation_rules-1           Started         4.3s
    Container publish-execute-1                       Started         4.2s
    Container publish-knowledge-1                     Started         3.5s
    Container publish-analysis-1                      Started         4.1s

En cambio, si quisiéramos parar su ejecución, tendremos que acceder a la
carpeta `src/AutoAdaptativeSystem/publish` y ejecutar el comando
`docker-compose down`. Este borrará todas las redes y contenedores que
se hayan creado durante el despliegue.

Interactuar con el sistema
--------------------------

### Servicios disponibles

A continuación, se describen algunos de los *endpoints* más interesantes
para interactuar con el sistema siguiente. Para consultar la lista completa, puede
accederse al fichero `docker-compose.yml`.

| **Servicio** | ***Endpoint*** | **Descripción** |
|--------------|-----------|------------|
| Grafana |<http://localhost:3000> | Acceso a los paneles de monitorización.|
| Climatisation AirConditioner | <http://localhost:11001/swagger> | Permite manipular el aire acondicionado y su temperatura actual.|
|Knowledge | <http://localhost:5001/swagger> | Permite consultar y manipular las propiedades de adaptación.


### Controlar el aire acondicionado

Para controlar el funcionamiento del aire acondicionado, podemos acceder
a la URL correspondiente de la tabla anterior. Nos mostrará una pantalla con la lista
de operaciones disponibles. Nos interesan especialmente
las operaciones de `FakeAirConditioner`. Con ellas podremos simular
situaciones distintas para forzar al sistema a adaptarse.

La primera, nos permite cambiar el comportamiento de la temperatura
ficticia cuando el dispositivo esté apagado. Se trata del *endpoint*
`/FakeAirConditioner/disabled-mode-configuration/{shouldIncreaseTemperature}`. Si se le
pasa el valor `true`, la temperatura aumentará. Esto forzará a ejecutar
las adaptaciones para enfriar la habitación. Por otro lado, si se le
pasa `false`, la estancia se enfriará y el aparato tendrá que
calentarla.

También podemos asignar manualmente la temperatura actual. Con este fin,
tenemos el *endpoint* `/FakeAirConditioner/temperature`. Así, podrá
forzarse la activación de un modo determinado. Aunque permite
especificar una unidad, de momento solo están soportadas las
temperaturas en grados Celsius.

### Paneles de monitorización en Grafana

Para poder visualizar el estado pasado y actual del sistema contamos con
el panel de monitorización de Grafana[^5]. Al acceder encontraremos
algo similar a la figura. Contaremos con distintas gráficas y
visualizaciones de métricas de interés. Por ejemplo, el valor de la
temperatura o el tiempo medio de adaptación.

![Panel de monitorización
`AdaptionLoop`.](../docs/dissertation/cap_despliegue/images/Grafana-panel-monitorizacion.png)

Podemos también explorar las fuentes de datos o ampliarlo con nuevas
visualizaciones[^6]. Si queremos hacer cambios y persistirlos para
futuras ejecuciones, tendremos que exportar el panel y guardar su
contenido en el fichero
`/src/AutoAdaptativeSystem/ config/grafana/dashboards/Adaption-Loop.json`.

------

[^1]: <https://dotnet.microsoft.com/en-us/download/dotnet/6.0>

[^2]:<https://docs.docker.com/compose/install/>

[^3]: <https://docs.microsoft.com/es-es/powershell/scripting/install/install-other-linux?view=powershell-7.2#install-as-a-net-global-tool>

[^4]: <https://www.python.org/downloads/release/python-3106/>

[^5]: <http://localhost:3000/d/N0ZSfeUnz/adaptionloop?orgId=1&refresh=10s>

[^6]: Grafana cuenta con muy buena documentación:
    <https://grafana.com/tutorials/>
