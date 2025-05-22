using ENTITY;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class ProveedorRepository : ConexionBD
    {
        public List<Proveedor> ObtenerProveedores()
        {
            List<Proveedor> proveedores = new List<Proveedor>();
            string sentencia = @"SELECT id_proveedor, idtipo, nombre1, nombre2, apellido1, apellido2, telefono, email 
                                 FROM postgres.""FullGestion"".proveedores ORDER BY id_proveedor";

            AbrirConexion();
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            NpgsqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                proveedores.Add(new Proveedor
                {
                    IDProveedor = reader.GetInt32(reader.GetOrdinal("id_proveedor")),
                    TipoID = new TipoID(reader.GetInt32(reader.GetOrdinal("idtipo")).ToString(), ""),
                    Nombre1 = reader.GetString(reader.GetOrdinal("nombre1")),
                    Nombre2 = reader.IsDBNull(reader.GetOrdinal("nombre2")) ? string.Empty : reader.GetString(reader.GetOrdinal("nombre2")),
                    Apellido1 = reader.GetString(reader.GetOrdinal("apellido1")),
                    Apellido2 = reader.IsDBNull(reader.GetOrdinal("apellido2")) ? string.Empty : reader.GetString(reader.GetOrdinal("apellido2")),
                    Telefono = reader.GetInt64(reader.GetOrdinal("telefono")),
                    Email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString(reader.GetOrdinal("email"))
                });
            }

            CerrarConexion();
            return proveedores;
        }

        public bool GuardarProveedor(Proveedor proveedor)
        {
            string sentencia = @"
                INSERT INTO postgres.""FullGestion"".proveedores (
                    id_proveedor, idtipo, nombre1, nombre2, apellido1, apellido2, telefono, email
                )
                VALUES (
                    @id, @idtipo, @nombre1, @nombre2, @apellido1, @apellido2, @telefono, @correo
                )";

            try
            {
                AbrirConexion();
                using (NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion))
                {
                    comando.Parameters.AddWithValue("@id", proveedor.IDProveedor);
                    comando.Parameters.AddWithValue("@idtipo", int.Parse(proveedor.TipoID.IDTipo));
                    comando.Parameters.AddWithValue("@nombre1", proveedor.Nombre1);
                    comando.Parameters.AddWithValue("@nombre2", (object)proveedor.Nombre2 ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@apellido1", proveedor.Apellido1);
                    comando.Parameters.AddWithValue("@apellido2", (object)proveedor.Apellido2 ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@telefono", proveedor.Telefono);
                    comando.Parameters.AddWithValue("@correo", (object)proveedor.Email ?? DBNull.Value);

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

        public bool ProveedorExiste(int idproveedor)
        {
            string sentencia = @"SELECT COUNT(*) FROM postgres.""FullGestion"".proveedores WHERE id_proveedor = @id";

            try
            {
                AbrirConexion();
                using (NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion))
                {
                    comando.Parameters.AddWithValue("@id", idproveedor);
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

        public bool ModificarProveedor(Proveedor proveedor)
        {
            string sentencia = @"
                UPDATE postgres.""FullGestion"".proveedores SET
                    idtipo = @idtipo,
                    nombre1 = @nombre1,
                    nombre2 = @nombre2,
                    apellido1 = @apellido1,
                    apellido2 = @apellido2,
                    telefono = @telefono,
                    email = @correo
                WHERE id_proveedor = @id";

            try
            {
                AbrirConexion();
                using (NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion))
                {
                    comando.Parameters.AddWithValue("@id", proveedor.IDProveedor);
                    comando.Parameters.AddWithValue("@idtipo", int.Parse(proveedor.TipoID.IDTipo));
                    comando.Parameters.AddWithValue("@nombre1", proveedor.Nombre1);
                    comando.Parameters.AddWithValue("@nombre2", (object)proveedor.Nombre2 ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@apellido1", proveedor.Apellido1);
                    comando.Parameters.AddWithValue("@apellido2", (object)proveedor.Apellido2 ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@telefono", proveedor.Telefono);
                    comando.Parameters.AddWithValue("@correo", (object)proveedor.Email ?? DBNull.Value);

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

        public bool EliminarProveedor(int idproveedor)
        {
            string sentencia = @"DELETE FROM postgres.""FullGestion"".proveedores WHERE id_proveedor = @id";

            try
            {
                AbrirConexion();
                using (NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion))
                {
                    comando.Parameters.AddWithValue("@id", idproveedor);
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
