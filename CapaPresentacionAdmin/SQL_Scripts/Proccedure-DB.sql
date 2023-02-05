use DBCARRITO

select * from USUARIO

-- Listar los Procedimientos almacenados de mi DBB
SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE ROUTINE_TYPE = 'PROCEDURE'
   ORDER BY ROUTINE_NAME 

-- Prodecimiento Almacenado para Registrar Usuario
create procedure sp_RegistrarUsuario(
	@Nombres varchar(100),
	@Apellidos varchar(100),
	@Correo varchar(100),
	@Clave varchar(100),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
as
begin
	SET @Resultado = 0 
	IF NOT EXISTS (SELECT * FROM USUARIO WHERE Correo = @Correo) -- Que no se repita el correo
	begin
		insert into USUARIO(Nombres, Apellidos, Correo, Clave, Activo) values
		(@Nombres, @Apellidos, @Correo, @Clave, @Activo)

		SET @Resultado = SCOPE_IDENTITY() -- Devuelve el nuevo ID y lo pone en reultado
	end
	else
		set @Mensaje = 'El correo del usuario ya existe'
end
-- Procedimiento Almacenado para Editar Usuario

create procedure sp_EditarUsuario(
@IdUsuario int,
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado bit output
)
as
begin
	SET @Resultado = 0 
	IF NOT EXISTS (SELECT * FROM USUARIO WHERE Correo = @Correo and IdUsuario != @IdUsuario) -- No editar un usuario ya existente
	begin
		update top(1) USUARIO set
		Nombres = @Nombres,
		Apellidos = @Apellidos,
		Correo = @Correo,
		Activo = @Activo
		where IdUsuario = @IdUsuario

		SET @Resultado = 1
	end
	else
		set @Mensaje = 'El correo del usuario ya existe'
end

-- Procedimiento Almacenado para Registrar Categoria
create procedure sp_RegistrarCategoria(
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0 
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion) -- Que no se repita la categoria
	begin
		insert into CATEGORIA(Descripcion, Activo) values
		(@Descripcion, @Activo)

		SET @Resultado = SCOPE_IDENTITY() -- Devuelve el nuevo ID y lo pone en reultado
	end
	else
		set @Mensaje = 'La categoria ya existe'
end

select * from CATEGORIA

-- Procedimiento Almacenado para Editar Categoria
create procedure sp_EditarCategoria(
@IdCategoria int,
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado bit output
)
as
begin
	SET @Resultado = 0 
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion and IdCategoria != @IdCategoria) -- No editar una categoria ya existente
	begin
		update top(1) CATEGORIA set
		Descripcion = @Descripcion,
		Activo = @Activo
		where IdCategoria = @IdCategoria

		SET @Resultado = 1
	end
	else
		set @Mensaje = 'La categoria ya existe'
end

-- Procedimiento Almacenado para Eliminar Categoria
create procedure sp_EliminarCategoria(
@IdCategoria int,
@Mensaje varchar(500) output,
@Resultado bit output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM PRODUCTO p
	inner join CATEGORIA c on c.IdCategoria = p.IdCategoria
	where p.IdCategoria = @IdCategoria) -- Que no exista un producto con esa categoria
	begin
		delete top(1) from CATEGORIA where IdCategoria = @IdCategoria
		SET @Resultado = 1
	end
	else
		set @Mensaje = 'No se puede eliminar la categoria porque tiene productos asociados'
end

select * from categoria;

-- Procedimiento Almacenados para Marcas
create procedure sp_RegistrarMarca(
	@Descripcion varchar(100),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
as
begin
	SET @Resultado = 0 
	IF NOT EXISTS (SELECT * FROM MARCA WHERE Descripcion = @Descripcion) -- Que no se repita la marca
	begin
		insert into MARCA(Descripcion, Activo) values
		(@Descripcion, @Activo)

		SET @Resultado = SCOPE_IDENTITY() -- Devuelve el nuevo ID y lo pone en reultado
	end
	else
		set @Mensaje = 'La marca ya existe'
end


create procedure sp_EditarMarca(
	@IdMarca int,
	@Descripcion varchar(100),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
as
begin
	SET @Resultado = 0 
	IF NOT EXISTS (SELECT * FROM MARCA WHERE Descripcion = @Descripcion and IdMarca != @IdMarca) -- No editar una marca ya existente
	begin
		update top(1) MARCA set
		Descripcion = @Descripcion,
		Activo = @Activo
		where IdMarca = @IdMarca

		SET @Resultado = 1
	end
	else
		set @Mensaje = 'La marca ya existe'
end

create procedure sp_EliminarMarca(
	@IdMarca int,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM PRODUCTO p
	inner join MARCA m on m.IdMarca = p.IdMarca
	where p.IdMarca = @IdMarca) -- Que no exista un producto con esa marca
	begin
		delete top(1) from MARCA where IdMarca = @IdMarca
		SET @Resultado = 1
	end
	else
		set @Mensaje = 'No se puede eliminar la marca porque tiene productos asociados'
end

select * from producto;


-- Procedimiento Almacenado para Productos
create procedure sp_RegistrarProducto(
	@Nombre varchar(100),
	@Descripcion varchar(100),
	@IdMarca varchar(100),
	@IdCategoria varchar(100),
	@Precio decimal(10,2),
	@Stock int,
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
as
begin
	SET @Resultado = 0 
	IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE Nombre = @Nombre) -- Que no se repita el producto
	begin
		insert into PRODUCTO(Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock, Activo) values
		(@Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio, @Stock, @Activo)

		SET @Resultado = SCOPE_IDENTITY() -- Devuelve el nuevo ID y lo pone en reultado
	end
	else
		set @Mensaje = 'El producto ya existe'
end

create procedure sp_EditarProducto(
	@IdProducto int,
	@Nombre varchar(100),
	@Descripcion varchar(100),
	@IdMarca varchar(100),
	@IdCategoria varchar(100),
	@Precio decimal(10,2),
	@Stock int,
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
as
begin
	SET @Resultado = 0 
	IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE Nombre = @Nombre and IdProducto != @IdProducto) -- No editar un producto ya existente
	begin
		update top(1) PRODUCTO set
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		IdMarca = @IdMarca,
		IdCategoria = @IdCategoria,
		Precio = @Precio,
		Stock = @Stock,
		Activo = @Activo
		where IdProducto = @IdProducto

		SET @Resultado = 1
	end
	else
		set @Mensaje = 'El producto ya existe'
end

create procedure sp_EliminarProducto(
	@IdProducto int,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM DETALLE_Venta dv
	inner join PRODUCTO p on p.IdProducto = dv.IdProducto
	where dv.IdProducto = @IdProducto) -- Que no exista un detalle de pedido con ese producto
	begin
		delete top(1) from PRODUCTO where IdProducto = @IdProducto
		SET @Resultado = 1
	end
	else
		set @Mensaje = 'No se puede eliminar el producto porque tiene pedidos asociados'
end

select * from DETALLE_VENTA;

-- Consulta Usada en la Capa de Datos de Producto en Listar
select p.IdProducto, p.Nombre, p.Descripcion,
m.IdMarca, m.Descripcion[DesMarca],
c.IdCategoria, c.Descripcion[DesMarca],
p.Precio, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo
from PRODUCTO p 
inner join MARCA m on m.IdMarca = p.IdMarca
inner join Categoria c on c.IdCategoria = p.IdCategoria;


select * from producto


-- Procedimiento Almacenado para Obtener el Total de Clientes, Ventas y Productos


create procedure sp_Reporte_Dashboard
as
begin

	select
	(select count(*) from CLIENTE) [TotalCliente],

	(select isnull(sum(cantidad), 0) from DETALLE_VENTA) [TotalVenta],

	(select count(*) from PRODUCTO) [TotalProducto]
end

exec sp_Reporte_Dashboard