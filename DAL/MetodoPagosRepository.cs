using ENTITY;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class MetodoPagosRepository : ConexionBD
    {
        public List<MetodoPagos> ObtenerPagos()
        {
            List<MetodoPagos> pagos = new List<MetodoPagos>();
            string sentencia = @"SELECT id_metodopago, nombre_metodopago FROM postgres.""FullGestion"".metodopago";

            AbrirConexion();
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            NpgsqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                pagos.Add(new MetodoPagos(
                    reader.GetInt32(reader.GetOrdinal("id_metodopago")).ToString(),
                    reader.GetString(reader.GetOrdinal("nombre_metodopago"))
                ));
            }

            CerrarConexion();
            return pagos;
        }

        public bool GuardarMetodo(MetodoPagos metodo)
        {
            string sentencia = @"INSERT INTO postgres.""FullGestion"".metodopago(nombre_metodopago) VALUES (@nombre)";

            try
            {
                AbrirConexion();

                using (NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", metodo.NombreMetodo);
                    comando.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }

        public bool MetodoExiste(string idmetodo)
        {
            string sentencia = @"SELECT COUNT(*) FROM postgres.""FullGestion"".metodopago WHERE id_metodopago = @id";

            try
            {
                AbrirConexion();
                using (NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion))
                {
                    comando.Parameters.AddWithValue("@id", int.Parse(idmetodo));
                    int count = Convert.ToInt32(comando.ExecuteScalar());
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }

        public bool ModificarMetodo(MetodoPagos metodo)
        {
            string sentencia = @"UPDATE postgres.""FullGestion"".metodopago SET nombre_metodopago = @nombre WHERE id_metodopago = @id";

            try
            {
                AbrirConexion();

                using (NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", metodo.NombreMetodo);
                    comando.Parameters.AddWithValue("@id", int.Parse(metodo.IDPago));
                    int filas = comando.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }

        public bool Eliminar(string idmetodo)
        {
            string sentencia = @"DELETE FROM postgres.""FullGestion"".metodopago WHERE id_metodopago = @id";

            try
            {
                AbrirConexion();

                using (NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion))
                {
                    comando.Parameters.AddWithValue("@id", int.Parse(idmetodo));
                    comando.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }
    }
}
