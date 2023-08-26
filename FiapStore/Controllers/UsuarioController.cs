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
    private readonly ILogger<UsuarioController> _logger;
    public UsuarioController(IUsuarioRepository usuarioRepository, ILogger<UsuarioController> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    [HttpGet("obter-todos-usuarios")]
    public IActionResult ObterTodosUsuarios()
    {
        try
        {
            return Ok(_usuarioRepository.ObterTodos());
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, $"{DateTime.Now:yyyy-MM-dd} | Exception forçada: {ex.Message}");
            return BadRequest();
        }
    }

    [HttpGet("obter-usuario/{id}")]
    public IActionResult ObterUsuario(int id)
    {
        _logger.LogInformation("Executando método ObterPorId");
        return Ok(_usuarioRepository.ObterPorId(id));
    }

    [HttpPost]
    public IActionResult CadastrarUsuario(CadastrarUsuarioDTO usuarioDto)
    {
        _usuarioRepository.Cadastrar(new Usuario(usuarioDto));
        var mensagem = $"Usuário cadastrado com sucesso | Nome: {usuarioDto.Nome}";
        _logger.LogWarning(mensagem);
        return Ok(mensagem);
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
