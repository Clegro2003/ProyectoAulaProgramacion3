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
                SELECT idventa, nombreproducto, cantidadlibras, preciolibra, totalventa, monto, vueltos,
                       nombrecliente, apellidocliente, fechafactura, metododepago, idfactura
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
                    NombreProducto = reader.GetString(reader.GetOrdinal("nombreproducto")),
                    CantidadLibras = reader.GetInt32(reader.GetOrdinal("cantidadlibras")),
                    PrecioLibra = reader.GetDecimal(reader.GetOrdinal("preciolibra")),
                    TotalVenta = reader.GetDecimal(reader.GetOrdinal("totalventa")),
                    Monto = reader.GetDecimal(reader.GetOrdinal("monto")),
                    Vueltos = reader.GetDecimal(reader.GetOrdinal("vueltos")),
                    NombreCliente = reader.GetString(reader.GetOrdinal("nombrecliente")),
                    ApellidoCliente = reader.GetString(reader.GetOrdinal("apellidocliente")),
                    FechaFactura = reader.GetDateTime(reader.GetOrdinal("fechafactura")),
                    MetodoDePago = reader.GetString(reader.GetOrdinal("metododepago")),
                    IDFactura = reader.GetInt32(reader.GetOrdinal("idfactura"))
                });
            }

            CerrarConexion();
            return lista;
        }
    }
}
