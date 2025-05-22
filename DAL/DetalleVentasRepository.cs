using ENTITY;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class DetalleVentasRepository : ConexionBD
    {
        public List<DetallesVentas> Consultar()
        {
            List<DetallesVentas> lista = new List<DetallesVentas>();
            string sentencia = @"
        SELECT idventa, id_producto, nombre_producto, cantidadlibras, preciolibra, totalventa,
               nombrecliente, apellidocliente, id_factura, fecha_factura, total, monto, vueltos,
               id_metodopago, metododepago
        FROM postgres.""FullGestion"".vistaventas 
        ORDER BY idventa";

            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);
            AbrirConexion();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new DetallesVentas
                {
                    IDVenta = reader.GetInt32(reader.GetOrdinal("idventa")),
                    //IDProducto = reader.GetInt32(reader.GetOrdinal("id_producto")),
                    NombreProducto = reader.GetString(reader.GetOrdinal("nombre_producto")),
                    CantidadLibras = reader.GetInt32(reader.GetOrdinal("cantidadlibras")),
                    PrecioLibra = reader.GetDecimal(reader.GetOrdinal("preciolibra")),
                    TotalVenta = reader.GetDecimal(reader.GetOrdinal("totalventa")),
                    NombreCliente = reader.GetString(reader.GetOrdinal("nombrecliente")),
                    ApellidoCliente = reader.GetString(reader.GetOrdinal("apellidocliente")),
                    IDFactura = reader.GetInt32(reader.GetOrdinal("id_factura")),
                    FechaFactura = reader.GetDateTime(reader.GetOrdinal("fecha_factura")),
                    //Total = reader.GetDecimal(reader.GetOrdinal("total")),
                    Monto = reader.GetDecimal(reader.GetOrdinal("monto")),
                    Vueltos = reader.GetDecimal(reader.GetOrdinal("vueltos")),
                    //IDMetodoPago = reader.GetInt32(reader.GetOrdinal("id_metodopago")),
                    MetodoDePago = reader.GetString(reader.GetOrdinal("metododepago"))
                });
            }

            CerrarConexion();
            return lista;
        }

    }
}
