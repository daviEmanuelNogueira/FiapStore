using FiapStore.DTO;
using FiapStore.Entity;
using FiapStore.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FiapStore.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet("obter-todos-usuarios")]
    public IActionResult ObterTodosUsuarios()
    {
        return Ok(_usuarioRepository.ObterTodos());
    }
    
    [HttpGet("obter-usuario/{id}")]
    public IActionResult ObterUsuario(int id)
    {
        return Ok(_usuarioRepository.ObterPorId(id));
    }

    [HttpPost]
    public IActionResult CadastrarUsuario(CadastrarUsuarioDTO usuarioDto)
    {
        _usuarioRepository.Cadastrar(new Usuario(usuarioDto));
        return Ok("Usuário cadastrado com sucesso");
    }

    [HttpPut]
    public IActionResult AlterarUsuario(AlterarUsuarioDTO usuarioDto)
    {
        _usuarioRepository.Alterar(new Usuario(usuarioDto));
        return Ok("Usuário alterado com sucesso");
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarUsuario(int id)
    {
        _usuarioRepository.Deletar(id);
        return Ok("Usuário deletado com sucesso");
    }

    [HttpGet("obter-todos-com-pedidos/{id}")]
    public IActionResult ObterTodosComPedidos(int id)
    {
        return Ok(_usuarioRepository.ObterComPedidos(id));
    }
}
