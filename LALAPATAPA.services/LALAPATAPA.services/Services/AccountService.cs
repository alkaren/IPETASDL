using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using LALAPATAPA.services.Models;
using LALAPATAPA.services.Helpers;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace LALAPATAPA.services.Services
{
    #region interfaces
    public interface IAccountService
    {
        Task<Account> Authenticate(string username, string password);
        Task<string> RecoveryPassword(string email);
    }
    #endregion

    public class AccountService : IAccountService
    {

        private readonly lalapatapadbContext _context;
        private readonly AppSetting _appSetting;
        private readonly IConfiguration _config;

        public AccountService(lalapatapadbContext context, IOptions<AppSetting> appSetting, IConfiguration iConfig)
        {
            _context = context;
            _appSetting = appSetting.Value;
            _config = iConfig;
        }

        public async Task<Account> Authenticate(string username, string password)
        {
            var user = await _context.Account.SingleOrDefaultAsync(acc => acc.Username == username && acc.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.IdAccount.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public async Task<string> RecoveryPassword(string email)
        {
            string emailserver = _config.GetSection("EmailSettings").GetSection("Email").Value;
            string emailpassword = _config.GetSection("EmailSettings").GetSection("Password").Value;
            int port = Convert.ToInt32(_config.GetSection("EmailSettings").GetSection("SMTPPort").Value);

            try
            {
                // generate password
                RandomGenerator generator = new RandomGenerator();
                string pass = generator.RandomPassword();

                Console.WriteLine($"Random string of 6 chars is {pass}");

                using (MailMessage mail = new MailMessage())
                {
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    var body = $"Silahkan login dengan password baru anda '{pass}'";
                    var subject = "Lupa Password";
                    mail.From = new MailAddress(emailserver);
                    mail.To.Add(email);
                    mail.Subject = subject;
                    mail.Body = body;

                    SmtpServer.Port = port;
                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.EnableSsl = true;
                    SmtpServer.UseDefaultCredentials = true;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(emailserver, emailpassword);

                    SmtpServer.Send(mail);
                }

                return pass;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

            }
        }
    }
}
