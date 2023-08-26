﻿using FiapStore.Entity;
using FiapStore.Interface;
using Microsoft.EntityFrameworkCore;

namespace FiapStore.Repository
{
    public class ERUsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public ERUsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Usuario ObterComPedidos(int id)
        {
            return _context.Usuario
                .Include(u => u.Pedidos)
                .Where(u => u.Id == id)
                .ToList()
                .Select(usuario =>
                {
                    usuario.Pedidos = usuario.Pedidos.Select(pedido => new Pedido(pedido)).ToList();
                    return usuario;
                }).FirstOrDefault();
        }
    }
}