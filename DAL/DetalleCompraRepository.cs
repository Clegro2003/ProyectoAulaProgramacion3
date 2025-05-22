using ENTITY;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class DetalleCompraRepository : ConexionBD
    {
        public List<DetalleCompras> Consultar()
        {
            List<DetalleCompras> lista = new List<DetalleCompras>();
            string sentencia = @"SELECT idcompra, idproveedor, nombre1, apellido1, nombreproducto, cantidadcajas, 
                                        preciocaja, totalcompra, metododepago 
                                 FROM postgres.""FullGestion"".vistacompras 
                                 ORDER BY idcompra";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);
            AbrirConexion();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new DetalleCompras
                {
                    NumeroCompra = reader.GetInt32(reader.GetOrdinal("idcompra")),
                    IDProveedor = reader.GetInt32(reader.GetOrdinal("idproveedor")),
                    NombreProveedor = reader.GetString(reader.GetOrdinal("nombre1")),
                    ApellidoProveedor = reader.GetString(reader.GetOrdinal("apellido1")),
                    NombreProducto = reader.GetString(reader.GetOrdinal("nombreproducto")),
                    Cantidad = reader.GetInt32(reader.GetOrdinal("cantidadcajas")),
                    Precio = reader.GetDecimal(reader.GetOrdinal("preciocaja")),
                    Total = reader.GetDecimal(reader.GetOrdinal("totalcompra")),
                    MetodoPago = reader.GetString(reader.GetOrdinal("metododepago"))
                });
            }

            CerrarConexion();
            return lista;
        }
    }
}
