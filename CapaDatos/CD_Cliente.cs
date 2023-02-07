using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Cliente
    {

        public int Registrar(Cliente obj, out string Mensaje)
        {
            int idautogenerado = 0; // Almacenar el i del Cliente que se registre
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oconexion); // llamando al procedimiento almacenado
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado; // Devolvera el ID del nuevo Cliente registrado
        }

        public List<Cliente> Listar(Convert convert)
        {
            List<Cliente> lista = new List<Cliente>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    // Nuestra Consulta
                    string query = "select IdCliente,Nombres,Apellidos,Correo,Clave,Reestablecer from Cliente";

                    // El command para correr la consulta
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Abrimos la conexion para realizar la consulta
                    oconexion.Open();

                    // Usamos el Reader para leer el resultado de nuestra consultas
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Mientras recibe los resultados
                        while (dr.Read())
                        {
                            lista.Add(
                                new Cliente()
                                {
                                    IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Reestablecer = Convert.ToBoolean(dr["Reestablecer"])
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Cliente>();
            }

            return lista;

        }


        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update cliente set clave = @nuevaclave, reestablecer = 0 where IdCliente = @id", oconexion);

                    cmd.Parameters.AddWithValue("@id", idusuario); // Parametro valor 
                    cmd.Parameters.AddWithValue("@nuevaclave", nuevaclave); // Parametro valor 
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open(); // abrimos la conexion
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false; // si hay filas afectadas
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }




    }
}
