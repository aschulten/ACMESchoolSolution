# ACMESchoolSolution

## Decisiones de diseño y implementación

* Se eligió una arquitectura de capas para separar responsabilidades, facilitando la mantenibilidad y escalabilidad del proyecto. Esto asegura que cambios en una capa no afecten a las demás, permitiendo una evolución más ágil.
* Las interfaces para los repositorios y servicios permiten cambiar implementaciones sin afectar al resto del sistema, apoyando la flexibilidad del código y la adherencia a principios SOLID como la Inversión de Dependencias.

## Consideraciones adicionales

* Se utilizó una aplicación de consola para facilitar la carga de datos y simular interaccíon del usuario.
* La persistencia en archivos fue elegida para simplificar la prueba y la demostración del concepto. Est0 permite una rápida iteración y testing manual sin necesidad de configurar bases de datos.
* Se implementó una clase mock para simular el comportamiento del gateway de pago.

## Cosas que me gustaría haber hecho pero no pude

* Implementación de logs para facilitar el seguimiento de errores y la auditoría de la aplicación en entornos de producción.

## Cosas que hice pero que podrían mejorarse

* Me hubiera gustado hacer un repaso exhaustivo de todo el código, especialmente en las áreas de validación y manejo de excepciones, para asegurarse de que todas las rutas de código sean robustas y fácilmente mantenibles.
* La implementación del gateway de pago podría incluir validaciones más estrictas y mejor manejo de errores.
* El testing podría haber cubierto más casos de borde, como la validación de entradas inválidas, para asegurar la fiabilidad de la aplicación.
