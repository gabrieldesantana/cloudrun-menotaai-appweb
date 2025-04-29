using MeNotaAi.Domain.Entities;
using MeNotaAi.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace MeNotaAi.Web.Pages.Visitantes;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IVisitanteService _visitanteService;

    public IndexModel(ILogger<IndexModel> logger, IVisitanteService visitanteService)
    {
        _logger = logger;
        _visitanteService = visitanteService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Response = await _visitanteService.SelectAllAsync();
        return Page();
    }

    public IEnumerable<VisitanteViewModel>? Response { get; set; } = [];

    [BindProperty]
    public VisitanteInputModel VisitanteModel { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(VisitanteModel.Nome))
            return RedirectToPage("./Index");

        if (Regex.IsMatch(VisitanteModel.Nome,
            @"\b(SELECT|INSERT|UPDATE|DELETE|DROP|ALTER|CREATE|EXEC|UNION|WHERE|FROM|JOIN)\b", RegexOptions.IgnoreCase))
        {
            TempData["MensagemErro"] = "Hoje não, Faro :)";
            return RedirectToPage("./Index");
        }


        if (!Regex.IsMatch(VisitanteModel.Nome, @"^[\p{L}\s]+$"))
        {
            TempData["MensagemErro"] = "Por favor,insira um nome válido :)";
            return RedirectToPage("./Index");
        }
            

        var visitantes = await _visitanteService.SelectAllAsync();

        if (visitantes.Any(v => v.Nome == VisitanteModel.Nome))
        {
            TempData["MensagemErro"] = "Poxa, esse nome já foi inserido :(";
            return RedirectToPage("./Index");
        }

        await _visitanteService.CreateAsync(VisitanteModel);
        TempData["MensagemSucesso"] = $"Obrigado(a) {VisitanteModel.Nome}";
        return RedirectToPage("./Index");
    }
}
