namespace RentHive.Models
{
    public class UserDataGetter
    {
        public int Acc_id { get; set; }

        public string Acc_FirstName { get; set; }

        public string Acc_LastName { get; set; }

        public string Acc_MiddleName { get; set; }

        public string Acc_DisplayName { get; set; }

        public string Acc_Birthdate { get; set; }

        public string Acc_PhoneNum { get; set; }

        public string Acc_Address { get; set; }

        public string Acc_Email { get; set; }

        public string Acc_Password { get; set; }

        public string Acc_UserType { get; set; }

        public int Acc_Active { get; set; }

        public string Acc_BanDuration { get; set; }
        


        public string userId { get; set; } // Selected User
        public string setTimeBan{ get; set; }
    }
}
