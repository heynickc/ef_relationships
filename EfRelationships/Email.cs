using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EfRelationships {
    public enum EmailAddressType {
        To,
        Cc,
        Bcc
    }

    [Table("Emails")]
    public class Email {
        [Key]
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromAddress { get; set; }
        public virtual ICollection<EmailAddress> ToAddresses { get; set; }
    }
    
    //[Table("EmailAddressMap")]
    //public class EmailAddressMap {
    //    [Key]
    //    public int ID { get; set; }
    //    public EmailAddress EmailAddress { get; set; }
    //    public EmailAddressType EmailAddressType { get; set; }
    //}

    [Table("EmailAddresses")]
    public class EmailAddress {
        [Key]
        public int ID { get; set; }
        public string Address { get; set; }
        public EmailAddressType EmailAddressType { get; set; }
    }

    public class EfRelationshipsDbContext : DbContext {
        public EfRelationshipsDbContext() : base("name=EfRelationshipsDb") {
            Database.SetInitializer<EfRelationshipsDbContext>(new DropCreateDatabaseAlways<EfRelationshipsDbContext>());
        }
        public virtual DbSet<Email> Emails { get; set; }
        //public virtual DbSet<EmailAddressMap> EmailAddressMaps { get; set; }
        public virtual DbSet<EmailAddress> EmailAddresses { get; set; }
    }
}
