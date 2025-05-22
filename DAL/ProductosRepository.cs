using ENTITY;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class ProductosRepository : ConexionBD
    {
        public List<Productos> ObtenerProductos()
        {
            List<Productos> productos = new List<Productos>();

            string sentencia = @"SELECT id_producto, idcategoria, nombre_producto, precio_venta, stock 
                                 FROM postgres.""FullGestion"".productos ORDER BY id_producto";

            AbrirConexion();
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            NpgsqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                productos.Add(new Productos
                {
                    IDProducto = reader.GetInt32(reader.GetOrdinal("id_producto")).ToString(),
                    IDCategoria = new Categoria(reader.GetInt32(reader.GetOrdinal("idcategoria")).ToString(), ""),
                    NombreProducto = reader.GetString(reader.GetOrdinal("nombre_producto")),
                    PrecioVenta = reader.GetDecimal(reader.GetOrdinal("precio_venta")),
                    Cantidad = reader.GetDecimal(reader.GetOrdinal("stock"))
                });
            }

            CerrarConexion();
            return productos;
        }

        public bool GuardarProducto(Productos producto)
        {
            string sentencia = @"INSERT INTO postgres.""FullGestion"".productos (
                                     nombre_producto, precio_venta, stock, precio_compra, id_categoria
                                 ) VALUES (
                                     @nombre, 0, 0, 0, @id_categoria
                                 )";

            try
            {
                AbrirConexion();
                using (NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", producto.NombreProducto);
                    comando.Parameters.AddWithValue("@id_categoria", int.Parse(producto.IDCategoria.IDCategoria));

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

        public bool ProductoExiste(string idproducto)
        {
            string sentencia = @"SELECT COUNT(*) FROM postgres.""FullGestion"".productos WHERE id_producto = @id";

            try
            {
                AbrirConexion();
                using (NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion))
                {
                    comando.Parameters.AddWithValue("@id", int.Parse(idproducto));
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

        public bool ModificarProducto(Productos producto)
        {
            string sentencia = @"UPDATE postgres.""FullGestion"".productos 
                                 SET nombre_producto = @nombre,
                                     precio_venta = @precio,
                                     stock = @stock,
                                     id_categoria = @id_categoria
                                 WHERE id_producto = @id_producto";

            try
            {
                AbrirConexion();
                using (NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", producto.NombreProducto);
                    comando.Parameters.AddWithValue("@precio", producto.PrecioVenta);
                    comando.Parameters.AddWithValue("@stock", producto.Cantidad);
                    comando.Parameters.AddWithValue("@id_categoria", int.Parse(producto.IDCategoria.IDCategoria));
                    comando.Parameters.AddWithValue("@id_producto", int.Parse(producto.IDProducto));

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

        public bool EliminarProducto(string idproducto)
        {
            string sentencia = @"DELETE FROM postgres.""FullGestion"".productos WHERE id_producto = @id";

            try
            {
                AbrirConexion();
                using (NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion))
                {
                    comando.Parameters.AddWithValue("@id", int.Parse(idproducto));
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
