using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccessLibrary.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public DateTime DateVote { get; set; }
        public User User { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}