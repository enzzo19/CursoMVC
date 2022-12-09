
use DBCARRITO
select * from usuario

insert into USUARIO(Nombres, Apellidos, Correo, Clave) values ('test nombre', 'test apellido', 'test@example.com', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae')
insert into USUARIO(Nombres, Apellidos, Correo, Clave) values ('test 02', 'user 02', 'user02@example.com', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae')

select * from CATEGORIA

insert into CATEGORIA(Descripcion) values
('Bebidas'),
('Comestibles'),
('Perfumeria'),
('Limpieza'),
('Golosinas'),
('Fiambres')


select  * from MARCA

insert into MARCA(Descripcion) values
('Nestle'),
('P&G'),
('Gohnson&Gohnson'),
('Unilever'),
('Mars'),
('Kellogs'),
('Pepsico'),
('Coca Cola'),
('KRAFT'),
('PALADINI'),
('LARIO')

select * from DEPARTAMENTO

insert into DEPARTAMENTO(IdDepartamento, Descripcion) values
('01', 'Capital'),
('O2', 'La Caldera'),
('03', 'Cerrillos')

select * from MUNICIPIO

insert into MUNICIPIO(IdMunicipio, Descripcion, IdDepartamento) values
-- Municipios de Capital
('0101', 'Salta', '01'),
('0102', 'Villa San Lorenzo', '02'),
-- Municipios de La Caldera
('0201', 'La Caldera', '01'),
('0201', 'Vaqueros', '02'),
-- Municipios de Cerrillos
('0301', 'Cerrillos', '01'),
('0302', 'La Merced', '02')


select * from LOCALIDAD

insert into LOCALIDAD(IdLocalidad, Descripcion, IdMunicipio, IdDepartamento) values
-- Localidad de Salta
('010101', 'Gral Alvarado', '0101', '01'),
-- Localidad de Villa San Lorenzo
('010201', 'La Troja', '0102', '01'),
-- Localidad de La Caldera
('020101', 'La Calderilla', '0201', '02'),
-- Localidad de La Merced
('030201', 'Sumalao', '0302', '03')

select * from USUARIO
-- Cambio el valor en una columna para poedr visualizarlo mejor
update USUARIO set Activo = 0 where IdUsuario = 2