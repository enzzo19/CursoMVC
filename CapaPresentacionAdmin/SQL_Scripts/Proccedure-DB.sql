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