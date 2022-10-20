# Refactoring an MAPE-K loop infrastructure as microservices (ENG)

This is my dissertation for the master's degree in Software Engineering, Formal Methods and Information Systems (MITSS) from the Universidad Politécnica de Valencia (UPV).

Reference: http://hdl.handle.net/10251/188361

## Abstract:
  Autonomic Computing promotes the engineering, design, and development of systems with self-adaptive capabilities, using control loops. These capabilities allow those systems to adapt to changing environments, to resolve operational conflicts and even to optimize dynamically at runtime. On the other hand, in the past decade, cloud computing and microservice architectures have been promoted as the basis for dynamic and flexible architectures to deploy highly available and efficient solutions. There is a clear tendency to apply these types of infrastructures, thanks to the many benefits they promote.

  This dissertation will explore how to design solutions that, when applying the concepts of feedback loops (AC), will be ready to be deployed to the cloud. For this, the FaDA infrastructure (developed by the PROS/Tatami group from the VRAIN/UPV institute) will be used as a starting point. It proposes a strategy for the engineering of self-adaptive system using MAPE-K control loops.

  As a result of this dissertation, it is expected to obtain the architectural definition of self-adaptive solutions (including the MAPE-K control loop and guidelines to implement the different adaptive components of the solution) designed to be deployed as microservices in the cloud. Finally, this proposal will be applied to the development of a case study to demonstrate its feasibility and applicability.

## Repository's structure:
Currently, the repository is divided in two parts:

- `docs`: The LaTeX project for the dissertation. Contains a PDF file with the latest version of the dissertation (in spanish). It includes an extensive description of the architecture of the distributed MAPE-K loop.

- `src`: The source code of the project. It contains instructions on how to execute the developed prototype.

----

# Refactorización de una infraestructura de bucles MAPE-K como microservicios (ES)

Este es mi trabajo final para el máster en Ingeniería y Tecnología de Sistemas Software de la Universidad Politécnica de Valencia (UPV).

Referencia: http://hdl.handle.net/10251/188361

## Abstract:
  La Computación Autónoma (_Autonomic Computing_) promueve la ingeniería, diseño y desarrollo de sistemas con capacidades de autoadaptación, a través del uso de bucles de control. Estas capacidades le confieren a estos sistemas la posibilidad de adaptarse a entornos cambiantes, a resolver conflictos operacionales e incluso a la optimización dinámica en su ejecución. Por otra parte, en la última década, la computación en el _cloud_ y las arquitecturas basadas en microservicios se han postulado como una infraestructura muy flexible y dinámica para desplegar soluciones altamente disponibles y eficientes. Hay una tendencia clara a aplicar este tipo de infraestructuras, gracias a los múltiples beneficios que aporta.

  En este trabajo se explorará cómo diseñar soluciones que, aplicando los conceptos de los bucles de control (AC), estén preparadas para desplegarse en la nube. Para ello se tomará como punto de partida la infraestructura FaDA (desarrollada por el grupo PROS/Tatami del instituto VRAIN/UPV) que propone una estrategia para realizar la ingeniería de sistemas autoadaptativos usando bucles de control MAPE-K.

  Como resultado de este TFM se espera obtener la definición arquitectónica de soluciones autoadaptativas (incluyendo tanto al bucle de control MAPE-K como directrices para la implementación de los diferentes componentes adaptativos de la solución) diseñadas para desplegarse nativamente como microservicios en la nube. Por último, se aplicará la propuesta realizada al desarrollo de un caso práctico para demostrar su viabilidad y aplicabilidad.

## Estructura del repositorio:
Actualmente, se divide en dos partes:

- `docs`: El proyecto LaTeX de la memoria. Contiene un fichero PDF con la última versión del documento (en castellano). Describe extensamente la arquitectura del bucle MAPE-K distribuido.

- `src`: El código fuente del proyecto. Contiene instrucciones sobre cómo ejecutar el prototipo desarrollado.
