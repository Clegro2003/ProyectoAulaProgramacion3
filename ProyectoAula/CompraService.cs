﻿using DAL;
using ENTITY;
using System;
using System.Collections.Generic;

namespace ProyectoAula
{
    public class CompraService
    {
        CompraRepository compraRepo;
        public CompraService()
        {
            compraRepo = new CompraRepository();

        }

        public List<Compra> ObtenerCompras()
        {
            return compraRepo.ObtenerCompras();
        }

        public bool InsertarCompra(Compra compra)
        {
            return compraRepo.InsertarCompra(compra);
        }
    }
}
