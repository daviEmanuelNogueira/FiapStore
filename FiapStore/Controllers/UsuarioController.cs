using FiapStore.DTO;
using FiapStore.Entity;
using FiapStore.Enum;
using FiapStore.Interface;
using Microsoft.AspNetCore.Authorization;
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

    /// <summary>
    /// Obtém todos os usuários armazenados
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Exemplo: 
    /// 
    /// Enviar para requisição</remarks>
    /// <response code="200">retorna sucesso</response>
    /// <response code="401">Não autenticado</response>
    /// <response code="403">Não autorizado</response>
    [Authorize]
    [Authorize(Roles = Permissoes.Administrador)]
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

    /// <summary>
    /// Obtém um usuário por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [Authorize(Roles = $"{Permissoes.Administrador}, {Permissoes.Funcionario}")]
    [HttpGet("obter-usuario/{id}")]
    public IActionResult ObterUsuario(int id)
    {
        _logger.LogInformation("Executando método ObterPorId");
        return Ok(_usuarioRepository.ObterPorId(id));
    }

    /// <summary>
    /// Cadastrar um novo usuário
    /// </summary>
    /// <param name="usuarioDto"></param>
    /// <returns></returns>
    [Authorize]
    [Authorize(Roles = $"{Permissoes.Administrador}, {Permissoes.Funcionario}")]
    [HttpPost]
    public IActionResult CadastrarUsuario(CadastrarUsuarioDTO usuarioDto)
    {
        _usuarioRepository.Cadastrar(new Usuario(usuarioDto));
        var mensagem = $"Usuário cadastrado com sucesso | Nome: {usuarioDto.Nome}";
        _logger.LogWarning(mensagem);
        return Ok(mensagem);
    }

    /// <summary>
    /// Realiza alteração em um usuário 
    /// </summary>
    /// <param name="usuarioDto"></param>
    /// <returns></returns>
    [HttpPut]
    public IActionResult AlterarUsuario(AlterarUsuarioDTO usuarioDto)
    {
        _usuarioRepository.Alterar(new Usuario(usuarioDto));
        return Ok("Usuário alterado com sucesso");
    }

    /// <summary>
    /// Deleta um usuário por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult DeletarUsuario(int id)
    {
        _usuarioRepository.Deletar(id);
        return Ok("Usuário deletado com sucesso");
    }

    /// <summary>
    /// Obtém todos os usuários com pedidos
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("obter-todos-com-pedidos/{id}")]
    public IActionResult ObterTodosComPedidos(int id)
    {
        return Ok(_usuarioRepository.ObterComPedidos(id));
    }
}
