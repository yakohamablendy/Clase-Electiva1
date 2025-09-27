# Proyecto: Pipeline ETL para Análisis de Opiniones de Clientes

Este proyecto implementa un pipeline ETL (Extract, Transform, Load) en C# para procesar opiniones de clientes desde archivos CSV y cargarlas en una base de datos SQL Server.

## 1. Modelo de la Base de Datos

La base de datos se diseñó con cuatro entidades principales para asegurar la integridad de los datos y evitar redundancias.

-   **Clientes**: Almacena la información de cada cliente.
-   **Productos**: Contiene el catálogo de productos.
-   **Fuentes**: Guarda el origen de donde proviene cada opinión (ej. Encuesta, Web).
-   **Opiniones**: Es la tabla central que conecta a un cliente, un producto y una fuente para registrar una opinión específica.

### Diagrama Entidad-Relación (ER)

<img width="641" height="541" alt="Diagrama de la base de datos drawio" src="https://github.com/user-attachments/assets/55472d30-de64-4289-900e-edfcf54ed960" />


## 2. Flujo de Trabajo del Pipeline ETL

El programa sigue un proceso de 3 fases, con cada fase organizada en su propio archivo de código para mayor claridad.

### Fase 1: Extracción (`Extraccion.cs`)
El programa lee los cuatro archivos de datos (`clientes.csv`, `productos.csv`, `fuentes.csv`, `opiniones.csv`) desde la carpeta `datos_csv` y carga la información en listas de objetos en memoria.

### Fase 2: Transformación (`Transformacion.cs`)
Se aplica una regla de limpieza de datos: se eliminan los clientes duplicados que puedan tener el mismo correo electrónico, asegurando que cada cliente sea único en el sistema.

### Fase 3: Carga (`Carga.cs`)
El programa se conecta a la base de datos SQL Server. Antes de insertar, borra todos los datos existentes en las tablas para asegurar una carga limpia. Luego, inserta los datos limpios en el orden correcto para respetar las claves foráneas: primero Fuentes, Clientes y Productos, y finalmente Opiniones.

## 3. Resultados y Evidencias

<img width="1366" height="768" alt="Tabla Opiniones" src="https://github.com/user-attachments/assets/c49dff21-6a2d-40b7-ac60-9c3320adad2b" />
<img width="1366" height="768" alt="Tabla Fuentes" src="https://github.com/user-attachments/assets/5163bae8-27d1-4a9f-aba5-67eb3f4580c8" />
<img width="1366" height="768" alt="Tabla Clientes" src="https://github.com/user-attachments/assets/42aa8a5d-29b1-44f9-9cf9-3bc9bf548cd8" />
<img width="1366" height="768" alt="cantidad de registros subida " src="https://github.com/user-attachments/assets/8b07e6ff-6d7c-4dae-858a-ea7aaf750b39" />
<img width="1366" height="768" alt="Tabla Productos" src="https://github.com/user-attachments/assets/ed7306d9-4d5e-4b05-8b8c-cbdc724862de" />
