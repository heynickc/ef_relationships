using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EfRelationships.Test {

    public class InitializeDb {
        [Fact]
        public void SeedEmailsTable() {
            using (EfRelationshipsDbContext db = new EfRelationshipsDbContext()) {
                Email email = new Email() {
                    Subject = "Hello",
                    Body = "Here's some work for you to derail you from your project entirely!",
                    FromAddress = "test@test.com",
                    ToAddresses = new List<EmailAddress> {
                        new EmailAddress() {
                            Address = "test@test.com",
                            EmailAddressType = EmailAddressType.To
                        },
                        new EmailAddress() {
                            Address = "test@test.com",
                            EmailAddressType = EmailAddressType.To
                        },
                        new EmailAddress() {
                            Address = "test@test.com",
                            EmailAddressType = EmailAddressType.Cc
                        },
                        new EmailAddress() {
                            Address = "test@test.com",
                            EmailAddressType = EmailAddressType.Cc
                        },
                        new EmailAddress() {
                            Address = "test@test.com",
                            EmailAddressType = EmailAddressType.Bcc
                        },
                        new EmailAddress() {
                            Address = "test@test.com",
                            EmailAddressType = EmailAddressType.Bcc
                        },
                    }
                };

                db.Emails.Add(email);
                db.SaveChanges();
            }
        }

        [Fact]
        public void SendAnEmail() {
            using (EfRelationshipsDbContext db = new EfRelationshipsDbContext()) {

                var email = db.Emails.Where(e => e.ID == 1).FirstOrDefault();
                var toAddresses = email.ToAddresses
                    .Where(x => x.EmailAddressType == EmailAddressType.To)
                    .Select(y => new MailAddress(y.Address))
                    .Select(z => new MailAddressCollection() { z }).FirstOrDefault();
                var ccAddresses = email.ToAddresses
                    .Where(x => x.EmailAddressType == EmailAddressType.Cc)
                    .Select(y => new MailAddress(y.Address))
                    .Select(z => new MailAddressCollection() { z }).FirstOrDefault();
                var bccAddresses = email.ToAddresses
                    .Where(x => x.EmailAddressType == EmailAddressType.Bcc)
                    .Select(y => new MailAddress(y.Address))
                    .Select(z => new MailAddressCollection() { z }).FirstOrDefault();

                var emailMessage = new MailMessage() {
                    Subject = email.Subject,
                    Body = email.Body,
                };

                foreach (var address in toAddresses) {
                    emailMessage.To.Add(address);
                }
                foreach (var address in ccAddresses) {
                    emailMessage.CC.Add(address);
                }
                foreach (var address in bccAddresses) {
                    emailMessage.Bcc.Add(address);
                }
            }
        }
    }
}
