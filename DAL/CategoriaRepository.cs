using ENTITY;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class CategoriaRepository : ConexionBD
    {
        public List<Categoria> ObtenerCategorias()
        {
            List<Categoria> Categorias = new List<Categoria>();
            string Sentencia = "SELECT * FROM postgres.\"FullGestion\".Categorias";
            AbrirConexion();
            NpgsqlCommand Comando = new NpgsqlCommand(Sentencia, conexion);
            NpgsqlDataReader reader = Comando.ExecuteReader();

            while (reader.Read())
            {
                Categorias.Add(new Categoria(
                    idcategoria: reader.GetInt32(reader.GetOrdinal("idcategoria")).ToString(),
                    nombrecategoria: reader.GetString(reader.GetOrdinal("nombrecategoria"))
                ));
            }

            CerrarConexion();
            return Categorias;
        }

        public bool GuardarCategoria(Categoria categoria)
        {
            try
            {
                AbrirConexion();

                using (NpgsqlCommand Comando = new NpgsqlCommand("INSERT INTO postgres.\"FullGestion\".categorias(idcategoria, nombrecategoria) VALUES (@IDCategoria, @NombreCategoria)", conexion))
                {
                    Comando.CommandType = CommandType.Text;
                    Comando.Parameters.AddWithValue("@idcategoria", categoria.IDCategoria);
                    Comando.Parameters.AddWithValue("@nombrecategoria", categoria.NombreCategoria);

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

        public bool CategoriaExiste(string idcategoria)
        {
            try
            {
                AbrirConexion();
                using (NpgsqlCommand comando = new NpgsqlCommand("SELECT COUNT(*) FROM postgres.\"FullGestion\".categorias WHERE idcategoria = @p_id_categoria", conexion))
                {
                    comando.Parameters.AddWithValue("@p_id_categoria", idcategoria);
                    int count = Convert.ToInt32(comando.ExecuteScalar());
                    return count > 0;
                }
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

        public bool ModificarCategoria(Categoria categoria)
        {
            try
            {
                AbrirConexion();

                using (NpgsqlCommand Comando = new NpgsqlCommand("UPDATE postgres.\"FullGestion\".categorias SET nombreCategoria = @NombreCategoria WHERE idcategoria = @IDCategoria", conexion))
                {
                    Comando.Parameters.AddWithValue("@nombrecategoria", categoria.NombreCategoria);
                    Comando.Parameters.AddWithValue("@idcategoria", categoria.IDCategoria);

                    int filasAfectadas = Comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
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

        public bool Eliminar(string idcategoria)
        {
            try
            {
                AbrirConexion();

                using (NpgsqlCommand Comando = new NpgsqlCommand("DELETE FROM postgres.\"FullGestion\".Categorias WHERE idcategoria = @IDCategoria", conexion))
                {
                    Comando.Parameters.AddWithValue("@idcategoria", idcategoria);

                    Comando.ExecuteNonQuery();
                    return true;
                }
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
