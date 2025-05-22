using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAula
{
    public class ProveedorService
    {
        RepositorioProveedores Repositorio;
        public event Action<Proveedor> ProveedorGuardado;
        private List<Proveedor> proveedores;
        public ProveedorService()
        {
            Repositorio = new RepositorioProveedores(Config.Filename_Prove);
            RefrescarLista();
        }
        public void RefrescarLista()
        {
            proveedores = Repositorio.GetAll();
        }
        public string Guardar(Proveedor proveedor)
        {
            var msg = Repositorio.SaveData(proveedor);
            RefrescarLista();
            ProveedorGuardado?.Invoke(proveedor);
            return msg;
        }
        public List<Proveedor> Consultar()
        {
            return proveedores ?? new List<Proveedor>();
        }

        public Proveedor BuscarId(string id)
        {
            var prove = proveedores.FirstOrDefault<Proveedor>(x => x.IDProveedor == id);
            return prove;
        }

        public string Actualizar(Proveedor proveedor)
        {
            var proveedorExistente = proveedores.FirstOrDefault<Proveedor>(x => x.IDProveedor == proveedor.IDProveedor);
            if (proveedorExistente != null)
            {
                proveedorExistente.Nombre = proveedor.Nombre;
                proveedorExistente.Telefono = proveedor.Telefono;
                proveedorExistente.Email = proveedor.Email;
                proveedorExistente.TipoID = proveedor.TipoID;
                var msg = Guardar(proveedorExistente);
                return "Provedor modificado";
            }
            return "Proveedor no encontrado";
        }

        public string Eliminar(string idProveedor)
        {
            var proveedore = proveedores.FirstOrDefault(x => x.IDProveedor == idProveedor); 
            if (proveedore != null)
            {
                proveedores.Remove(proveedore);
                 RefrescarLista();
                return "Proveedor eliminado.";
            } 

            return "Proveedor no encontrado."; }

            }
}