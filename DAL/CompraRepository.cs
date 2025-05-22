using ENTITY;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class CompraRepository : ConexionBD
    {
        public List<Compra> ObtenerCompras()
        {
            string sentencia = @"SELECT * FROM postgres.""FullGestion"".compras ORDER BY id_compra";
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);
            AbrirConexion();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            List<Compra> lista = new List<Compra>();
            while (reader.Read())
            {
                Compra compra = new Compra(
                    reader.GetInt32(reader.GetOrdinal("id_compra")),
                    new Proveedor { IDProveedor = reader.GetInt32(reader.GetOrdinal("id_proveedor")) },
                    new Productos { IDProducto = reader.GetInt32(reader.GetOrdinal("id_producto")).ToString() },
                    reader.GetInt32(reader.GetOrdinal("cantidad_cajas")),
                    reader.GetDecimal(reader.GetOrdinal("precio_compra")),
                    reader.GetDecimal(reader.GetOrdinal("total_compra")),
                    reader.GetDateTime(reader.GetOrdinal("fecha_compra")),
                    new MetodoPagos { IDPago = reader.GetInt32(reader.GetOrdinal("id_metodopago")).ToString() }
                );
                lista.Add(compra);
            }

            CerrarConexion();
            return lista;
        }

        public bool InsertarCompra(Compra compra)
        {
            try
            {
                AbrirConexion();

                using (NpgsqlCommand Comando = new NpgsqlCommand("CALL insertar_compra(@p_id_proveedor, @p_id_producto, @p_cantidad_cajas, @p_precio_caja, @p_fecha_compra, @p_pago_id)", conexion))
                {
                    Comando.CommandType = CommandType.Text;
                    Comando.Parameters.AddWithValue("@p_id_proveedor", compra.IDProveedor);
                    Comando.Parameters.AddWithValue("@p_id_producto", int.Parse(compra.IDProducto));
                    Comando.Parameters.AddWithValue("@p_cantidad_cajas", compra.CantidadCajas);
                    Comando.Parameters.AddWithValue("@p_precio_caja", compra.PrecioCaja);
                    Comando.Parameters.AddWithValue("@p_fecha_compra", compra.FechaCompra);
                    Comando.Parameters.AddWithValue("@p_pago_id", int.Parse(compra.Pagoid));

                    Comando.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception)
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
