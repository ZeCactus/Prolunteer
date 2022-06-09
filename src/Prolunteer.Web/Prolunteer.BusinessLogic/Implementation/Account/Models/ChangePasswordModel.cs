using System;
using System.ComponentModel.DataAnnotations;

namespace Prolunteer.BusinessLogic.Implementation.Account.Models
{
    public class ChangePasswordModel
    {
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPasswordRepeat { get; set; }
    }
}