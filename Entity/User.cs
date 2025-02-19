using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class User
    {
        [Display(Name = "Kullanıcı ID")]
        public int UserId { get; set; }
        [Display(Name = "İsim")]
        public string UserName { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        public string Rol {  get; set; }

        public object FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
