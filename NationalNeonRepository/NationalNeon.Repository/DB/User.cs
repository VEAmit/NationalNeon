using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NationalNeon.Repository.DB
{
    public partial class User
    {
        [Key]
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public int unsuccesful_login_attempt { get; set; }
        public Guid reset_me_guid { get; set; }
        public DateTime reset_expiry_date { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }
    }
}
