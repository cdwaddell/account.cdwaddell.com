using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Account.CdWaddell.Models.Client
{
    public class ScopesViewModel
    {
        public ApiResourceViewModel[] ApiResources { get; set; }
        public ScopeListViewModel IdentityResources { get; set; }
    }

    public class ScopeListViewModel:List<ScopeViewModel>
    {
        public ScopeType ScopeType { get; set; }
        public ScopeListViewModel(IEnumerable<ScopeViewModel> scopes):base(scopes){ }
    }

    public enum ScopeType
    {
        Identity,
        Api,
        ApiResource
    }

    public class SecretInputViewModel
    {
        [Required]
        public string ScopeName { get; set; }
        [Required]
        public string Secret { get; set; }
    }

    public class ScopeInputViewModel
    {
        [Required]
        public string ScopeName { get; set; }
        [Required]
        public ScopeType ScopeType { get; set; }
    }

    public class ScopeViewModel: ScopeInputViewModel
    {
        public string[] ClaimNames { get; set; }
    }

    public class ScopeClaimInputViewModel
    {
        [Required]
        public string ScopeName { get; set; }
        [Required]
        public ScopeType ScopeType { get; set; }
        [Required]
        public string ClaimName { get; set; }
    }

    public class ApiResourceViewModel
    {
        public ScopeViewModel ApiResourceScope { get; set; }
        public ScopeListViewModel ApiScopes { get; set; }
    }
}