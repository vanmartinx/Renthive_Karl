namespace RentHive.Models
{
    public class UserPostGetter
    {
        public int Post_id { get; set; }
        public string Post_Title { get; set; }
        public string Post_RentPeriod { get; set; }
        public string Post_Term { get; set; }
        public string Post_Calendar { get; set;}
        public string Post_Status { get; set;}
        public string Post_Price { get; set; }
        public int Post_Active{ get; set;}
        public string Post_AdvDeposit { get; set;}
        public string Post_DatePosted { get; set;}
        public string Rental_id { get; set;}

    }
}
