
-- Create the BDD
create database DBCARRITO

GO
-- Use the BDD
USE DBCARRITO

GO

-- Create tables on database

-- CATEGORIA
create table  CATEGORIA(
IdCategoria int primary key identity,
Descripcion varchar(100),
Activo bit default 1,
FechaRegistro datetime default getdate()
)

GO

-- MARCA
create table MARCA(
IdMarca int primary key identity,
Descripcion varchar(100),
Activo bit default 1,
FechaRegistro datetime default getdate()
)

GO

-- PRODUCTO
create table PRODUCTO(
IdProducto int primary key identity,
Nombre varchar (500),
Descripcion varchar(500),
IdMarca int references Marca(IdMarca),
IdCategoria int references Categoria(IdCategoria),
Precio decimal(10,2) default 0,
Stock int,
RutaImagen varchar(100),
NombreImagen varchar(100),
Activo bit default 1,
FechaRegistro datetime default getdate()
)

GO

-- CLIENTE
create table CLIENTE(
IdCliente int primary key identity,
Nombres varchar(100),
Apellidos varchar(100),
Correo varchar(100),
Clave varchar(150),
Reestablecer bit default 0,
FechaRegistro datetime default getdate()
)

GO

-- CARRITO
create table CARRITO(
IdCarrito int primary key identity,
IdCliente int references CLIENTE(IdCliente),
IdProducto int references PRODUCTO(IdProducto),
Cantidad int
)

GO

-- VENTA
create table VENTA(
IdVenta int primary key identity,
IdCliete int references CLIENTE(IdCliente),
TotalProducto int,
MontoTotal decimal(10,2),
Contacto varchar(50),
IdDistrito varchar(10),
Telefono varchar(50),
Direccion varchar(500),
IdTransaccion varchar(50),
FechaVenta datetime default getdate()
)

GO

-- DETALLEVENTA
create table DETALLE_VENTA(
IdDetalleVenta int primary key identity,
IdVenta int references VENTA(IdVenta),
IdProducto int references PRODUCTO(IdProducto),
Cantidad int,
Total decimal(10,2)
)

GO

-- USUARIO
create table USUARIO(
IdUsuario int primary key identity,
Nombres varchar(100),
Apellidos varchar(100),
Correo varchar(100),
Clave varchar(150),
Reestablecer bit default 1,
Activo bit default 1,
FechaRegistro datetime default getdate()
)

GO

-- Ubicacion Cliente/Usuario
-- DEPARTAMENTO
create table DEPARTAMENTO(
IdDepartamento varchar(2) NOT NULL,
Descripcion varchar(45) NOT NULL
)

GO

-- PROVINCIA
create table MUNICIPIO(
IdMunicipio varchar(4) NOT NULL,
Descripcion varchar(45) NOT NULL,
IdDepartamento varchar(2) NOT NULL
)

GO

-- DISTRITO
create table LOCALIDAD(
IdLocalidad varchar(6) NOT NULL,
Descripcion varchar(45) NOT NULL,
IdMunicipio varchar(4) NOT NULL,
IdDepartamento varchar(2) NOT NULL
)

GO