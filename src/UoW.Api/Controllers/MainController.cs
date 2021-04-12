using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UoW.Api.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        private readonly List<string> _notificacoes;

        protected MainController()
        {
            _notificacoes = new List<string>();
        }

        protected bool ValidOperation()
        {
            return !_notificacoes.Any();
        }

        protected void NotifyError(string mensagem)
        {
            _notificacoes.Add(mensagem);
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificacoes,
            });
        }

        protected ActionResult CustomResponse(Exception ex)
        {
            NotifyError(ex.ToString());

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyInvalidModelErrors(modelState);

            return CustomResponse();
        }

        protected void NotifyInvalidModelErrors(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(errorMsg);
            }
        }
    }
}