using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.ViewModels
{
    public class ConfirmEmailViewModel
    {
        public bool IsSuccessful { get; set; }
        [Url]
        public string? ReturnUrl{get;set;}
    }
}