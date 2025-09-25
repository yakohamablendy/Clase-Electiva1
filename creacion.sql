USE MiProyectoETL;
GO

IF OBJECT_ID('Opiniones', 'U') IS NOT NULL DROP TABLE Opiniones;
IF OBJECT_ID('Clientes', 'U') IS NOT NULL DROP TABLE Clientes;
IF OBJECT_ID('Productos', 'U') IS NOT NULL DROP TABLE Productos;
IF OBJECT_ID('Fuentes', 'U') IS NOT NULL DROP TABLE Fuentes;
GO

CREATE TABLE Fuentes (
    id_fuente INT PRIMARY KEY,
    nombre_fuente VARCHAR(100) NOT NULL
);

CREATE TABLE Clientes (
    id_cliente INT PRIMARY KEY,
    nombre_cliente VARCHAR(150) NOT NULL,
    email VARCHAR(150) UNIQUE
);

CREATE TABLE Productos (
    id_producto INT PRIMARY KEY,
    nombre_producto VARCHAR(200) NOT NULL,
    categoria VARCHAR(100)
);

CREATE TABLE Opiniones (
    id_opinion INT PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_producto INT NOT NULL,
    id_fuente INT NOT NULL,
    fecha DATETIME,
    texto_opinion VARCHAR(MAX),
    calificacion INT,
    FOREIGN KEY (id_cliente) REFERENCES Clientes(id_cliente),
    FOREIGN KEY (id_producto) REFERENCES Productos(id_producto),
    FOREIGN KEY (id_fuente) REFERENCES Fuentes(id_fuente)
);
GO