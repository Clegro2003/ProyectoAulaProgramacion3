using ENTITY;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class TipoIDRepository : ConexionBD
    {
        public List<TipoID> ObtenerTipos()
        {
            List<TipoID> Tipos = new List<TipoID>();
            string sentencia = "SELECT idtipo, nombre FROM postgres.\"FullGestion\".identificaciones";
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conexion);

            AbrirConexion();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Tipos.Add(new TipoID(reader.GetInt32(reader.GetOrdinal("idtipo")).ToString(), reader.GetString(1)));
            }

            CerrarConexion();
            return Tipos;
        }
    }
}
