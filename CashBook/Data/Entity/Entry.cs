using CashBook.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashBook.DAL.Entity
{
    public class Entry
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime dateTime { get; set; }
        public string Description { get; set; }
        [Required]
        public double Amount { get; set; }
        public double Balance { get; set; }
        public bool Type { get; set; }
        public User user { get; set; }

    }
}
