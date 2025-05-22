using ENTITY;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class VentasRepository : ConexionBD
    {
        public List<Venta> ObtenerVentas()
        {
            List<Venta> Ventas = new List<Venta>();
            string sentencia = "SELECT * FROM postgres.\"FullGestion\".ventas ORDER BY idventa";
            AbrirConexion();
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Venta venta = new Venta(
                    Convert.ToInt32(reader["idventa"]),
                    new Productos() { IDProducto = reader["idproducto"].ToString() },
                    Convert.ToDecimal(reader["cantidadlibras"]),
                    Convert.ToDecimal(reader["preciolibra"]),
                    Convert.ToString(reader["nombrecliente"]),
                    Convert.ToString(reader["apellidocliente"]),
                    new Factura() { IDFactura = Convert.ToInt32(reader["idfactura"]) }
                );
                Ventas.Add(venta);
            }

            CerrarConexion();
            return Ventas;
        }

        public bool Vender(Venta venta, decimal monto, string metodo)
        {
            try
            {
                AbrirConexion();

                using (var cmd = new NpgsqlCommand("pkg_gestionventas.hacerventa", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_id_producto", venta.IDProducto.ToString());
                    cmd.Parameters.AddWithValue("p_cantidad", Convert.ToInt32(venta.Cantidad));
                    cmd.Parameters.AddWithValue("p_nombre_cliente", venta.NombreCliente);
                    cmd.Parameters.AddWithValue("p_apellido_cliente", venta.ApellidoCliente);
                    cmd.Parameters.AddWithValue("p_monto", monto);
                    cmd.Parameters.AddWithValue("p_id_metodo_pago", metodo);

                    cmd.ExecuteNonQuery();
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
