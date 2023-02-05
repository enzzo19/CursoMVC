using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencia de acceso a nuestra Capa de Entidad
using CapaEntidad;
// Referencia de acceso a SQL Connection
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public List<Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    
                    // El command para correr la consulta
                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oconexion);

                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.Parameters.AddWithValue("idtransaccion", idtransaccion);

                    cmd.CommandType = CommandType.StoredProcedure;

                    // Abrimos la conexion para realizar la consulta
                    oconexion.Open();

                    // Usamos el Reader para leer el resultado de nuestra consultas
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Mientras recibe los resultados
                        while (dr.Read())
                        {
                            lista.Add(
                                new Reporte()
                                {
                                    FechaVenta = dr["FechaVenta"].ToString(),
                                    Cliente = dr["Cliente"].ToString(),
                                    Producto = dr["Producto"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-PE")),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                    Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-PE")),
                                    IdTransaccion = dr["IdTransaccion"].ToString()
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Reporte>();
            }

            return lista;

        }

        
        public Dashboard VerDashboard()
        {
            Dashboard objeto = new Dashboard();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    // El command para correr la consulta
                    SqlCommand cmd = new SqlCommand("sp_Reporte_Dashboard", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Abrimos la conexion para realizar la consulta
                    oconexion.Open();

                    // Usamos el Reader para leer el resultado de nuestra consultas
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Mientras recibe los resultados
                        while (dr.Read())
                        {
                            objeto = new Dashboard()
                            {
                                TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"])
                            };
                             
                        }
                    }
                }
            }
            catch
            {
                objeto = new Dashboard();
            }
            
            return objeto;

        }


    }
}
