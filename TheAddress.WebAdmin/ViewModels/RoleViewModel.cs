using System.ComponentModel;

namespace TheAddress.WebAdmin.ViewModels
{
    public class RoleViewModel
    {
        public string? Id { get; set; }

        [DisplayName("Role adı")]
        public string Name { get; set; }
    }
}

